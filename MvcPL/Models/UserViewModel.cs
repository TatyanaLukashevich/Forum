using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace MvcPL.Models
{
    public enum Role
    {
        Administrator = 1,
        Moderator,
        User,
        BannedUser  
    }
    
    public class UserViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        public Role Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IEnumerable<PostViewModel> UserPost { get; set; }
        public IEnumerable<MessageViewModel> UserMessage { get; set; }
        public int RoleId { get; set; }

    }
}