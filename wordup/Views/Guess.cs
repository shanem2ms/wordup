using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;
using System.Linq;

namespace wordup.Views;

public partial class Guess : UserControl, INotifyPropertyChanged
{
    public List<string> Items {get; private set;} = new List<string>();
    public event PropertyChangedEventHandler PropertyChanged;

    bool isActive = false;
    public bool IsActive {
        get => isActive; 
        set { 
            isActive = value;
            PropertyChanged?.Invoke(this, 
                new PropertyChangedEventArgs(nameof(IsActive)));
        }
    }
    public Guess()
    {
        this.DataContext = this;
        InitializeComponent();
        SetLetters("     ");
    }

    public void SetLetters(string letters)
    {
        Items = 
            letters.ToCharArray().Select(c => c.ToString()).ToList();
        PropertyChanged?.Invoke(this, 
            new PropertyChangedEventArgs(nameof(Items)));
    }
}

public class BoolToBrushConverter : IValueConverter
{
    public IBrush TrueBrush { get; set; } = Brushes.AliceBlue;
    public IBrush FalseBrush { get; set; }= Brushes.AliceBlue;

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? TrueBrush : FalseBrush;
        }
        return FalseBrush; // Default if the value isn't a bool
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // Not needed for this example
    }
}
