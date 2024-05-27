using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder2._0.Models
{
    public class Pet
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }

        public string? Location {  get; set; }

        public string? ImagePath { get; set; }

        public bool IsFound { get; set; } = false;

        [Required]
        public string? IdentityUderID { get; set; }
        [ForeignKey("IdentityUserID")]
        public IdentityUser? User { get; set; }
        public List<Comment> Comments { get; set; }

        public Pet() { }
    }
}
