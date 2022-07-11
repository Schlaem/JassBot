using JassBot.Cards;
using JassBot.Enumerations;
using JassBot.Players;

namespace JassBot.Boards {
  public class CiaoSeppBoard : JassBoard {
    private Direction _Direction = Direction.Ascending;
    private int _PlayerIndex;

    public override Player CurrentPlayer {
      get {
        _PlayerIndex %= Players.Count;
        if (_PlayerIndex == -1) {
          _PlayerIndex += Players.Count;
        }
        return Players[_PlayerIndex];
      }
    }

    public CiaoSeppBoard(List<Player> players) : base(players) {
      // empty...
    }

    public override void Start() {
      foreach (Player player in Players) {
        for (int n = 0; n < 6; n++) {
          player.Cards.Add(TakeACard());
        }
      }

      CurrentCard = TakeACard();
      ApplyCardPreChangeEffect();
      ApplyCardPostChangeEffect();
    }

    public override void MakeTurn() {
      Console.WriteLine($"\nTurn #{++TurnCounter} -> {CurrentPlayer.Name}");
      Console.WriteLine($"#Cards {CurrentPlayer.Cards.Count}");
      HashSet<SwissCard> possibleCards = GetPossibleCards();
      
      for (int n = 0; n <= 1; n++) {
        if (CurrentPlayer.SelectCard(this, possibleCards) is SwissCard selectedCard) {
          Console.WriteLine($"Player {CurrentPlayer.Name} plays {selectedCard}");
          CurrentCard = selectedCard;
          break;
        } else {
          Console.WriteLine($"{CurrentPlayer.Name} takes a card");
          CurrentPlayer.Cards.Add(TakeACard());
        }
      }

      ApplyCardPreChangeEffect();
      IncreasePlayerIndex();
      ApplyCardPostChangeEffect();

      CheckFinish();
    }

    private void IncreasePlayerIndex() {
      switch (_Direction) {
        case Direction.Ascending:
          _PlayerIndex++;
          break;

        case Direction.Descending:
          _PlayerIndex--;
          break;
      }
    }

    private HashSet<SwissCard> GetPossibleCards() {
      HashSet<SwissCard> cards = new();

      if (CurrentCard.Index == SwissIndex.Unter) {
        foreach (SwissIndex index in Enum.GetValues<SwissIndex>()) {
          cards.Add(new SwissCard(CurrentCard.Color, index));
        }
      } else {
        foreach (SwissColor color in Enum.GetValues<SwissColor>()) {
          foreach (SwissIndex index in Enum.GetValues<SwissIndex>()) {
            if (index == SwissIndex.Unter) {
              cards.Add(new SwissCard(color, index));
            } else if (CurrentCard.Color == color || CurrentCard.Index == index) {
              cards.Add(new SwissCard(color, index));
            }
          }
        }
      }

      // Bauern können immer gelegt werden..
      foreach (SwissColor color in Enum.GetValues<SwissColor>()) {
        if (!cards.Any(x => x.Color == color && x.Index == SwissIndex.Unter)) {
          cards.Add(new SwissCard(color, SwissIndex.Unter));
        }
      }

      return cards;
    }

    private void ApplyCardPreChangeEffect() {
      switch (CurrentCard) {
        case SwissCard swissCard:
          ApplySwissCardPreChangeEffect(swissCard);
          break;
      }
    }

    private void ApplySwissCardPreChangeEffect(SwissCard card) {
      switch (card.Index) {
        case SwissIndex.Acht:
          Console.WriteLine("Der nächste Spieler wird ausgelassen...");
          IncreasePlayerIndex();
          break;

        case SwissIndex.Zehn:
          Console.WriteLine("Wir wechseln die Richtung...");
          _Direction = _Direction == Direction.Ascending ? Direction.Descending : Direction.Ascending;
          break;

        case SwissIndex.Unter:
          Console.WriteLine($"{CurrentPlayer.Name} wählt eine Farbe...");
          SwissColor color = CurrentPlayer.SelectColor();
          CurrentCard = new SwissCard(color, SwissIndex.Unter);
          Console.WriteLine($"{CurrentPlayer.Name} wählt {color}!");
          break;

        case SwissIndex.Ass:
          break;
      }
    }

    private void ApplyCardPostChangeEffect() {
      switch (CurrentCard) {
        case SwissCard swissCard:
          ApplySwissCardPostChangeEffect(swissCard);
          break;
      }
    }

    private void ApplySwissCardPostChangeEffect(SwissCard card) {
      switch (card.Index) {
        case SwissIndex.Sieben:
          Console.WriteLine($"{CurrentPlayer} zieht zwei Karten!");
          for (int n = 0; n < 2; n++) {
            CurrentPlayer.Cards.Add(TakeACard());
          }
          break;
      }
    }

    private void CheckFinish() {
      Winner = Players.FirstOrDefault(p => p.Cards.Count == 0);
      IsFinished = Winner != null;
    }

    #region ENUMERATIONS
    public enum Direction { Ascending, Descending };
    #endregion ENUMERATIONS
  }
}
