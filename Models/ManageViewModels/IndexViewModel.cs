#region Using

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace SmartAdmin.Seed.Models.ManageViewModels
{
    public class IndexViewModel
    {

        public string ID { get; set; }
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        public string FullName { get; set; }

        public List<IdentityUserRole<string>> Roles { get; set; }
    }
}
