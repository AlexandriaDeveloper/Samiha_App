using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure
{
    public class AppContext2 : DbContext
    {

        public DbSet<Collage> Collages { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<DailyBoxes> DailyBoxes { get; set; }
        public DbSet<Daily> Dailies { get; set; }

        public AppContext2(DbContextOptions<AppContext2> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>().Property(x => x.SumTax).HasComputedColumnSql("[TaxNormal]+[Stamp]+[Taxsettlement]+[Tax2]+[Other]");
            // modelBuilder.Entity<DailyBoxes>().Property(x => x.Total).HasComputedColumnSql(" [a.Id,Sum(e.SumTax)] as Total from DailyBoxes as a join Forms as e on a.Id=e.DailyBoxId group by a.Id");

            modelBuilder.Entity<Daily>().HasIndex(u => u.DailyDate).IsUnique();
            modelBuilder.Entity<Daily>().Property(e => e.DailyDate).HasColumnType("Date");
            modelBuilder.Entity<DailyBoxes>().HasIndex(u => new { u.BoxId, u.DailyId }).IsUnique();


            modelBuilder.Entity<DailyBoxes>().Ignore(x => x.Total);
            modelBuilder.Entity<DailyBoxes>().Ignore(x => x.TotalTaxDevelopment);
            modelBuilder.Entity<Form>().HasOne(t => t.DailyBoxes).WithMany(m => m.Forms).HasForeignKey(k => k.DailyBoxId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Form>()
            .Property(p => p.Tax2).HasColumnType("decimal(8,2)").HasDefaultValue(0);
            modelBuilder.Entity<Form>()
          .Property(p => p.TaxDevelopment).HasColumnType("decimal(8,2)").HasDefaultValue(0);
            modelBuilder.Entity<Form>()
          .Property(p => p.TaxNormal).HasColumnType("decimal(8,2)").HasDefaultValue(0);
            modelBuilder.Entity<Form>()
          .Property(p => p.SumTax).HasColumnType("decimal(8,2)");
            modelBuilder.Entity<Form>()
          .Property(p => p.Stamp).HasColumnType("decimal(8,2)").HasDefaultValue(0);
            modelBuilder.Entity<Form>()
          .Property(p => p.Taxsettlement).HasColumnType("decimal(8,2)").HasDefaultValue(0);
            modelBuilder.Entity<Form>()
          .Property(p => p.Other).HasColumnType("decimal(8,2)").HasDefaultValue(0);

            //   modelBuilder.Entity<DailyBoxes>()
            // .Property(p => p.Total).HasColumnType("decimal(8,2)");
            //   modelBuilder.Entity<DailyBoxes>()
            // .Property(p => p.TotalTaxDevelopment).HasColumnType("decimal(8,2)");


        }
    }
}