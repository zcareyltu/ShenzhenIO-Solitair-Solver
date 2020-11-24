using System;
using System.Collections.Generic;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public enum CardType {
		Empty = 0,
		Dragon,
		Flower,
		Suit,
		Filled //AKA dragon card flipped over, not empty, but not a specific card
	}

	public static class CardTypeUtils {
		public static byte Hash(this CardType suit) {
			return (byte)suit; //0, 1, 2, 3, or 4
		}
	}
}
