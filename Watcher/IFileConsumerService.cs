namespace WorkerServiceFileWatcher.Watcher;

public interface IFileConsumerService
{
    public Task ConsumeFile_Created(string inPathFile);
    public Task ConsumeFile_Renamed(string inPathFile);
    public Task ConsumeFile_Deleted(string inPathFile);
    public Task ConsumeFile_Error(string errorMessage);
}