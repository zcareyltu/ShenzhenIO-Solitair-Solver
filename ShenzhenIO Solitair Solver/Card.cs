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

		public const int HashBits = 6;
		public const int MaxHash = 32;

		public int Hash() {
			switch (Type) {
				case CardType.Empty:
					return 0; //0
				case CardType.Suit:
					return ((int)Suit - 1) * 9 + Value; //1-27
				case CardType.Dragon:
					return 27 + (int)Suit; //28-30
				case CardType.Flower:
					return 31;
				case CardType.Filled:
					return 32;
				default:
					throw new InvalidOperationException("Invalid CardType, can't hash.");
			}
		}

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

		public static bool ParseSuit(char suit, out Suit result) {
			switch (suit) {
				case 'G':
					result = Suit.Green;
					return true;
				case 'W':
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
