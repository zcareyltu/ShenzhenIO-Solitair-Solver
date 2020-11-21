using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ShenzhenIO_Solitair_Solver {
	public struct Card {

		private const char CharEmpty = '-';
		private const char CharDragon = 'D';
		private const char CharFlower = 'F';
		private const char CharRed = 'R';
		private const char CharGreen = 'G';
		private const char CharBlack = 'B';

		private const int NumberMask = 0x0000000F;
		private const int SuitMask = 0x000000F0;
		private const int Dragon = 10;
		private const int Flower = 11;

		public static Card DragonCard => new Card(Dragon);
		public static Card FlowerCard => new Card(Flower);
		public static Card EmptyCard => new Card();

		private short data;

		public int Number {
			get => data & NumberMask;
			set => data = (short)((data & ~NumberMask) | (value & NumberMask));
		}

		public SuitType Suit {
			get => (SuitType)(data & SuitMask);
			set => data = (short)((data & ~SuitMask) | ((int)value & SuitMask)); 
		}

		public bool IsDragon {
			get => Number == Dragon;
			private set => Number = (value ? Dragon : 0);
		}

		public bool IsFlower {
			get => Number == Flower;
			private set => Number = (value ? Flower : 0);
		}

		public bool IsEmpty { get => data == 0; }

		public Card(Card copy) {
			this.data = copy.data;
		}

		private Card(short data) {
			this.data = data;
		}

		public override string ToString() {
			if (IsEmpty) {
				return CharEmpty + " ";
			}else if (IsFlower) {
				return CharFlower + " ";
			} else {
				char suit = ' ';
				if (Suit == SuitType.Red) suit = CharRed;
				else if (Suit == SuitType.Green) suit = CharGreen;
				else if (Suit == SuitType.Black) suit = CharBlack;
				
				if (IsDragon) return suit + "" + CharDragon;
				else return suit + "" + Number;
			}
		}

		public static bool TryParse(string input, out Card card) {
			card = new Card();
			if (input.Length == 1) {
				char c = char.ToUpper(input[0]);
				if (c == CharEmpty) {
					card = Card.EmptyCard;
				} else if (c == CharFlower) {
					card = Card.FlowerCard;
				} else {
					return false;
				}

				return true;
			} else if (input.Length == 2) {
				char suit = char.ToUpper(input[0]);
				char number = char.ToUpper(input[1]);

				if (suit == CharRed) card.Suit = SuitType.Red;
				else if (suit == CharGreen) card.Suit = SuitType.Green;
				else if (suit == CharBlack) card.Suit = SuitType.Black;
				else return false;

				if (number == CharDragon) {
					card.IsDragon = true;
				} else if (number >= '0' && number <= '9') {
					card.Number = number - '0';
				} else {
					return false;
				}

				return true;
			}

			return false;
		}

		public static bool operator ==(Card left, Card right) {
			return left.data == right.data;
		}

		public static bool operator !=(Card left, Card right) {
			return left.data != right.data;
		}
	}

	public enum SuitType {
		Empty = 0,
		Red = 0x10,
		Green = 0x20,
		Black = 0x30
	}

}
