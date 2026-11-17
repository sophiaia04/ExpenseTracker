using ExpenseTracker.Models.CategoryExpenses;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models;

public class ExpenseContext : DbContext
{
    // Tell EF Core to build a table out of your Expense model
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Income> Incomes { get; set; }
    
    public DbSet<VacationExpense> VacationExpenses { get; set; }
    public DbSet<TechExpense> TechExpenses { get; set; }
    public DbSet<ShoppingExpense> ShoppingExpenses { get; set; }
    public DbSet<SbbExpense> SbbExpenses { get; set; }
    public DbSet<PresentExpense> PresentExpenses { get; set; }
    public DbSet<OthersExpense> OthersExpenses { get; set; }
    public DbSet<GroceryExpense> GroceryExpenses { get; set; }
    public DbSet<EatOutExpense> EatOutExpenses { get; set; }
    public DbSet<ActivityExpense> ActivityExpenses { get; set; }
    public DbSet<AccommodationExpense> AccommodationExpenses { get; set; }
    

    // 3. Configure where the file actually lives on your hard drive
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // This will create a file named "expenses.db" right next to your .exe file
        optionsBuilder.UseSqlite("Data Source=expenses.db");
    }
}