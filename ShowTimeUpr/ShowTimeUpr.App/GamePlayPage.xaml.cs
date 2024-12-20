using System.Diagnostics;
using Microsoft.Maui.Controls;
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
    private const string _cardBack = "card_nine.png";

    public GamePlayPage()
    {
        InitializeComponent();
        //labels on top

        _game = new Game();
        _score = _game.Points;
        _cards = _game.Board.Cards;
        _rows = _game.Board.SizeX;
        _columns = _game.Board.SizeY;
        
        SetupGrid();
        CreateCardButton();
    }

    private void SetupGrid()
    {
        throw new NotImplementedException();
    }
}