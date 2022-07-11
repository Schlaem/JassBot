using JassBot.Boards;
using JassBot.Cards;
using JassBot.Enumerations;
using JassBot.Players;

namespace MyApp // Note: actual namespace depends on the project name.
{
  internal class Program {
    static void Main(string[] args) {
      //List<SwissCard> cards = new();
      //foreach (SwissColor color in Enum.GetValues<SwissColor>()) {
      //  foreach (SwissIndex index in Enum.GetValues<SwissIndex>()) {
      //    cards.Add(new SwissCard(color, index));
      //  }
      //}

      //SwissCard currentCard = new(cards[0].Color, cards[0].Index);
      //List<SwissCard> playerACards = new();
      //List<SwissCard> playerBCards = new();
      //for (int i = 1; i <= 12; i++) {
      //  SwissCard card = new(cards[i].Color, cards[i].Index);
      //  if (playerACards.Count < 6) {
      //    playerACards.Add(card);
      //  } else {
      //    playerBCards.Add(card);
      //  }
      //}
      //List<List<SwissCard>> playerCards = new() {
      //  playerACards,
      //  playerBCards
      //};
      //cards = cards.Where(x => x.Color != currentCard.Color || x.Index != currentCard.Index).ToList();
      //cards = cards.Where(x => !playerCards.Any(y => y.Any(z => z.Color == x.Color && z.Index == x.Index))).ToList();

      Console.WriteLine("Willkommen zu JassBot!");
      Console.WriteLine(string.Empty);
      Console.WriteLine("Gewählter Modus: Ciao Sepp!");
      Console.WriteLine(string.Empty);

      List<Player> players = new();
#if DEBUG
      Console.WriteLine("Player 1 name: Bäpf");
      players.Add(new UserPlayer("Bäpf"));
      Console.WriteLine("Player 2 name: Randy");
      players.Add(new RandomPlayer("Randy"));
#else
      Console.Write("Player 1 name: ");
      players.Add(new UserPlayer(Console.ReadLine()));
      Console.Write("Player 2 name: ");
      players.Add(new RandomPlayer(Console.ReadLine()));
#endif
      JassBoard board = new CiaoSeppBoard(players);

      board.Start();

      while (!board.IsFinished) {
        board.MakeTurn();
      }

      Console.WriteLine($"\nPlayer {board.Winner} wins!");
    }
  }
}