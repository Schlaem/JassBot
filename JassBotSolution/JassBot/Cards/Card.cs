namespace JassBot.Cards {
  public abstract class Card {
    // empty...
  }

  public abstract class Card<TColor, TIndex>: Card, IComparable<Card<TColor, TIndex>>, IEquatable<Card<TColor, TIndex>> where TColor : Enum where TIndex : Enum {
    public TColor Color { get; }
    public TIndex Index { get; }

    public Card(TColor color, TIndex index) {
      Color = color;
      Index = index;
    }

    public static bool operator ==(Card<TColor, TIndex> a, Card<TColor, TIndex> b) {
      if (ReferenceEquals(a, b)) {
        return true;
      }
      if (a is null || b is null) {
        return false;
      }

      return a.Equals(b);
    }

    public static bool operator !=(Card<TColor, TIndex> a, Card<TColor, TIndex> b) {
      return !(a == b);
    }

    public bool Equals(Card<TColor, TIndex>? other) {
      if (other != null) {
        return Equals(Color, other.Color) && Equals(Index, other.Index);
      }

      return false;
    }

    public override bool Equals(object? obj) {
      return base.Equals(obj as Card<TColor, TIndex>);
    }

    public int CompareTo(Card<TColor, TIndex>? other) {
      if (other == null) {
        return -1;
      } else {
        int compare = Color.CompareTo(other.Color);
        if (compare == 0) {
          compare = Index.CompareTo(other.Index);
        }
        return compare;
      }
    }
  }
}
