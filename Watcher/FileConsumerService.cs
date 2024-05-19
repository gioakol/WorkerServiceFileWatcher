using WorkerServiceFileWatcher.Entity;

namespace WorkerServiceFileWatcher.Watcher;

public class FileConsumerService : IFileConsumerService
{
    private readonly ConfigSettings _options;
    ILogger<FileConsumerService> _logger;
    private string _connectionString = string.Empty;

    public FileConsumerService(ILogger<FileConsumerService> logger, ConfigSettings options)
    {
        _logger = logger;
        _options = options;
        _connectionString = _options.ConnectionString;
    }

    public async Task ConsumeFile_Created(string pathFile)
    {
        string conn = _connectionString;
        //Code u process when add new file
        _logger.LogInformation($"File added/created event for file {pathFile}");
    }

    public async Task ConsumeFile_Renamed(string pathFile)
    {
        string conn = _connectionString;
        //Code u process when rename file
        _logger.LogInformation($"File renamed event for file {pathFile}");
    }

    public async Task ConsumeFile_Deleted(string pathFile)
    {
        string conn = _connectionString;
        //Code u process when delete file
        _logger.LogInformation($"File deleted event for file {pathFile}");
    }

    public async Task ConsumeFile_Error(string errorMessage)
    {
        string conn = _connectionString;
        //save error messages
        _logger.LogInformation($"File error event {errorMessage}");
    }
}