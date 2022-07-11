using JassBot.Boards;
using JassBot.Cards;
using JassBot.Enumerations;

namespace JassBot.Players {
  public class RandomPlayer: Player {
    private static Random _Random = new();

    public RandomPlayer(string name) : base(name) {
      // empty...
    }

    public override SwissCard? SelectCard(JassBoard board, HashSet<SwissCard> possibleCards) {
      IEnumerable<SwissCard> cards = Cards.Where(x => possibleCards.Any(y => x.Color == y.Color && x.Index == y.Index));

      if (cards.ElementAtOrDefault(_Random.Next(cards.Count())) is SwissCard card) {
        Cards.Remove(card);
        return card;
      }

      return null;
    }

    public override SwissColor SelectColor() {
      SwissColor[] values = Enum.GetValues<SwissColor>();
      return values[_Random.Next(values.Length)];
    }
  }
}
