using Avalonia.Controls;
using Avalonia.Interactivity;

namespace wordup.Views;

public partial class MainView : UserControl
{
    Guess []guess = new Guess[5];
    int currentGuess = 0;
    string currentGuessStr;
    public MainView()
    {
        this.DataContext = this;
        InitializeComponent();

        for (int i = 0; i < guess.Length; ++i)
        {
            guess[i] = new Guess();
            GuessPanel.Children.Add(guess[i]);
        }
    }

    public void Button_Click(object? sender, RoutedEventArgs args)
    {
        if (sender is Button b)
        {
            string btn = b.Content as string;
            currentGuessStr += btn[0];
            guess[currentGuess].SetLetters(currentGuessStr);
            guess[currentGuess].IsActive = true;
        }
    }
}
