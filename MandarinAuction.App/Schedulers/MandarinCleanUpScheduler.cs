using MandarinAuction.Infrastructure;
using Microsoft.Extensions.Logging;

namespace MandarinAuction.App.Schedulers;

public class MandarinCleanUpScheduler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MandarinCleanUpScheduler> _logger;

    public MandarinCleanUpScheduler(IUnitOfWork unitOfWork, ILogger<MandarinCleanUpScheduler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task CleanUpExpiredMandarins()
    {
        var expiredMandarins = await _unitOfWork.MandarinRepository.GetExpiredMandarins();

        if (expiredMandarins.Any())
        {
            await _unitOfWork.MandarinRepository.RemoveRange(expiredMandarins);
            await _unitOfWork.Complete();

            _logger.LogInformation($"{expiredMandarins.Count()} мандаринов было удалено.");
        }
        else
        {
            _logger.LogInformation("Нет испорченных мандаринок для удаления.");
        }
    }
}
