using System;

namespace ExpenseTracker.Models.CategoryExpenses;

public class ActivityExpense
{
    public int Id { get; set; } 
    public decimal Amount { get; set; }
    public string Activity { get; set; }
    public string Comments { get; set; }
    public DateTime DateSaved { get; set; }
}