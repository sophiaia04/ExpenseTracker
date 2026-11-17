using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseTracker.Models;
using ExpenseTracker.Models.CategoryExpenses;

namespace ExpenseTracker.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to ExpenseTracker!";
    
    private decimal _total = 0;
    
    private string _amount;
    private string _selectedCategory;
    private string _comments;

    private bool _isActivityVisible = false;
    private string _activity; 
    
    private bool _isTechVisible = false;
    private string _techProduct;
    
    private bool _isOthersVisible = false;
    private string _others; 
    
    private bool _isShoppingVisible  = false;
    private string _shoppingCategory;
    
    private bool _isVacationVisible  = false;
    private string _tripName;
    private string _vacFoodAmount;
    private string _vacTransportAmount;
    private string _vacAccommodationAmount;
    private string _vacShoppingAmount;
    private string _vacPresentAmount;
    
    private string _income;
    private string _selectedIncomeCategory;
    private string _incomeComments;
 
    [ObservableProperty]
    private string _errorText;

    [ObservableProperty]
    private bool _isHomeVisible = true;

    [ObservableProperty]
    private bool _isExpense2026Visible = false;

    [ObservableProperty]
    private bool _isMenuOpen = false;
    
    [ObservableProperty]
    private bool _isMonthVisible = false;
    
    [ObservableProperty]
    private decimal _groceryAmount;
    
    [ObservableProperty]
    private decimal _eatOutAmount;
    
    [ObservableProperty]
    private decimal _shoppingAmount;
    
    [ObservableProperty]
    private decimal _activityAmount;
    
    [ObservableProperty]
    private decimal _presentAmount;
    
    [ObservableProperty]
    private decimal _techAmount;
    
    [ObservableProperty]
    private decimal _investmentAmount;
    
    [ObservableProperty]
    private decimal _sbbAmount;
    
    [ObservableProperty]
    private decimal _accommodationAmount;
    
    [ObservableProperty]
    private decimal _vacationAmount;
    
    [ObservableProperty]
    private decimal _othersAmount;
    
    public ObservableCollection<Expense> SavedExpenses { get; } = new();
    
    public ObservableCollection<MonthlySummary> MonthlySummaries { get; } = new();
    
    public ObservableCollection<MonthlySummary> CategorySummaries { get; } = new();


    public MainWindowViewModel()
    {
        using (var db = new ExpenseContext())
        {
            db.Database.EnsureCreated();
           
            var allExpenses = db.Expenses.ToList();
            
            if (allExpenses.Any())
            {
                Total = allExpenses.Sum(e => e.Input) - allExpenses.Sum(e => e.Output);
            }
            else
            {
                Total = 0; 
            }
            
            var savedCategories = db.Expenses
                .Where(e => e.Input > 0 && !string.IsNullOrEmpty(e.Category))
                .Select(e => e.Category)
                .Distinct() 
                .ToList();
            
            foreach (var category in savedCategories)
            {
                if (!IncomeCategories.Contains(category))
                {
                    IncomeCategories.Add(category);
                }
            }
        }
    }
    

    [RelayCommand]
    private void ToggleMenu()
    {
        IsMenuOpen = !IsMenuOpen; 
    }
    
    [RelayCommand]
    private void ClickedHome()
    {
        IsMenuOpen = false;
        IsHomeVisible = true;
        IsExpense2026Visible = false;
        IsMonthVisible = false; 
    }

    [RelayCommand]
    private void ShowMonthView()
    {
        IsMenuOpen = false;
        IsHomeVisible = false;
        IsExpense2026Visible = false;
        IsMonthVisible = true;
        LoadDataFromDatabase();
    }

    [RelayCommand]
    private void ShowExpense2026()
    {
        IsMenuOpen = false;
        IsHomeVisible = false;
        IsExpense2026Visible = true;
        IsMonthVisible = false;
        LoadDataFromDatabase();
    }
    
    private void LoadDataFromDatabase()
    {
        SavedExpenses.Clear();
        MonthlySummaries.Clear();
        CategorySummaries.Clear();

        using (var db = new ExpenseContext())
        {
            var allData = db.Expenses.OrderBy(e => e.DateSaved).ToList();
            foreach (var item in allData)
            {
                SavedExpenses.Add(item);
            }

            // Gruppieren nach Jahr und Monat
            var groupedByMonth = allData
                .GroupBy(e => new { e.DateSaved.Year, e.DateSaved.Month })
                .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                .ToList(); 

            foreach (var group in groupedByMonth)
            {
                string monthName = new DateTime(group.Key.Year, group.Key.Month, 1)
                                    .ToString("MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var summary = new MonthlySummary
                {
                    Month = monthName,
                    Income = group.Where(e => e.Input > 0).Sum(e => e.Input),
                    Grocery = group.Where(e => e.Category == "Grocery").Sum(e => e.Output),
                    EatOut = group.Where(e => e.Category == "EatOut").Sum(e => e.Output),
                    Activity = group.Where(e => e.Category == "Activity").Sum(e => e.Output),
                    Shopping = group.Where(e => e.Category == "Shopping").Sum(e => e.Output),
                    Present = group.Where(e => e.Category == "Present").Sum(e => e.Output),
                    Tech = group.Where(e => e.Category == "Tech").Sum(e => e.Output),
                    Investment = group.Where(e => e.Category == "Investment").Sum(e => e.Output),
                    Sbb = group.Where(e => e.Category == "SBB").Sum(e => e.Output),
                    Accommodation = group.Where(e => e.Category == "Accommodation").Sum(e => e.Output),
                    Vacation = group.Where(e => e.Category == "Vacation").Sum(e => e.Output),
                    Others = group.Where(e => e.Category == "Others").Sum(e => e.Output),
                    Total = this.Total
                };
                MonthlySummaries.Add(summary);
                
                var allTimeSummary = new MonthlySummary
                {// Beschriftung für die Zeile
                    Income = allData.Where(e => e.Input > 0).Sum(e => e.Input),
                    Grocery = allData.Where(e => e.Category == "Grocery").Sum(e => e.Output),
                    EatOut = allData.Where(e => e.Category == "EatOut").Sum(e => e.Output),
                    Activity = allData.Where(e => e.Category == "Activity").Sum(e => e.Output),
                    Shopping = allData.Where(e => e.Category == "Shopping").Sum(e => e.Output),
                    Present = allData.Where(e => e.Category == "Present").Sum(e => e.Output),
                    Tech = allData.Where(e => e.Category == "Tech").Sum(e => e.Output),
                    Investment = allData.Where(e => e.Category == "Investment").Sum(e => e.Output),
                    Sbb = allData.Where(e => e.Category == "SBB").Sum(e => e.Output),
                    Accommodation = allData.Where(e => e.Category == "Accommodation").Sum(e => e.Output),
                    Vacation = allData.Where(e => e.Category == "Vacation").Sum(e => e.Output),
                    Others = allData.Where(e => e.Category == "Others").Sum(e => e.Output),
                    Total = this.Total 
                };

                CategorySummaries.Add(allTimeSummary);
                
                
            }
        }
    }

    public decimal Total
    {
        get => _total;
        set { _total = value; OnPropertyChanged(nameof(Total)); }
    }
    
    public string Income
    {
        get => _income;
        set { _income = value; OnPropertyChanged(nameof(Income)); }
    }

    public string SelectedIncomeCategory
    {
        get => _selectedIncomeCategory;
        set { _selectedIncomeCategory = value; OnPropertyChanged(nameof(SelectedIncomeCategory)); }
    }

    public string IncomeComments
    {
        get => _incomeComments;
        set { _incomeComments = value; OnPropertyChanged(nameof(IncomeComments)); }
    }
    
    public ObservableCollection<string> IncomeCategories { get; } = new()
    {
        "Siemens"
    };
    
    public string Amount
    {
        get => _amount;
        set
        {
            if (_amount != value)
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                SaveExpenseCommand.NotifyCanExecuteChanged();
            }
        }
    }

    public string Comments
    {
        get => _comments;
        set { _comments = value; OnPropertyChanged(nameof(Comments)); }
    }

    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            _selectedCategory = value;
            OnPropertyChanged(nameof(SelectedCategory));
            
            IsShoppingVisible = IsTechVisible = IsActivityVisible = IsVacationVisible = IsOthersVisible = false;
            switch (_selectedCategory)
            {
                case "Activity": IsActivityVisible = true; break;
                case "Tech": IsTechVisible = true; break;
                case "Shopping": IsShoppingVisible = true; break;
                case "Vacation": IsVacationVisible = true; break;
                case "Others": IsOthersVisible = true; break;
            }
        }
    }

    public ObservableCollection<string> Categories { get; } = new()
    {
        "Grocery", "EatOut", "Activity", "Shopping", "Present", "Tech", "Investment", "SBB", "Accommodation", "Vacation", "Others"
    };

    public bool IsShoppingVisible
    {
        get => _isShoppingVisible; 
        set { _isShoppingVisible = value; OnPropertyChanged(nameof(IsShoppingVisible)); }
    }

    public ObservableCollection<string> ShoppingCategories { get; } = new()
    {
        "Bag", "Cloths", "Shoes", "Accessories"
    };

    public string SelectedShoppingCategory
    {
        get => _shoppingCategory;
        set { _shoppingCategory = value; OnPropertyChanged(nameof(SelectedShoppingCategory)); }
    }

    public string ActivityWatermarkText { get; } = "What activity?";
    
    public bool IsActivityVisible
    {
        get => _isActivityVisible;
        set { _isActivityVisible = value; OnPropertyChanged(nameof(IsActivityVisible)); }
    }
   
    public string ActivityCategory
    {
        get => _activity;
        set { _activity = value; OnPropertyChanged(nameof(ActivityCategory)); }
    }
    
    public string TechWatermarkText { get; } = "What did you buy?";
    
    public bool IsTechVisible
    {
        get => _isTechVisible;
        set { _isTechVisible = value; OnPropertyChanged(nameof(IsTechVisible)); }
    }
   
    public string TechProductCategory
    {
        get => _techProduct;
        set { _techProduct = value; OnPropertyChanged(nameof(TechProductCategory)); }
    }
    
    public string OthersWatermarkText { get; } = "What did you buy?";
    
    public bool IsOthersVisible
    {
        get => _isOthersVisible;
        set { _isOthersVisible = value; OnPropertyChanged(nameof(IsOthersVisible)); }
    }
   
    public string OthersCategory
    {
        get => _others;
        set { _others = value; OnPropertyChanged(nameof(OthersCategory)); }
    }
    
    public bool IsVacationVisible
    {
        get => _isVacationVisible;
        set { _isVacationVisible = value; OnPropertyChanged(nameof(IsVacationVisible)); }
    }

    public string TripName
    {
        get => _tripName;
        set { _tripName = value; OnPropertyChanged(nameof(TripName)); }
    }
    
    public string vacFoodAmount
    {
        get => _vacFoodAmount;
        set { _vacFoodAmount = value; OnPropertyChanged(nameof(vacFoodAmount)); }
    }
    
    public string vacTransportAmount
    {
        get => _vacTransportAmount;
        set { _vacTransportAmount = value; OnPropertyChanged(nameof(vacTransportAmount)); }
    }
    
    public string vacAccommodationAmount
    {
        get => _vacAccommodationAmount;
        set { _vacAccommodationAmount = value; OnPropertyChanged(nameof(vacAccommodationAmount)); }
    }
    
    public string vacShoppingAmount
    {
        get => _vacShoppingAmount;
        set { _vacShoppingAmount = value; OnPropertyChanged(nameof(vacShoppingAmount)); }
    }
    
    public string vacPresentAmount
    {
        get => _vacPresentAmount;
        set { _vacPresentAmount = value; OnPropertyChanged(nameof(vacPresentAmount)); }
    }
    
    [RelayCommand(CanExecute = nameof(ClickOkButton))]
    private void SaveExpense()
    {
        ErrorText = ""; 
        bool incomeAmountEntered = !string.IsNullOrWhiteSpace(Income);
        bool incomeCategoryEntered = !string.IsNullOrWhiteSpace(SelectedIncomeCategory);
        bool incomeCommentsEntered = !string.IsNullOrWhiteSpace(IncomeComments);
        
        bool expenseAmountEntered = !string.IsNullOrWhiteSpace(Amount);
        bool expenseCategoryEntered = SelectedCategory != null;
        bool expenseCommentsEntered = !string.IsNullOrWhiteSpace(Comments);
        
        if (incomeAmountEntered && !incomeCategoryEntered)
        {
            ErrorText = "Bitte eine Einkommens-Kategorie eingeben oder auswählen!";
            return;
        }
        if (!incomeAmountEntered && incomeCategoryEntered)
        {
            ErrorText = "Bitte einen Einkommens-Betrag eingeben!";
            return;
        }
        if (incomeCommentsEntered && (!incomeCategoryEntered || !incomeAmountEntered))
        {
            ErrorText = "Bitte Betrag und Kategorie für das Einkommen eingeben!";
            return;
        }
        
        if (expenseAmountEntered && !expenseCategoryEntered)
        {
            ErrorText = "Bitte eine Ausgaben-Kategorie auswählen!";
            return;
        }
        if (!expenseAmountEntered && expenseCategoryEntered)
        {
            ErrorText = "Bitte einen Ausgaben-Betrag eingeben!";
            return;
        }
        if (expenseCommentsEntered && (!expenseCategoryEntered || !expenseAmountEntered))
        {
            ErrorText = "Bitte Betrag und Kategorie für die Ausgabe eingeben!";
            return;
        }
        
        if (!incomeAmountEntered && !expenseAmountEntered)
        {
            ErrorText = "Bitte entweder ein Einkommen oder eine Ausgabe eingeben.";
            return;
        }
        
        Expense incomeEntry = null;
        Expense expenseEntry = null;

        if (incomeAmountEntered && incomeCategoryEntered)
        {
            if (!decimal.TryParse(Income, out decimal parsedIncome))
            {
                ErrorText = "Nur Dezimalzahlen beim Einkommen erlaubt!";
                return;
            }

            if (!IncomeCategories.Contains(SelectedIncomeCategory))
            {
                IncomeCategories.Add(SelectedIncomeCategory);
            }

            incomeEntry = new Expense
            {
                Input = parsedIncome,
                Output = 0,
                Category = SelectedIncomeCategory,
                Comments = IncomeComments ?? "",
                DateSaved = DateTime.Today,

                IncomeDetails = new Income
                {
                Amount = parsedIncome,
                Category = SelectedIncomeCategory,
                Comments = IncomeComments ?? "",
                DateSaved = DateTime.Today
                }
            };
        }

        if (expenseAmountEntered && expenseCategoryEntered)
        {
            if (!decimal.TryParse(Amount, out decimal parsedAmount))
            {
                ErrorText = "Nur Dezimalzahlen bei der Ausgabe erlaubt!";
                return;
            }
            
            expenseEntry = new Expense
            {
                Input = 0,
                Output = parsedAmount, 
                Category = SelectedCategory,
                Comments = Comments ?? "", 
                DateSaved = DateTime.Now
            };
            
            bool isChildValid = true;
            switch (SelectedCategory)
            {
                case "Vacation": isChildValid = SelectedVacation(expenseEntry); break;
                case "Tech": isChildValid = SelectedTech(expenseEntry); break;
                case "Shopping": isChildValid = SelectedShopping(expenseEntry); break;
                case "SBB": isChildValid = SelectedSbb(expenseEntry); break;
                case "Present": isChildValid = SelectedPresent(expenseEntry); break;
                case "Others": isChildValid = SelectedOthers(expenseEntry); break;
                case "Grocery": isChildValid = SelectedGrocery(expenseEntry); break;
                case "EatOut": isChildValid = SelectedEatOut(expenseEntry); break;
                case "Activity": isChildValid = SelectedActivity(expenseEntry); break;
                default: isChildValid = SelectedAccommodation(expenseEntry); break;
            }
            if (!isChildValid)
            {
                return;
            }
        }
        
        using var db = new ExpenseContext();
        
        if (incomeEntry != null)
        {
            Total += incomeEntry.Input;
            incomeEntry.Total = Total; // KORRIGIERT: Zuweisung repariert
            db.Expenses.Add(incomeEntry);
        }
        if (expenseEntry != null)
        {
            Total -= expenseEntry.Output;
            expenseEntry.Total = Total;
            db.Expenses.Add(expenseEntry);
        }
  
        db.SaveChanges();
        
        // RESET
        Amount = "";
        Comments = "";
        SelectedCategory = null; 
        TripName = "";
        vacFoodAmount = "";
        vacTransportAmount = "";
        vacAccommodationAmount = "";
        vacShoppingAmount = "";
        vacPresentAmount = "";
        TechProductCategory = "";
        SelectedShoppingCategory = null;
        ActivityCategory = "";
        OthersCategory = "";
        
        Income = "";
        SelectedIncomeCategory = null;
        IncomeComments = "";
    }
    
    private bool ClickOkButton()
    {
        return true;
    }

    private bool SelectedVacation(Expense newExpense)
    {
        if (string.IsNullOrWhiteSpace(vacFoodAmount)) vacFoodAmount = "0";
        if (string.IsNullOrWhiteSpace(vacTransportAmount)) vacTransportAmount = "0";
        if (string.IsNullOrWhiteSpace(vacAccommodationAmount)) vacAccommodationAmount = "0";
        if (string.IsNullOrWhiteSpace(vacShoppingAmount)) vacShoppingAmount = "0";
        if (string.IsNullOrWhiteSpace(vacPresentAmount)) vacPresentAmount = "0";
        
        decimal.TryParse(vacFoodAmount, out decimal parsedFood);
        decimal.TryParse(vacTransportAmount, out decimal parsedTransport);
        decimal.TryParse(vacAccommodationAmount, out decimal parsedAccommodation);
        decimal.TryParse(vacShoppingAmount, out decimal parsedShopping);
        decimal.TryParse(vacPresentAmount, out decimal parsedPresent);

        if (string.IsNullOrWhiteSpace(TripName))
        {
            ErrorText = "Bitte einen Trip Name eingeben.";
            return false;
        }
        if (newExpense.Output != (parsedFood + parsedTransport + parsedAccommodation + parsedShopping))
        {
            ErrorText = "Der Gesamtbetrag stimmt nicht überein.";
            return false;
        }
                
        newExpense.VacationDetails = new VacationExpense
        {
            TripName = TripName, 
            Food = parsedFood,
            Transport = parsedTransport,
            Accommodation = parsedAccommodation,
            Comments = newExpense.Comments,
            Shopping = parsedShopping,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedTech(Expense newExpense)
    {
        if (string.IsNullOrWhiteSpace(TechProductCategory))
        {
            ErrorText = "Produkt eingeben!";
            return false;
        }
        newExpense.TechDetails = new TechExpense
        {
            Amount = newExpense.Output, 
            Product = TechProductCategory,
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedShopping(Expense newExpense)
    {
        if (SelectedShoppingCategory == null)
        {
            ErrorText = "Bitte eine Kategorie wählen!";
            return false;
        }
        newExpense.ShoppingDetails = new ShoppingExpense
        {
            Amount = newExpense.Output, 
            Category = SelectedShoppingCategory,
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedSbb(Expense newExpense)
    {
        newExpense.SbbDetails = new SbbExpense
        {
            Amount = newExpense.Output, 
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedPresent(Expense newExpense)
    {
        newExpense.PresentDetails = new PresentExpense
        {
            Amount = newExpense.Output, 
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedOthers(Expense newExpense)
    {
        if (string.IsNullOrWhiteSpace(OthersCategory))
        {
            ErrorText = "Produkt eingeben!";
            return false;
        }
        newExpense.OthersDetails = new OthersExpense()
        {
            Amount = newExpense.Output, 
            Product = OthersCategory,
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedGrocery(Expense newExpense)
    {
        newExpense.GroceryDetails = new GroceryExpense()
        {
            Amount = newExpense.Output, 
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedEatOut(Expense newExpense)
    {
        newExpense.EatOutExpenseDetails = new EatOutExpense()
        {
            Amount = newExpense.Output, 
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedActivity(Expense newExpense)
    {
        if (string.IsNullOrWhiteSpace(ActivityCategory))
        {
            ErrorText = "Aktivität eingeben!";
            return false;
        }
        newExpense.ActivityDetails = new ActivityExpense()
        {
            Amount = newExpense.Output, 
            Activity = ActivityCategory,
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
    
    private bool SelectedAccommodation(Expense newExpense)
    {
        newExpense.AccommodationDetails = new AccommodationExpense()
        {
            Amount = newExpense.Output, 
            Comments = newExpense.Comments,
            DateSaved = DateTime.Now
        };
        return true;
    }
}