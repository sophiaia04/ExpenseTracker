using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace ExpenseTracker.Views.Controls;

public partial class SmartComboBox : UserControl
{
    public SmartComboBox()
    {
        InitializeComponent();
    }

    // SubCategory ist und bleibt die Liste (IEnumerable)
    public static readonly StyledProperty<IEnumerable<string>> SubCategoryProperty =
        AvaloniaProperty.Register<SmartComboBox, IEnumerable<string>>(nameof(SubCategory));
    
    // REPARIERT: SelectedCategory ist jetzt ein einzelner string!
    public static readonly StyledProperty<string> SelectedCategoryProperty =
        AvaloniaProperty.Register<SmartComboBox, string>(nameof(SelectedCategory));

    public IEnumerable<string> SubCategory
    {
        get => GetValue(SubCategoryProperty);
        set => SetValue(SubCategoryProperty, value);
    }
    
    // REPARIERT: Rückgabetyp auf string geändert
    public string SelectedCategory
    {
        get => GetValue(SelectedCategoryProperty);
        set => SetValue(SelectedCategoryProperty, value);
    }
}