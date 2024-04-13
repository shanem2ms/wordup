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
    public class Character
    {
        public Character(char c, int m) 
        {
            C = c.ToString();
            match = m;
        }
        public IBrush MatchBrush
        {
            get
            {
                switch (match)
                {
                    case 0:
                        return Brushes.Gray;
                    case 1:
                        return Brushes.Orange;
                    case 2:
                        return Brushes.Green;
                }

                return Brushes.Transparent;
            }
        }
        int match;
        public string C { get; set; }
    }
    public List<Character> Items {get; private set;} = new List<Character>();
    public event PropertyChangedEventHandler PropertyChanged;

    bool isActive = false;
    public int NSpaces { get; set; } = 5;
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
            letters.ToCharArray().Select(c => new Character(c, -1)).ToList();
        for (int i = Items.Count; i < NSpaces; i++)
        {
            Items.Add(new Character(' ', -1));
        }
        PropertyChanged?.Invoke(this, 
            new PropertyChangedEventArgs(nameof(Items)));
    }

    public void SetMatches(string letters, int[]matches)
    {
        Items =
            new List<Character>();
        for (int i = 0; i < letters.Length; i++)
        {
            Items.Add(new Character(letters[i], matches[i]));
        }
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
