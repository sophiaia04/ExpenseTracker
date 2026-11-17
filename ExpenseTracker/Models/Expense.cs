using System;
using ExpenseTracker.Models.CategoryExpenses;

namespace ExpenseTracker.Models;

public class Expense
{
    public int Id { get; set; } 
    
    public decimal Total { get; set; }
    public decimal Input { get; set; }
    public decimal Output { get; set; }
    public string Category { get; set; }
    public string Comments { get; set; }
    public DateTime DateSaved { get; set; }
    
    public int? IncomeDetailsId { get; set; }
    public Income IncomeDetails { get; set; }
    
    // FREMDSCHLÜSSEL
    public int? VacationDetailsId { get; set; }
    public VacationExpense VacationDetails { get; set; }
    
    public int? TechDetailsId { get; set; }
    public TechExpense TechDetails { get; set; }
    
    public int? ShoppingDetailsId { get; set; }
    public ShoppingExpense ShoppingDetails { get; set; }
    
    public int? SbbDetailsId { get; set; }
    public SbbExpense SbbDetails { get; set; }
    
    public int? OthersDetailsId { get; set; }
    public OthersExpense OthersDetails { get; set; }
    
    public int? GroceryDetailsId { get; set; }
    public GroceryExpense GroceryDetails { get; set; }
    
    public int? EatOutExpenseDetailsId { get; set; }
    public EatOutExpense EatOutExpenseDetails { get; set; }
    
    public int? ActivityDetailsId { get; set; }
    public ActivityExpense ActivityDetails { get; set; }
    
    public int? AccommodationDetailsId { get; set; }
    public AccommodationExpense AccommodationDetails { get; set; }
    
    public int? PresentDetailsId { get; set; }
    public PresentExpense PresentDetails { get; set; }
    
    public string SubCategory 
    {
        get 
        {
            if (Category == "Shopping" && ShoppingDetails != null) return ShoppingDetails.Category;
            if (Category == "Tech" && TechDetails != null) return TechDetails.Product;
            if (Category == "Activity" && ActivityDetails != null) return ActivityDetails.Activity;
            if (Category == "Others" && OthersDetails != null) return OthersDetails.Product;
            if (Category == "Vacation" && VacationDetails != null) return VacationDetails.TripName;
        
            return "-"; // Wenn es keine Unterkategorie gibt
        }
    }
}