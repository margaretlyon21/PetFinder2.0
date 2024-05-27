using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetFinder2._0.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string? Name {  get; set; }

        [Required]
        public string? IdentityUderID { get; set; }
        [ForeignKey("IdentityUserID")]
        public IdentityUser? User { get; set; }
    }
}
