using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace url.shortener.data
{
    public class UrlContext : DbContext
    {
        public virtual DbSet<GkamaUrl> Urls { get; set; }
        public virtual DbSet<GkamaUrlMetadata> UrlMetadata { get; set; }
 
        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        { }

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

            modelBuilder.Entity<GkamaUrlMetadata>(e =>
            {
                e.ToTable("gkama_url_metadata");

                e.HasKey(x => x.Target);

                e.Property(x => x.Target).HasColumnName("target").HasMaxLength(1000).IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").IsRequired();
                e.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").IsRequired();
                e.Property(x => x.Scheme).HasColumnName("scheme").HasMaxLength(100);
                e.Property(x => x.Domain).HasColumnName("domain").HasMaxLength(100);
                e.Property(x => x.Port).HasColumnName("port");
                e.Property(x => x.Path).HasColumnName("path").HasMaxLength(100);
                e.Property(x => x.Query).HasColumnName("query");
                e.Property(x => x.Fragment).HasColumnName("fragment");
            });
        }
    }
}
