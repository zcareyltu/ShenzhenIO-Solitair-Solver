using System;
using System.Collections.Generic;
using System.Text;

namespace ShenzhenIO_Solitair_Solver {
	public class Action {

		public Suit? Collapse;
		public int? Pop;
		public int? PopCardIndex; //used when performing the actions
		public int? PopToStackIndex; //What suit stack index the popped card is going

		public int? FromSlot;
		public int? ToSlot;

		public int? From;
		public int? FromCardIndex;
		public int? FromCount;
		public int? To;
		public int? ToCardIndex;

		public static Action GetPop(int i, int cardIndex, int PopToStackIndex) {
			Action action = new Action();
			action.Pop = i;
			action.PopCardIndex = cardIndex;
			action.PopToStackIndex = PopToStackIndex;
			return action;
		}

		public static Action GetFrom(int from, int cardIndex, int count, int to, int toCardIndex) {
			Action action = new Action();
			action.From = from;
			action.FromCardIndex = cardIndex;
			action.FromCount = count;
			action.To = to;
			action.ToCardIndex = toCardIndex;
			return action;
		}

		public static Action FromSlotToTray(int fromSlot, int toTray, int toCardIndex) {
			Action action = new Action();
			action.FromSlot = fromSlot;
			action.To = toTray;
			action.ToCardIndex = toCardIndex;
			return action;
		}

		public static Action FromTrayToSlot(int fromTray, int cardIndex, int fromCount, int toSlot) {
			Action action = new Action();
			action.From = fromTray;
			action.FromCardIndex = cardIndex;
			action.FromCount = fromCount;
			action.ToSlot = toSlot;
			return action;
		}

		public static Action GetCollapse(Suit suit) {
			Action action = new Action();
			action.Collapse = suit;
			return action;
		}
	}

	public enum ActionType {
		Collapse,
		Pop,
		Tray
	}
}
