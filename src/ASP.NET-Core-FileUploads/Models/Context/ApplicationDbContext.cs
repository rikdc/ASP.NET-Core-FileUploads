using Microsoft.AspNetCore.Hosting;
using Microsoft.DotNet.InternalAbstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace ASP.NET_Core_FileUploads.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        private IHostingEnvironment _hostingEnvironment;

        public ApplicationDbContext(
            IHostingEnvironment environment,
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _hostingEnvironment = environment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = $"FileName={_hostingEnvironment.ContentRootPath}upload-demo.db";
            optionsBuilder.UseSqlite(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Document> Documents { get; set; }
    }
}
