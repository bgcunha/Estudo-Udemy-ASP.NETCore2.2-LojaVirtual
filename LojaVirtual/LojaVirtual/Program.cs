﻿using LojaVirtual.Libraries.Bug_GetCurrentDirectory;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace LojaVirtual
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
