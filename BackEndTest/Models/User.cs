using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackEndTest.Models
{
    [Index("Email", IsUnique = true)]
    public class User : IdentityUser<int>
    {
        [Required]
        [EmailAddress]
        public override string Email { get; set; }


    }

}
