using Serilog;
using WorkerServiceFileWatcher;
using WorkerServiceFileWatcher.Entity;
using WorkerServiceFileWatcher.Watcher;

/* Console App */
IHost host = Host.CreateDefaultBuilder(args)
                 .ConfigureServices((hostContext, services) =>
                 {
                     IConfiguration configuration = hostContext.Configuration;
                     ConfigSettings configSettings = new ConfigSettings
                     {
                         PathFolder = configuration.GetSection("ConfigFolder").GetValue<string>("PathFolder"),
                         PathLogFolder = configuration.GetSection("Log").GetValue<string>("PathLogFolder"),
                         ConnectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("ConnectionString")
                     };

                     services.AddSingleton(configSettings);
                     services.AddHostedService<Worker>();
                     services.AddSingleton<IMyFileWatcher, MyFileWatcher>();
                     services.AddScoped<IFileConsumerService, FileConsumerService>();
                 }).Build();

await host.RunAsync();


/* Windows Service App - log with Serilog */

//IConfiguration lConfiguration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
//Log.Logger = new LoggerConfiguration().WriteTo.File(lConfiguration["Log:PathLogFolder"]).CreateLogger();

//var host = Host.CreateDefaultBuilder(args)
//                .UseWindowsService(options =>
//                {
//                    options.ServiceName = lConfiguration["WindowsService:ServiceName"];
//                })
//                .UseSerilog()
//                .ConfigureServices((hostContext, services) =>
//                {
//                    IConfiguration configuration = hostContext.Configuration;
//                    ConfigSettings configSettings = new ConfigSettings
//                    {
//                        PathFolder = configuration.GetSection("ConfigFolder").GetValue<string>("PathFolder"),
//                        PathLogFolder = configuration.GetSection("Log").GetValue<string>("PathLogFolder"),
//                        ConnectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("ConnectionString")
//                    };

//                    services.AddSingleton(configSettings);
//                    services.AddHostedService<Worker>();
//                    services.AddSingleton<IMyFileWatcher, MyFileWatcher>();
//                    services.AddScoped<IFileConsumerService, FileConsumerService>();
//                })
//                .Build();

//await host.RunAsync();