using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
	public struct Position
	{
		public int x;
		public int y;

		public Position(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// 포지션값 비교
		public static bool operator ==(Position left, Position right)
		{
			return left.x == right.x && left.y == right.y;
		}

		public static bool operator !=(Position left, Position right)
		{
			return left.x != right.x || left.y != right.y;
		}
	}
}
