using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace url.shortener.data
{
    public class UrlContext : DbContext
    {
        public virtual DbSet<GkamaUrl> Urls { get; set; }

        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GkamaUrl>(e =>
            {
                e.ToTable("gkama_url");

                e.HasKey(x => x.Id);
            });
        }
    }
}
