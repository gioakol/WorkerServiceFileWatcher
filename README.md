# WorkerService FileWatcher

This repository contains a Worker Service for monitoring file changes in a specified directory using .NET. The service logs file creation, modification, and deletion events, making it useful for applications that need to track file system changes.

## Features

- **File Monitoring**: Monitors a specified directory for file changes.
- **Event Logging**: Logs events such as file creation, modification, and deletion.
- **Worker Service**: Runs as a background service using .NET Worker Service.

## Technologies Used

- **.NET Core**
- **C#**
- **Worker Service**
- **FileSystemWatcher**

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/gioakol/WorkerServiceFileWatcher.git
   cd WorkerServiceFileWatcher
   ```

2. **Build the project**:
   ```bash
   dotnet build
   ```

3. **Run the application**:
   ```bash
   dotnet run
   ```

## Configuration

Configure the directory to be monitoring a folder `appsettings.json`:

```json
{
  "WorkerServiceFileWatcher": {
    "DirectoryToWatch": "C:\\path\\to\\directory"
  }
}
```

Configure the logPath to be save the log of actions `appsettings.json`:

```json
{
  "Log": {
    "PathLogFolder": "C:\\WorkerService\\Log\\log_Sistem.log"
  }
}
```

If you need database connection, configure the databse connection `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ConnectionString": "Server=DATABASE_SERVER;Database=DATABASE_NAME;User Id=USER_NAME;Password=PASSWORD;TrustServerCertificate=true;Trusted_Connection=Yes"
  }
}
```

## Contributing

Contributions are welcome! Please submit a pull request or open an issue for any improvements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.
