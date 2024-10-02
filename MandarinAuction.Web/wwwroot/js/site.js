let bidButtons = document.querySelectorAll('[data-bs-target="#bidMandarinModal"]');

bidButtons.forEach(b => {
    b.addEventListener('click', function () {
        document.getElementById('auctionId').value = this.getAttribute('data-auction-id');
        document.getElementById('mandarinBidSum').value = this.getAttribute('data-bid-sum');
    });
});

document.querySelectorAll('[data-buy-sum]').forEach(b => {
    b.addEventListener('click', async function () {
        const response = await fetch('/Auctions/Buy', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                auctionId: this.getAttribute('data-auction-id')
            })
        });

        if (response.ok) {
            location.reload();
        }
    });
});



document.forms.RaiseBidForm.addEventListener('submit', async function (e) {
    e.preventDefault();

    const auctionId = document.getElementById('auctionId').value;
    const bidSum = document.getElementById('mandarinBidSum').value;

    const response = await fetch('/Auctions/RaiseBid', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            auctionId: auctionId,
            bidSum: parseFloat(bidSum)
        })
    });

    if (response.ok) {
        alert('Ставка успешно поднята!');
        location.reload();
    } else {
        document.getElementById('raiseBidErrors').innerHTML = await response.text();
    }
});