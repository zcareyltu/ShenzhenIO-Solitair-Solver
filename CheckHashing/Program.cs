using ShenzhenIO_Solitair_Solver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckHashing {
	class Program {
		static void Main(string[] args) {
			SortedSet<int> hashes = new SortedSet<int>();
			foreach(Card card in GetAllCards()) {
				int hash = card.Hash();
				if(hash < 0 || hash > 32) {
					throw new IndexOutOfRangeException("Hash was outside of expected range.");
				}
				if (hashes.Contains(hash)) {
					throw new InvalidOperationException("Two cards contain the same hash value.");
				}
				hashes.Add(hash);
			}
			Console.WriteLine("All hashes unique.");
			Console.ReadLine();
		}

		private static IEnumerable<Card> GetAllCards() {
			return GetNumericCards().Concat(GetDragonCards()).Concat(GetEmptyCard()).Concat(GetFlowerCard()).Concat(GetFilledCard());
		}

		private static IEnumerable<Card> GetNumericCards() {
			List<Card> cards = new List<Card>();
			foreach(Suit suit in GetSuits()) {
				for(int i = 1; i <= 9; i++) {
					Card newCard;
					if (Card.TryParse(suit.GetIdentifier() + "" + i, out newCard)) {
						cards.Add(newCard);
					} else {
						throw new Exception("Bad parse.");
					}
				}
			}
			return cards;
		}

		private static IEnumerable<Card> GetDragonCards() {
			List<Card> cards = new List<Card>();
			foreach (Suit suit in GetSuits()) {
				Card newCard;
				if (Card.TryParse(suit.GetIdentifier() + "D", out newCard)) {
					cards.Add(newCard);
				} else {
					throw new Exception("Bad parse.");
				}
			}
			return cards;
		}

		private static IEnumerable<Card> GetEmptyCard() {
			Card card;
			if (!Card.TryParse("-", out card)) {
				throw new Exception("Bad parse.");
			}
			List<Card> cards = new List<Card>();
			cards.Add(card);
			return cards;
		}

		private static IEnumerable<Card> GetFlowerCard() {
			Card card;
			if(!Card.TryParse("F", out card)) {
				throw new Exception("Bad parse.");
			}
			List<Card> cards = new List<Card>();
			cards.Add(card);
			return cards;
		}

		private static IEnumerable<Card> GetFilledCard() {
			Card card = new Card();
			card.Type = CardType.Filled;
			List<Card> cards = new List<Card>();
			cards.Add(card);
			return cards;
		}

		private static IEnumerable<Suit> GetSuits() {
			return ((Suit[])Enum.GetValues(typeof(Suit))).Where((x) => x != Suit.None);
		}
	}
}
