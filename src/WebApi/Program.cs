using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string currentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "saved_games.json");

            // Om filen inte existerar, skapa den.
            if (!File.Exists(currentDirectory))
                File.Create(currentDirectory);
            GameEngine.Game.LoadSaveService = new GameEngine.Drive(currentDirectory);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
