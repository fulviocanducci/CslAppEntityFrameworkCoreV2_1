using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies.Internal;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Models
{
    public partial class DatabasecoreContext : DbContext
    {
        public DatabasecoreContext()
        {
            
        }

        //DbSet
        public virtual DbSet<Credit> Credit { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<People> People { get; set; }

        //DbQuery (ReadOnly)
        public virtual DbQuery<LayoutView> LayoutView { get; set; }
        public virtual DbQuery<VNoticeCredit> VNoticeCredit { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var strCtx = @"Data Source=.\SqlExpress;Initial Catalog=DatabaseCore;Persist Security Info=True;User ID=sa;Password=senha";
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLazyLoadingProxies()
                    .UseSqlServer(strCtx);
                    
            }
        }

        public int InsCredit(int creditId, string title, DateTime dateCreated)
        {
            var CreditId = new SqlParameter("CreditId", creditId);
            var Title = new SqlParameter("Title", title);
            var DateCreated = new SqlParameter("DateCreated", dateCreated);                    
            return Database.ExecuteSqlCommand("InsCredit @CreditId, @Title, @DateCreated", CreditId, Title, DateCreated);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User and Phone
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).HasMaxLength(20);
                entity.Property(e => e.Password)
                    .HasConversion(
                        from => Convert.ToBase64String(Encoding.UTF8.GetBytes(from)),
                        to => Encoding.UTF8.GetString(Convert.FromBase64String(to)))
                    .HasMaxLength(60);

                entity.HasMany(x => x.Phones)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .HasPrincipalKey(x => x.Id);
            });
            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("Phone");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .UseSqlServerIdentityColumn();
                entity.Property(x => x.UserId)
                    .IsRequired();
                entity.Property(x => x.Number)
                    .HasMaxLength(12)
                    .IsRequired();
                entity.Property(x => x.Ddd)
                    .HasMaxLength(3)
                    .IsRequired();

                entity.HasOne(x => x.User)
                    .WithMany(x => x.Phones)
                    .HasForeignKey(x => x.UserId)
                    .HasPrincipalKey(x => x.Id);
            });
            #endregion
            #region Credit
            modelBuilder.Entity<Credit>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Status).HasConversion<string>();
                /*
                    .HasConversion(new CreditStateValueConverter(
                                to => Enum.GetName(typeof(State), to), 
                                from => Enum.Parse<State>(from)));*/
            });
            #endregion
            #region Notice
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
            #endregion
            #region LayoutView
            modelBuilder
                .Query<LayoutView>()
                .ToQuery(() => Credit        
                    .Where(w => w.Status == State.Active)
                    .Select(s => new LayoutView
                    {
                        Id = s.Id, 
                        Text = s.Name                      
                    }));
            #endregion
            #region VNoticeCredit
            modelBuilder
                .Query<VNoticeCredit>(entity =>
                {
                    entity.ToView("VNoticeCredit");
                });
            #endregion
            #region People
            modelBuilder.Entity<People>(entity =>
            {
                entity.ToTable("People");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Status).IsRequired();
                entity.Ignore(x => x.NameStatus);

            });
            #endregion
        }
    }
}
