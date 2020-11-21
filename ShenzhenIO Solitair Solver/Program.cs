using System;
using System.Collections.Generic;

namespace ShenzhenIO_Solitair_Solver {
	public class Program {
		static void Main(string[] args) {
			GameState state = GameState.RequestUserInput();
			//Console.WriteLine(state);
			Stack<Move> moves = Solve(state);
			if (moves == null) {
				Console.WriteLine("Unsolveable.");
			} else {
				foreach(Move move in moves) {
					Console.WriteLine(move.ToString());
				}
			}

			Console.Write("Press any key to continue: ");
			Console.Read();
		}

		private static Stack<Move> Solve(GameState state) {

		}

		private static int solve(GameState state, int moveLimit, Stack<Move> moves) {
			if (moveLimit == 0) return -1;
			List<Move> possibleMoves = state.GetPossibleMoves();

		}
	}
}
