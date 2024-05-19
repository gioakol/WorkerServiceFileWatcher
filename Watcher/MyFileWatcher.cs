using WorkerServiceFileWatcher.Entity;

namespace WorkerServiceFileWatcher.Watcher;

public class MyFileWatcher : IMyFileWatcher
{
    ILogger<MyFileWatcher> _logger;
    IServiceProvider _serviceProvider;
    private readonly ConfigSettings _options;

    private string _directoryName;
    private string _fileFilter = "*.*";
    FileSystemWatcher _fileSystemWatcher;

    public MyFileWatcher(ILogger<MyFileWatcher> logger, IServiceProvider serviceProvider, ConfigSettings options)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _options = options;

        _directoryName = _options.PathFolder;

        if (!Directory.Exists(_directoryName))
            Directory.CreateDirectory(_directoryName);

        _fileSystemWatcher = new FileSystemWatcher(_directoryName, _fileFilter);

    }

    public void Start()
    {
        _fileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             | NotifyFilters.DirectoryName
                             | NotifyFilters.FileName
                             | NotifyFilters.LastAccess
                             | NotifyFilters.LastWrite
                             | NotifyFilters.Security
                             | NotifyFilters.Size;

        _fileSystemWatcher.Created += _fileSystemWatcher_Created;
        _fileSystemWatcher.Deleted += _fileSystemWatcher_Deleted;
        _fileSystemWatcher.Renamed += _fileSystemWatcher_Renamed;
        _fileSystemWatcher.Error += _fileSystemWatcher_Error;

        _fileSystemWatcher.EnableRaisingEvents = true;
        _fileSystemWatcher.IncludeSubdirectories = true;

        _logger.LogInformation($"File Watching has started for directory {_directoryName}");
    }

    private void _fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var consumerService = scope.ServiceProvider.GetRequiredService<IFileConsumerService>();
            Task.Run(() => consumerService.ConsumeFile_Created(e.FullPath));
        }
    }

    private void _fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var consumerService = scope.ServiceProvider.GetRequiredService<IFileConsumerService>();
            Task.Run(() => consumerService.ConsumeFile_Renamed(e.FullPath));
        }
    }

    private void _fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var consumerService = scope.ServiceProvider.GetRequiredService<IFileConsumerService>();
            Task.Run(() => consumerService.ConsumeFile_Deleted(e.FullPath));
        }
    }

    private void _fileSystemWatcher_Error(object sender, ErrorEventArgs e)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var consumerService = scope.ServiceProvider.GetRequiredService<IFileConsumerService>();
            Task.Run(() => consumerService.ConsumeFile_Error(e.GetException().Message));
        }
    }
}