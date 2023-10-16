using EShop.Domain.common;
using EShop.Domain.Models.Comment;
using EShop.Domain.Models.Order;
using EShop.Domain.Models.Rating;
using EShop.Domain.Models.Ticket;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Models.Users
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(150)]
        public string Avatar { get; set; }
        [StringLength(150)]
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        #region Relations
        public List<Wallet>? Wallets { get; set; }
        public List<Models.Ticket.Ticket>? Tickets { get; set; }
        public List<TicketMessage>? TicketMessages { get; set; }
        public List<Order.Order>? Orders { get; set; }
        public List<UserFavourite>? UserFavourites { get; set; }
        public List<Comment.Comment>? Comments { get; set; }
        public List<UserCommentLikeOrDislike>? UserCommentLikes { get; set; }
        public List<Rate> Rates { get; set; }
        #endregion

    }
}
