namespace ExpenseTracker.Models;

public class MonthlySummary
{
    public string Month { get; set; }
    public decimal Income { get; set; }
    public decimal Grocery { get; set; }
    public decimal EatOut { get; set; }
    public decimal Activity { get; set; }
    public decimal Shopping { get; set; }
    public decimal Present { get; set; }
    public decimal Tech { get; set; }
    public decimal Investment { get; set; }
    public decimal Sbb { get; set; }
    public decimal Accommodation { get; set; }
    public decimal Vacation { get; set; }
    public decimal Others { get; set; }
    
    public decimal Total { get; set; }
}