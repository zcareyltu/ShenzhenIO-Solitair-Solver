using System;
using System.Collections.Generic;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public struct Move {

		public MoveType FromLocation;
		public int FromIndex; //FreeSpace index when collecting dragons
		public int FromCardIndex; 
		public Card FromCard;
		public MoveType MoveType;
		public MoveType ToLocation;
		public int ToIndex;
		public Card ToCard; //Only used when moving to card stack

		public Move(MoveType FromLocation, int fromIndex, int fromCardIndex, Card From, MoveType Move, MoveType ToLocation, int toIndex, Card toCard) {
			this.FromLocation = FromLocation;
			this.FromIndex = fromIndex;
			this.FromCardIndex = fromCardIndex;
			this.FromCard = From;
			this.MoveType = Move;
			this.ToLocation = ToLocation;
			this.ToIndex = toIndex;
			this.ToCard = Card.EmptyCard;
		}

		/*public static Move MoveToFreeSpace(MoveType OriginalLocation, int fromIndex, int fromCardIndex, Card movedCard) {
			return new Move(OriginalLocation, fromIndex, fromCardIndex, movedCard, MoveType.FreeSpace, MoveType.FreeSpace,);
		}

		public static Move MoveToFlower() {
			return new Move(Card.FlowerCard, MoveType.Flower);
		}

		public static Move MoveToSuitStack(Card movedCard) {
			return new Move(movedCard, MoveType.SuitStack);
		}

		public static Move MoveToOtherCard(Card movedCard, Card baseCard) {
			return new Move(movedCard, MoveType.CardStack, baseCard);
		}*/

		public override string ToString() {
			string str = "Move " + FromCard.ToString() + "to ";
			if (MoveType == MoveType.FreeSpace) {
				str += "a free space.";
			} else if (MoveType == MoveType.SuitStack) {
				str += "the suit stack.";
			} else if (MoveType == MoveType.CardStack) {
				str += ToCard.ToString() + ".";
			} else if (MoveType == MoveType.Flower) {
				str += "the flower space.";
			}else if(MoveType == MoveType.StackDragons) {
				return "Stack the " + FromCard.Suit.ToString() + " dragons.";
			} else {
				return "No move selected.";
			}
			return str;
		}

	}

	public enum MoveType {
		None = default,
		FreeSpace,
		SuitStack,
		CardStack,
		Flower,
		StackDragons
	}
}
