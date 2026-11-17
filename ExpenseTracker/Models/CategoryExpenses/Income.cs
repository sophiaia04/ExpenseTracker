using System;

namespace ExpenseTracker.Models.CategoryExpenses;

public class Income
{
    public int Id { get; set; } 
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Comments { get; set; }
    public DateTime DateSaved { get; set; }
}