using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HomeTheater.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace HomeTheater.EF
{
    public class MyContext : DbContext
    {
       public class Connections
    {
        public string connStr { get; set; }
    }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json").Build();
                var appConfig = new Connections();
                builder.GetSection("Connections").Bind(appConfig);
                optionsBuilder.UseSqlServer(appConfig.connStr);
            }
        }
        public DbSet<Music> Musics { get; set; }
    }
}