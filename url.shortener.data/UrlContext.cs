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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GkamaUrl>(e =>
            {
                e.ToTable("gkama_url");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").IsRequired();
                e.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").IsRequired();
                e.Property(x => x.Target).HasColumnName("target").HasMaxLength(200).IsRequired();
                e.Property(x => x.ShortUrl).HasColumnName("short_url").HasMaxLength(50);
            });
        }
    }
}
