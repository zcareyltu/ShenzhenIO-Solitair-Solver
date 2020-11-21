using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ShenzhenIO_Solitair_Solver {
	public class MouseManager {

		private const int MouseEvent_LeftDown = 0x00000002;
		private const int MouseEvent_LeftUp = 0x00000004;

		private readonly Point Origin; //Grab position of first card, frist tray
		private readonly int TrayDistance; //Add this to Origin.X to get the grab position of Card1, Tray2
		private readonly int CardDistance; //Add this to Origin.Y to get the grab position of Card2, Tray1
		private readonly int TopRowOffset;
		private readonly int FlowerOffset;

		private bool clicked = false;

		public MouseManager(int Tray1X, int Tray8X, int Card1Y, int Card2Y) {
			this.TrayDistance = (Tray8X - Tray1X) / 7;
			this.CardDistance = Card2Y - Card1Y;

			float estimatedCardWidth = (Tray8X - Tray1X) * 0.78947f / 7f;
			this.Origin = new Point(Tray1X + (int)(estimatedCardWidth / 2f), Card1Y + CardDistance / 2);

			float estimatedCardHeight = CardDistance * 7.451612903225806f;
			this.TopRowOffset = (int)(estimatedCardHeight + (Tray8X - Tray1X) * 0.21053 / 7f);

			this.FlowerOffset = (int)(estimatedCardWidth / 3);
		}

		~MouseManager() {
			Dispose();
		}

		public void Dispose() {
			if (clicked) {
				releaseMouse();
			}
		}

		//Tray = 0 is Tray1, Card = 0 is Card1 
		public void MoveTo(int Tray, int Card) {
			if (Tray < 0) throw new ArgumentOutOfRangeException("Tray", "Tray index can't be negative.");
			if (Card < 0) throw new ArgumentOutOfRangeException("Card", "Card index can't be negative.");

			Cursor.Position = Origin + new Size(Tray * TrayDistance, Card * CardDistance);
		}

		//Index starts at 0
		public void MoveToFreeSpace(int Index) {
			if (Index < 0) throw new ArgumentOutOfRangeException("Index", "Tray index can't be negative.");
			if (Index > 2) throw new ArgumentOutOfRangeException("Index", "There are only three free spaces available.");

			Cursor.Position = Origin + new Size(Index * TrayDistance, -TopRowOffset);
		}

		public void MoveToSuitSpace(int Index) {
			if (Index < 0) throw new ArgumentOutOfRangeException("Index", "Tray index can't be negative.");
			if (Index > 2) throw new ArgumentOutOfRangeException("Index", "There are only three suit spaces available.");

			Cursor.Position = Origin + new Size((Index + 5) * TrayDistance, -TopRowOffset);
		}

		public void MoveToFlowerSpace() {
			Cursor.Position = Origin + new Size(4 * TrayDistance - FlowerOffset, -TopRowOffset);
		}

		public void ShortClick(int WaitTime = 500) {
			mouse_event(MouseEvent_LeftDown, Cursor.Position.X, Cursor.Position.Y, 0, 0);
			Thread.Sleep(100);
			mouse_event(MouseEvent_LeftUp, Cursor.Position.X, Cursor.Position.Y, 0, 0);
			clicked = false;
			Thread.Sleep(WaitTime);
		}

		public void ClickAndHold(int WaitTime = 500) {
			clicked = true;
			mouse_event(MouseEvent_LeftDown, Cursor.Position.X, Cursor.Position.Y, 0, 0);
			Thread.Sleep(WaitTime);
		}

		public void Release(int WaitTime = 500) {
			releaseMouse();
			Thread.Sleep(WaitTime);
		}

		private void releaseMouse() {
			mouse_event(MouseEvent_LeftUp, Cursor.Position.X, Cursor.Position.Y, 0, 0);
			clicked = false;
		}

		[DllImport("user32.dll")]
		private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

	}
}
