using System;

namespace ExpenseTracker.Models.CategoryExpenses;

public class PresentExpense
{
    public int Id { get; set; } 
    public decimal Amount { get; set; }
    public string Comments { get; set; }
    public DateTime DateSaved { get; set; }
}