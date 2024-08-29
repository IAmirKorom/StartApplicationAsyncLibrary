using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StartApplicationAsyncLibrary
{
    public class StartApplicationAsyncLibraryHelper
    {
        private readonly Func<Task> _runAsync;
        private readonly IConfiguration _configuration;

        public StartApplicationAsyncLibraryHelper(Func<Task> runAsync, IConfiguration configuration)
        {
            _runAsync = runAsync ?? throw new ArgumentNullException(nameof(runAsync));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task StartAsync()
        {
            // Get Configuration
            var appSettings = _configuration.GetSection("profiles:http:applicationUrl").Get<ProfileSettings>();
            var url = appSettings?.ApplicationUrl ?? "https://127.0.0.1:5570";
            string browser;

            var os = GetOperatingSystem();

            if (os == OperatingSystem.Windows)
            {
                browser = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            }
            else if (os == OperatingSystem.Linux)
            {
                browser = "/usr/bin/google-chrome";
            }
            else if (os == OperatingSystem.MacOS)
            {
                browser = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
            }
            else
            {
                throw new NotSupportedException("Unsupported OS");
            }

            // Open browser after 2 seconds
            await Task.Delay(2000);

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = browser,
                    Arguments = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open the browser: {ex.Message}");
            }

            // Start server
            await _runAsync();
        }

        public OperatingSystem GetOperatingSystem()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return OperatingSystem.Windows;
            }
            else if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return OperatingSystem.Linux;
            }
            else if (Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                return OperatingSystem.MacOS;
            }
            else
            {
                return OperatingSystem.Unknown;
            }
        }
    }

    public class ProfileSettings
    {
        public string ApplicationUrl { get; set; } = "https://127.0.0.1:5570"; // The default value
    }

    public enum OperatingSystem
    {
        Windows,
        MacOS,
        Linux,
        Unknown
    }
}
