using EShop.Domain.Models.Comment;
using EShop.Domain.Models.Discount;
using EShop.Domain.Models.Home;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Products;
using EShop.Domain.Models.Ticket;
using EShop.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data.Context
{
    public class EshopDBContext : DbContext
    {
        public EshopDBContext(DbContextOptions<EshopDBContext> options) : base(options)
        {

        }


        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        #endregion

        #region Products
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion
        public DbSet<ContactUs> ContactUs { get; set; }

        public DbSet<ProductColor> ProductColors { get; set; }

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserFavourite> UserFavourites { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserCommentLikeOrDislike> UserCommentLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<ContactUs>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<ProductGallery>().HasQueryFilter(pg => !pg.IsDelete);
            modelBuilder.Entity<Discount>().HasQueryFilter(d => !d.IsDelete);
            modelBuilder.Entity<ProductColor>().HasQueryFilter(d => !d.IsDelete);
            modelBuilder.Entity<Ticket>().HasQueryFilter(d => !d.IsDelete);
            modelBuilder.Entity<TicketMessage>().HasQueryFilter(d => !d.IsDelete);

            modelBuilder.Entity<User>().HasMany<TicketMessage>(u => u.TicketMessages)
                            .WithOne(t => t.User)
                            .HasForeignKey(t => t.SenderId)
                            .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Ticket>().HasMany<TicketMessage>(u => u.TicketMessages)
                .WithOne(t => t.Ticket)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull);



            modelBuilder.Entity<Product>().HasMany<OrderDetail>(u => u.OrderDetails)
                .WithOne(t => t.Product)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<User>().HasMany<UserCommentLikeOrDislike>(u => u.UserCommentLikes)
                .WithOne(ucl => ucl.User)
                .HasForeignKey(ucl => ucl.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Comment>().HasMany<UserCommentLikeOrDislike>(c => c.UserCommentLikes)
                .WithOne(ucl => ucl.Comment)
                .HasForeignKey(ucl => ucl.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
               
               
        }
    }
}
