using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Structs
{
	public struct Direction
	{
		public int x;
		public int y;

		public Direction(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static readonly Direction Up = new Direction(0, -1);
		public static readonly Direction Down = new Direction(0, 1);
		public static readonly Direction Left = new Direction(-1, 0);
		public static readonly Direction Right = new Direction(1, 0);
	}
}
