using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShenzhenIO_Solitair_Solver {
	public class Action {

		public Suit? Collapse;
		public int? Pop;
		public int? PopCardIndex; //used when performing the actions
								  //public int? PopToStackIndex; //What suit stack index the popped card is going
		public Suit? PopSuit;

		public int? FromSlot;//from.slot
		public int? ToSlot; //to.slot

		public int? From; //from.tray / from.count
		public int? FromCardIndex;//
		public int? FromCount;
		public int? To; //to.tray
		public int? ToCardIndex;//

		public int? WaitCount;
		public Suit[] WaitSuitOrder;

		public override string ToString() {
			if(Collapse != null) {
				return "Collapse " + ((Suit)Collapse).ToString();
			}else if(Pop != null) {
				return "Pop tray " + Pop + " card " + PopCardIndex;
			} else {
				string str = "Move from ";
				if(FromSlot != null) {
					str += "slot " + FromSlot;
				} else {
					str += "tray " + From + " card " + FromCardIndex;
				}
				str += " to ";
				if(ToSlot != null) {
					str += " slot " + ToSlot;
				} else {
					str += " tray " + To + " card " + ToCardIndex;
				}
				return str;
			}
		}

		public static Action GetPop(int i, int cardIndex/*, int PopToStackIndex*/, Suit? suit2 = null) {
			Action action = new Action();
			action.Pop = i;
			action.PopCardIndex = cardIndex;
			//action.PopToStackIndex = PopToStackIndex;
			action.PopSuit = suit2;
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

		public static List<Action> ParseWebCommands(string code) {
			List<Action> results = new List<Action>();
			string[] commands = code.TrimEnd(';').Split(';');
			int[] args;
			int?[] args2;
			string[] args3;
			foreach(string cmd in commands) {
				switch (cmd[0]) {
					case 'C':
						Suit suit;
						if (!Card.ParseSuit(cmd[1], out suit)) throw new ArgumentException("Invalid collapse suit.");
						results.Add(Action.GetCollapse(suit));
						break;
					case 'P':
						Suit suit2;
						if (!Card.ParseSuit(cmd[1], out suit2)) throw new ArgumentException("Invalid collapse suit.");
						args = cmd.Substring(2).Split(',').Select(x => int.Parse(x)).ToArray();
						results.Add(Action.GetPop(args[0], args[1], suit2));
						break;
					case 'M':
						args2 = cmd.Substring(1).Split(',').Select<string, int?>(x => { int r; return int.TryParse(x, out r) ? (int?)r : null; }).ToArray();
						Action action = new Action();
						action.From = args2[0];
						action.FromCardIndex = args2[1];
						action.FromSlot = args2[2];
						action.To = args2[3];
						action.ToCardIndex = args2[4];
						action.ToSlot = args2[5];
						results.Add(action);
						break;
					case 'W':
						args3 = cmd.Substring(1).Split(',');
						int arg;
						List<Suit> suits = new List<Suit>();
						if(int.TryParse(args3[0], out arg)) {
							for(int i = 1; i < args3.Length; i++) {
								Suit suit3;
								if(args3[i].Length == 1 && Card.ParseSuit(args3[i][0], out suit3)) {
									suits.Add(suit3);
								} else {
									throw new ArgumentException("Bad suit parse.");
								}
							}
							if(suits.Count <= 0) {
								throw new ArgumentException("Must receive at least 1 suit that was stacked.");
							}
							Action action2 = new Action();
							action2.WaitCount = arg;
							action2.WaitSuitOrder = suits.ToArray();
							results.Add(action2);
						}
						break;
					default:
						throw new InvalidOperationException("Bad command code.");
				}
			}
			return results;
		}
	}

	public enum ActionType {
		Collapse,
		Pop,
		Tray
	}
}
