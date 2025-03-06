using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Category model
// Defines a category entity in the database


// This defines transaction categories like Food, Rent, Entertainment.
// A category can have multiple transactions.
namespace PersonalFinanceTracker.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}