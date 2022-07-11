using JassBot.Enumerations;

namespace JassBot.Cards {
  public class SwissCard: Card<SwissColor, SwissIndex> {
    private static readonly int ColorMaxLength = Enum.GetNames<SwissColor>().Select(x => x.Length).Max();
    private static readonly int IndexMaxLength = Enum.GetNames<SwissIndex>().Select(x => x.Length).Max();

    public SwissCard(SwissColor color, SwissIndex index) : base(color, index) {
      // empty...
    }

    public override string ToString() {
      string name = Color switch {
        SwissColor.Eichel => "Eichel".PadRight(ColorMaxLength, ' '),
        SwissColor.Rose => "Rose".PadRight(ColorMaxLength, ' '),
        SwissColor.Schelle => "Schelle".PadRight(ColorMaxLength, ' '),
        SwissColor.Schild => "Schild".PadRight(ColorMaxLength, ' '),
        _ => throw new NotImplementedException()
      };

      string index = Index switch {
        SwissIndex.Sechs => "6".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Sieben => "7".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Acht => "8".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Neun => "9".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Zehn => "10".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Unter => "Unter".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Ober => "Ober".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Koenig => "König".PadLeft(IndexMaxLength, ' '),
        SwissIndex.Ass => "Ass".PadLeft(IndexMaxLength, ' '),
        _ => throw new NotImplementedException()
      };

      return string.Join(" ", name, index);
    }
  }
}
