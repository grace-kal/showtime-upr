using System.Diagnostics;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using ShowTimeUpr.App.ViewModel;

namespace ShowTimeUpr.App;

public partial class GamePlayPage : ContentPage
{
    private readonly Stopwatch _stopwatch;
    private readonly Game _game;
    private readonly List<Card> _cards;
    private int _rows;
    private int _columns;
    private int _score;
    private const string CardBackUrl = "card_nine.png";

    public GamePlayPage()
    {
        InitializeComponent();

        //labels on top
        _stopwatch = new Stopwatch();
        _stopwatch.Start();

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            UpdateTime();
            return true; // Return true to keep the timer running
        });

        _game = new Game();
        _score = 0;

        //game grid related
        _cards = _game.Board!.Cards!;
        _rows = _game.Board.SizeX;
        _columns = _game.Board.SizeY;

        SetupGrid();

        CreateCardButton();
    }

    private void UpdateTime()
    {
        if(_game.IsGameOver)
            _stopwatch.Stop();
        var time = _stopwatch.Elapsed;
        TimeLabel.Text = $"Time: {time.Minutes:D2}:{time.Seconds:D2}";
    }

    private void SetupGrid()
    {
        GameGrid.RowDefinitions.Clear();
        GameGrid.ColumnDefinitions.Clear();

        for (var i = 0; i < _rows; i++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        }

        for (var i = 0; i < _columns; i++)
        {
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }
    }

    private void CreateCardButton()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            // Calculate the row and column position
            var row = i / _columns;  // Integer division to get row
            var column = i % _columns;  // Modulo to get column
            var index = i;

            var button = new ImageButton
            {
                Source = _cards[i].IsOpen ? _cards[i].ImageUrl : CardBackUrl,
                Command = new Command(() => CardClicked(index)),
                Aspect = Aspect.AspectFit
            };


            GameGrid.Children.Add(button);
            GameGrid.SetColumn(button, column);
            GameGrid.SetRow(button, row);
        }
    }

    private void CardClicked(int i)
    {
        _game.FlipCard(_cards[i].Id);
        UpdateCardButton();
    }

    private void UpdateCardButton()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            var button = (ImageButton)GameGrid.Children[i];
            // Set the image based on whether the card is open or not
            button.Source = _cards[i].IsOpen ? _cards[i].ImageUrl : CardBackUrl;
        }

        _score = _game.Points;
        ScoreLabel.Text = $"Score: {_score}";
    }
}