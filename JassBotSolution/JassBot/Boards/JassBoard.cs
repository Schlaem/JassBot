using JassBot.Cards;
using JassBot.Enumerations;
using JassBot.Players;

namespace JassBot.Boards {
  public abstract class JassBoard {
    private static Random _Random = new();

    public SwissCard? CurrentCard { get; protected set; }

    public abstract Player? CurrentPlayer { get; }

    public int TurnCounter { get; protected set; }

    public List<Player> Players { get; }

    public bool IsFinished { get; protected set; }

    public Player? Winner { get; protected set; }

    protected List<SwissCard> Cards { get; }

    #region CONSTRUCTORS
    public JassBoard(List<Player> players) {
      Cards = GenerateCards();
      Players = players;
    }
    #endregion CONSTRUCTORS

    public abstract void Start();

    public abstract void MakeTurn();

    public SwissCard TakeACard() {
      if (Cards.Count == 0) {
        Console.WriteLine("NEUE KARTEN!");
        Console.WriteLine("Die Karten werden neu gemischt und auf den Ziehstapel gelegt.");
        Console.WriteLine($"Übrig bleiben die Handkarten der Spieler und die aktuelle Karte {CurrentCard}.");

        Cards.AddRange(GenerateCards().Where(x => x.Color != CurrentCard.Color || x.Index != CurrentCard.Index)
          .Where(x => !Players.Any(p => p.Cards.Any(y => x.Color == y.Color && x.Index == y.Index))));
      }

      int index = _Random.Next(Cards.Count);
      SwissCard card = Cards[index];
      Cards.RemoveAt(index);

      return card;
    }

    private static List<SwissCard> GenerateCards() {
      List<SwissCard> cards = new();
      foreach (SwissColor color in Enum.GetValues<SwissColor>()) {
        foreach (SwissIndex index in Enum.GetValues<SwissIndex>()) {
          cards.Add(new SwissCard(color, index));
        }
      }

      return cards;
    }
  }
}
