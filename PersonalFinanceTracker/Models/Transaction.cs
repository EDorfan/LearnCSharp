using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

// Transaction model

// Defines a transaction entity in the database
// Contains the transaction's description, amount, date, user ID, category ID
// Links this to the ApplicationUser and Category models


namespace PersonalFinanceTracker.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        // Links the transaction to the user who created it
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }


        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string Description { get; set; }
        

        [Required]
        public TransactionType Type { get; set; } // Enum: Income or Expense


    }
    public enum TransactionType
    {
        Income,
        Expense
    }
}
