using System;

namespace ExpenseTracker.Models.CategoryExpenses;

public class VacationExpense
{
    // The database will automatically generate a unique number for this!
    public int Id { get; set; } 
    
    public string TripName { get; set; } 
    public decimal Food { get; set; }
    public decimal Transport { get; set; }
    public decimal Accommodation { get; set; }
    public decimal Shopping { get; set; }
    public string Comments { get; set; }
    public DateTime DateSaved { get; set; }
}