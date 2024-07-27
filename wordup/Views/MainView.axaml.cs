using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace wordup.Views;

public partial class MainView : UserControl
{
    Guess[] guess = new Guess[5];
    int currentGuess = 0;
    string currentGuessStr;
    int LetterCount = 5;
    string currentWord = "ABOUT";
    public MainView()
    {
        this.DataContext = this;
        InitializeComponent();
        SetNextWord();
        for (int i = 0; i < guess.Length; ++i)
        {
            guess[i] = new Guess();
            GuessPanel.Children.Add(guess[i]);
        }
    }

    void SetNextWord()
    {
        int numwords = AllWords.FiveLetters.Length;
        Random r = new Random();
        int wordidx = r.Next(0, numwords);
        currentWord = AllWords.FiveLetters[wordidx];
        currentWord = currentWord.ToUpper();
    }

    public void Letter_Click(object? sender, RoutedEventArgs args)
    {
        if (sender is Button b)
        {
            string btn = b.Content as string;
            if (currentGuessStr == null ||
                currentGuessStr.Length < LetterCount)
            {
                currentGuessStr += btn[0];
                guess[currentGuess].SetLetters(currentGuessStr);
            }
            guess[currentGuess].IsActive = true;
        }
    }

    public void Backspace_Click(object? sender, RoutedEventArgs args)
    {
        if (currentGuessStr != null &&
            currentGuessStr.Length > 0)
        {
            currentGuessStr = currentGuessStr.Remove(currentGuessStr.Length - 1);
            guess[currentGuess].SetLetters(currentGuessStr);
        }
        guess[currentGuess].IsActive = true;
    }

    public void Submit_Click(object? sender, RoutedEventArgs args)
    {
        if (currentGuessStr != null &&
            currentGuessStr.Length == LetterCount)
        {
            int []match = new int[LetterCount];
            for (int i = 0; i < LetterCount; i++)
            {
                if (currentGuessStr[i] == currentWord[i])
                    match[i] = 2;
                else
                    if (currentWord.Contains(currentGuessStr[i]))
                    match[i] = 1;
                else
                    match[i] = 0;
            }

            guess[currentGuess].SetMatches(currentGuessStr, match);
            currentGuess++;
            currentGuessStr = "";
        }
    }
}
