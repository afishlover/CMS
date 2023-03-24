using ApplicationLayer;

namespace Api.Services;

public class RegenCodeService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RegenCodeService> _logger;

    public RegenCodeService(IServiceProvider serviceProvider, ILogger<RegenCodeService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            $"{nameof(RegenCodeService)} is running.");

        await DoWorkAsync(stoppingToken);
    }

    private async Task DoWorkAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            $"{nameof(RegenCodeService)} is working.");
        
        var timer = new PeriodicTimer(TimeSpan.FromHours(2));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IUnitOfWork scopedProcessingService =
                    scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                
            }
        }
    }


    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            $"{nameof(RegenCodeService)} is stopping.");
        return base.StopAsync(cancellationToken);
    }
}