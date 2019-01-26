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
            // Ange antingen sökväg till fil eller db connection string beroende på service.
            string source = 
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "saved_games.json");

            // Om filen inte existerar, skapa den.
            if (!File.Exists(source))
                File.Create(source);
            //GameEngine.Game.LoadSaveService = new GameEngine.Drive(currentDirectory);
            GamesContainer.LoadSaveService = new GameEngine.Drive(source);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
