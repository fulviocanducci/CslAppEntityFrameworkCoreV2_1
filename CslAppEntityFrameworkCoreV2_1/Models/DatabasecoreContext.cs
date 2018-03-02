using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using System;
using System.Linq;
using System.Text;

namespace Models
{
    public partial class DatabasecoreContext : DbContext
    {
        public DatabasecoreContext()
        {
        }

        public virtual DbSet<Credit> Credit { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbQuery<LayoutView> LayoutView { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var strCtx = @"Data Source=.\SqlExpress;Initial Catalog=DatabaseCore;Persist Security Info=True;User ID=sa;Password=senha";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(strCtx)
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).HasMaxLength(20);
                entity.Property(e => e.Password)
                    .HasConversion(
                        from => Convert.ToBase64String(Encoding.UTF8.GetBytes(from)),
                        to => Encoding.UTF8.GetString(Convert.FromBase64String(to)))
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Credit>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Status)
                    .HasConversion<string>();/*
                    .HasConversion(new CreditStateValueConverter(
                                to => Enum.GetName(typeof(State), to), 
                                from => Enum.Parse<State>(from)));*/
            });           

            modelBuilder.Entity<Notice>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
                                
                entity.HasOne(d => d.Credit)
                    .WithMany(p => p.Notice)                    
                    .HasForeignKey(d => d.CreditId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notice_Credit");
            });

            modelBuilder.Query<LayoutView>()
                .ToQuery(() => Credit        
                    .Where(w => w.Status == State.Active)
                    .Select(s => new LayoutView
                    {
                        Id = s.Id, 
                        Text = s.Name                      
                    }));

            
        }
    }
}
