using System;

namespace ExpenseTracker.Models.CategoryExpenses;

public class TechExpense
{
    public int Id { get; set; } 
    public decimal Amount { get; set; }
    public string Product { get; set; } 
    public string Comments { get; set; }
    public DateTime DateSaved { get; set; }
}