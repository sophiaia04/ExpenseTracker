using Avalonia;
using Avalonia.Controls;

namespace ExpenseTracker.Views.Controls;

// 1. It MUST inherit from UserControl, not ViewModelBase!
public partial class SmartTextBox : UserControl
{
    public SmartTextBox()
    {
        InitializeComponent();
    }
    // 3. The SubCategory Plug
    public static readonly StyledProperty<string> SubCategoryProperty =
        AvaloniaProperty.Register<SmartTextBox, string>(nameof(SubCategory));

    public string SubCategory
    {
        get => GetValue(SubCategoryProperty);
        set => SetValue(SubCategoryProperty, value);
    }

    // 4. The WatermarkText Plug
    public static readonly StyledProperty<string> WatermarkTextProperty =
        AvaloniaProperty.Register<SmartTextBox, string>(nameof(WatermarkText));

    public string WatermarkText
    {
        get => GetValue(WatermarkTextProperty);
        set => SetValue(WatermarkTextProperty, value);
    }
}