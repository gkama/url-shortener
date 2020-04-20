using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace url.shortener.data
{
    public class UrlContext : DbContext
    {
        public virtual DbSet<GkamaUrl> Urls { get; set; }
 
        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        { }

        public UrlContext()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=127.0.0.1;database=url;port=5432;username=root;password=root");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GkamaUrl>(e =>
            {
                e.ToTable("gkama_url");

                e.HasKey(x => x.Id);

                e.HasIndex(x => x.ShortUrl)
                    .IsUnique();

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").IsRequired();
                e.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").IsRequired();
                e.Property(x => x.Target).HasColumnName("target").HasMaxLength(1000).IsRequired();
                e.Property(x => x.ShortUrl).HasColumnName("short_url").HasMaxLength(50);
            });
        }
    }
}
