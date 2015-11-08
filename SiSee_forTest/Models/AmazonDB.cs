namespace SiSee_v1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AmazonDB : DbContext
    {
        public AmazonDB()
            : base("name=AmazonDB")
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<CommentRecord> CommentRecord { get; set; }
        public virtual DbSet<CustomSpot> CustomSpot { get; set; }
        public virtual DbSet<FavoriteRecord> FavoriteRecord { get; set; }
        public virtual DbSet<SearchRecord> SearchRecord { get; set; }
        public virtual DbSet<Spot> Spot { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentRecord>()
                .Property(e => e.comment_grade)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_FBID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_tel)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.user_other)
                .IsUnicode(false);
        }
    }
}
