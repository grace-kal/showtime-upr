using System.ComponentModel.DataAnnotations.Schema;
using ShowTimeUpr.Models;

namespace ShowTimeUpr.App.ViewModel;

public class Game
{
    public int Id { get; set; }
    public int Points { get; set; }
    public bool IsGameOver => Board!.Cards!.All(c => c.IsMatched);

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Card? FirstSelectedCard { get; set; }
    public Board? Board { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; }

    public Game()
    {
        Points = 0;
        InitializeGame();
    }

    private void InitializeGame()
    {
        var board = new Board();
        var cards = new List<Card>();
        var uniqueCardNum = (board.SizeX * board.SizeY) / 2;

        for (var i = 1; i <= uniqueCardNum; i++)
        {
            var card1 = new Card
            {
                Id = i,
                ImageUrl = $"c{i}c.png",
                IsMatched = false,
                IsOpen = false
            };

            var card2 = new Card
            {
                Id = i + 100,
                ImageUrl = $"c{i}c.png",
                IsMatched = false,
                IsOpen = false
            };
            cards.Add(card1);
            cards.Add(card2);
        }

        board.Cards = cards;
        board.Shuffle();
        Board = board;
    }

    public string FlipCard(int cardId)
    {
        var selectedCard = Board!.Cards!.FirstOrDefault(c => c.Id.Equals(cardId));

        if (selectedCard == null)
            return "Invalid card!";

        selectedCard.IsOpen = true;

        if (FirstSelectedCard == null)
        {
            FirstSelectedCard = selectedCard;
            return "Select another card.";
        }

        if (FirstSelectedCard.ImageUrl.Equals(selectedCard.ImageUrl))
        {
            FirstSelectedCard.IsMatched = true;
            selectedCard.IsMatched = true;

            Points += 3;
            FirstSelectedCard = null;

            if (IsGameOver)
                return "Game over";

            return "Cards matched!";
        }

        selectedCard.IsOpen = false;
        FirstSelectedCard.IsOpen = false;
        FirstSelectedCard = null;

        Points = Points > 0 ? Points - 1 : Points;
        return "Cards didn't match";
    }
}