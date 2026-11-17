using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ExpenseTracker.Views.Controls;

public partial class VacationView : UserControl
{
    public VacationView()
    {
        InitializeComponent();
    }
    
    public static readonly StyledProperty<string> TripNameProperty =
        AvaloniaProperty.Register<VacationView, string>(nameof(TripName));
    
    public static readonly StyledProperty<string> vacFoodAmountProperty =
        AvaloniaProperty.Register<VacationView, string>(nameof(vacFoodAmount));
        
    public static readonly StyledProperty<string> vacTransportAmountProperty =
            AvaloniaProperty.Register<VacationView, string>(nameof(vacTransportAmount));
    
    public static readonly StyledProperty<string> vacAccommodationAmountProperty =
            AvaloniaProperty.Register<VacationView, string>(nameof(vacAccommodationAmountProperty));
        
    public static readonly StyledProperty<string> vacShoppingAmountProperty =
            AvaloniaProperty.Register<VacationView, string>(nameof(vacShoppingAmount));
    
    public static readonly StyledProperty<string> vacPresentAmountProperty =
        AvaloniaProperty.Register<VacationView, string>(nameof(vacPresentAmount));

    public string TripName
    {
        get => GetValue(TripNameProperty);
        set => SetValue(TripNameProperty, value);
    }
    
    public string vacFoodAmount
    {
        get => GetValue(vacFoodAmountProperty);
        set => SetValue(vacFoodAmountProperty, value);
    }
    
    public string vacTransportAmount
    {
        get => GetValue(vacTransportAmountProperty);
        set => SetValue(vacTransportAmountProperty, value);
    }
    
    public string vacAccommodationAmount
    {
        get => GetValue(vacAccommodationAmountProperty);
        set => SetValue(vacAccommodationAmountProperty, value);
    }
    
    public string vacShoppingAmount
    {
        get => GetValue(vacShoppingAmountProperty);
        set => SetValue(vacShoppingAmountProperty, value);
    }
    
    public string vacPresentAmount
    {
        get => GetValue(vacPresentAmountProperty);
        set => SetValue(vacPresentAmountProperty, value);
    }
}