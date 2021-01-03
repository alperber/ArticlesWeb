using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ArticlesWeb.Entities.RequestModels
{
    public class UserRegisterModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [StringLength(50)]
        public string About { get; set; }
    }
}
