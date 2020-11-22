using System;
using System.Collections.Generic;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public struct Card {

		public bool IsDragon { get => Type == CardType.Dragon; }
		public bool IsFlower { get => Type == CardType.Flower; }
		public bool IsSuit { get => Type == CardType.Suit; }
		public bool IsEmpty { get => Type == CardType.Empty; }
		public bool IsFull { get => Type == CardType.Filled; }

		public CardType Type;
		public Suit Suit;
		public int Value;

		public override string ToString() {
			switch (Type) {
				case CardType.Filled:
					return "=";
				case CardType.Dragon:
					return Suit.GetIdentifier() + "D";
				case CardType.Flower:
					return "F";
				case CardType.Suit:
					return Suit.GetIdentifier() + Value;
				case CardType.Empty:
				default:
					return "-";
			}
		}

		public static bool TryParse(string input, out Card result) {
			result = new Card();
			input = input.Trim().ToUpper();
			if(input == null || input.Length == 0 || input == "-") {
				return true;
			} else {
				if(input == "F") {
					result.Type = CardType.Flower;
					return true;
				}else if(input.Length == 2) {
					if(input[1] == 'D') {
						result.Type = CardType.Dragon;
						return ParseSuit(input[0], out result.Suit);
					} else {
						result.Type = CardType.Suit;
						int n = input[1] - '0';
						if(n >= 1 && n <= 9) {
							result.Value = n;
							return ParseSuit(input[0], out result.Suit);
						}
					}
				}
			}

			return false;
		}

		private static bool ParseSuit(char suit, out Suit result) {
			switch (suit) {
				case 'G':
					result = Suit.Green;
					return true;
				case 'B':
					result = Suit.Black;
					return true;
				case 'R':
					result = Suit.Red;
					return true;
				default:
					result = Suit.None;
					return false;
			}
		}

	}
}
