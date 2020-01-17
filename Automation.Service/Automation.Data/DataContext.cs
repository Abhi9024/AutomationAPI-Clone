using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Automation.Core;

namespace Automation.Data
{
    /// <summary>
    /// TBD-- Try using EF for entity migrations --For docker implementation
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TestScripts> TestScripts { get; set; }
        public DbSet<ModuleController> TestController1 { get; set; }
        public DbSet<TestController> TestController2 { get; set; }
        public DbSet<BrowserVMExec> TestController3 { get; set; }
        public DbSet<KeywordLibrary> Keyword { get; set; }
        public DbSet<Repository> Repository { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestScripts>(e => e.HasKey(k => k.ID));
            modelBuilder.Entity<ModuleController>(e => e.HasKey(k => k.ID));
            modelBuilder.Entity<TestController>(e => e.HasKey(k => k.ID));
            modelBuilder.Entity<BrowserVMExec>(e => e.HasKey(k => k.ID));
            modelBuilder.Entity<KeywordLibrary>(e => e.HasKey(k => k.ID));
            modelBuilder.Entity<Repository>(e => e.HasKey(k => k.ID));

        }

    }
}
