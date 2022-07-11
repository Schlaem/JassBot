using JassBot.Boards;
using JassBot.Cards;
using JassBot.Enumerations;

namespace JassBot.Players {
  public class UserPlayer: Player {
    public UserPlayer(string name) : base(name) {
      // empty...
    }

    public override SwissCard? SelectCard(JassBoard board, HashSet<SwissCard> possibleCards) {
      Console.WriteLine($"Current Card: {board.CurrentCard}");
      Console.WriteLine(string.Empty);
      ShowBlatt(possibleCards);
      Console.WriteLine(string.Empty);

      if (!Cards.Any(x => possibleCards.Any(y => x.Color == y.Color && x.Index == y.Index))) {
        Console.WriteLine("No card possible!");
        return null;
      }

      int index;
      do {
        Console.Write("Select card: ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out index) && 0 <= index && index < Cards.Count && possibleCards.Any(x => x.Color == Cards[index].Color && x.Index == Cards[index].Index)) {
          break;
        } else {
          Console.WriteLine("Invalid input!\n");
        }
      } while (true);

      SwissCard card = Cards[index];
      Cards.Remove(card);

      return card;
    }

    public override SwissColor SelectColor() {
      Console.WriteLine("Select color:");
      foreach (SwissColor item in Enum.GetValues<SwissColor>()) {
        Console.WriteLine($"\t{(int)item} - {item}");
      }
      Console.Write("\nSelection: ");

      return (SwissColor)int.Parse(Console.ReadLine());
    }

    private void ShowBlatt(HashSet<SwissCard> possibleCards) {
      Cards.Sort();

      ConsoleColor foreground = Console.ForegroundColor;
      Console.WriteLine($"Your Cards:");
      for (int n = 0; n < Cards.Count; n++) {
        if (possibleCards.Any(x => x.Color == Cards[n].Color && x.Index == Cards[n].Index)) {
          Console.ForegroundColor = ConsoleColor.Green;
        } else {
          Console.ForegroundColor = foreground;
        }

        Console.WriteLine($"\t{n} - {Cards[n]}");
      }

      Console.ForegroundColor = foreground;
    }
  }
}
