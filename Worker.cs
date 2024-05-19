using WorkerServiceFileWatcher.Entity;
using WorkerServiceFileWatcher.Watcher;

namespace WorkerServiceFileWatcher;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMyFileWatcher _watcher;
    private readonly ConfigSettings _options;

    public Worker(ILogger<Worker> logger, IMyFileWatcher watcher, ConfigSettings options)
    {
        _logger = logger;
        _watcher = watcher;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _watcher.Start();

        while (!stoppingToken.IsCancellationRequested)
        {
        }
    }
}
