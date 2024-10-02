#region usings

using MailKit.Net.Smtp;
using MandarinAuction.Domain.Events;
using MandarinAuction.Domain.Events.Auctions.AuctionClose;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using MandarinAuction.Domain.Models;

#endregion

namespace MandarinAuction.App.Services.Emails.AuctionBidEmailNotifyServices;

public class AuctionBidEmailNotifyService : IAuctionBidEmailNotifyService, IEventListener<AuctionCloseEventArgs>
{
    private readonly IConfiguration _configuration;

    public AuctionBidEmailNotifyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendBidRaised(string userEmail, string auctionName)
    {
        const string emailSubject = "Ваша ставка перебита!";
        var emailBody = $"Ваша ставка на {auctionName} была перебита. Сделайте новую ставку!";

        var message = CreateEmailMessage(userEmail, emailSubject, emailBody);

        await SendEmailAsync(message);
    }

    async Task IEventListener<AuctionCloseEventArgs>.Handle(AuctionCloseEventArgs eventArg)
    {
        if (eventArg.Auction.HighestBidder != null)
        {
            await SendCheck(eventArg.Auction.HighestBidder.Email, eventArg.Auction, eventArg.IsPurchased);
        }
    }

    public async Task SendCheck(string userEmail, Auction auction, bool isPurchased)
    {
        var emailSubject = $"Вы выиграли аукцион {auction.Mandarin.Name}!";
        var emailBody =
            $"Поздравляем! Вы выиграли аукцион {auction.Mandarin.Name}. Ваш окончательный счет: {(isPurchased ? auction.BuySum : auction.BidSum)} р.";

        var pdfData = GenerateCheckPdf(auction, isPurchased);
        var message = CreateEmailWithAttachment(userEmail, emailSubject, emailBody, pdfData, "auction-check.pdf");

        await SendEmailAsync(message);
    }

    private MimeMessage CreateEmailMessage(string recipientEmail, string subject, string body)
    {
        const string senderName = "Mandarin Auction";
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, _configuration["EmailSettings:SenderEmail"]));
        message.To.Add(new MailboxAddress("", recipientEmail));
        message.Subject = subject;

        message.Body = new TextPart("plain")
        {
            Text = body
        };

        return message;
    }

    private MimeMessage CreateEmailWithAttachment(string recipientEmail, string subject, string body, byte[] pdfData,
        string fileName)
    {
        var message = CreateEmailMessage(recipientEmail, subject, body);

        var attachment = new MimePart("application", "pdf")
        {
            Content = new MimeContent(new MemoryStream(pdfData)),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = fileName
        };

        var multipart = new Multipart("mixed")
        {
            new TextPart("plain") { Text = body },
            attachment
        };

        message.Body = multipart;

        return message;
    }

    private byte[] GenerateCheckPdf(Auction auction, bool isPurchased)
    {
        using var stream = new MemoryStream();
        using var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);

        var font = new XFont("Verdana", 20, XFontStyle.Bold);
        gfx.DrawString($"Чек на аукцион {auction.Mandarin.Name}", font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);
        gfx.DrawString($"цена: {(isPurchased ? auction.BuySum : auction.BidSum)} руб.", font, XBrushes.Black, new XRect(40, 100, page.Width, 0));

        document.Save(stream, false);

        return stream.ToArray();
    }

    private async Task SendEmailAsync(MimeMessage message)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_configuration["EmailSettings:SmtpHost"],
            int.Parse(_configuration["EmailSettings:SmtpPort"]), true);
        await client.AuthenticateAsync(_configuration["EmailSettings:SenderEmail"],
            _configuration["EmailSettings:Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }


}