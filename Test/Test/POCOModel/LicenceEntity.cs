namespace Test.POCOModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LicenceEntity : DbContext
    {
        public LicenceEntity()
            : base("name=LicenceEntity")
        {
        }

        public virtual DbSet<Licence> Licence { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Licence>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Licence>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Pwd)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.Licence)
                .WithOptional(e => e.UserInfo)
                .HasForeignKey(e => e.LendUserID);
        }
    }
}
