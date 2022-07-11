using JassBot.Boards;
using JassBot.Cards;
using JassBot.Enumerations;

namespace JassBot.Players {
  public abstract class Player {
    public string Name { get; }

    public List<SwissCard> Cards { get; } = new();

    public Player(string name) {
      Name = name;
    }

    public abstract SwissCard? SelectCard(JassBoard board, HashSet<SwissCard> possibleCards);

    public abstract SwissColor SelectColor();

    public override string ToString() {
      return Name;
    }
  }
}
