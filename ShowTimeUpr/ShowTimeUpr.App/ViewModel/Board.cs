namespace ShowTimeUpr.App.ViewModel;

public class Board
{
    public int Id { get; set; }
    public int SizeX { get; set; } = 3;
    public int SizeY { get; set; } = 4;
    public List<Card>? Cards { get; set; }

    public void Shuffle()
    {
        Random rand = new Random();
        Cards = Cards!.OrderBy(x => rand.Next()).ToList();
    }
}