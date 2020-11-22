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
}
