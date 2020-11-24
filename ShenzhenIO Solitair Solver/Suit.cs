using System;
using System.Collections.Generic;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public enum Suit {
		None = 0,
		Green,
		Red,
		Black
	}

	public static class SuitUtils {
		public static string GetIdentifier(this Suit suit) {
			switch (suit) {
				case Suit.Green: return "G";
				case Suit.Red: return "R";
				case Suit.Black: return "B";
				case Suit.None:
				default:
					return " ";
			}
		}

		public static byte Hash(this Suit suit) {
			return (byte)suit; //0, 1, 2, or 3
		}
	}
}
