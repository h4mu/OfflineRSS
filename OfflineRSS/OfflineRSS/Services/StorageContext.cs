using Microsoft.EntityFrameworkCore;
using OfflineRSS.Models;
using System;
using System.IO;
using Xamarin.Forms;

namespace OfflineRSS.Services
{
    public class StorageContext : DbContext
    {
        private const string DatabaseName = "storage.db3";

        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Item> Items { get; set; }

        public StorageContext()
        {
            SQLitePCL.Batteries_V2.Init();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = null;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", DatabaseName);
                    break;
                case Device.UWP:
                case Device.WPF:
                case Device.GTK:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName);
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}