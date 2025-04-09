using ProjectB.Entities;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Tiles
{
	class GrassTile2 : Tile
	{
		public GrassTile2(Position position, ConsoleColor bgColor = ConsoleColor.Black)
			: base('W', position, ConsoleColor.DarkGreen, bgColor)
		{
		}

		public override bool IsTrigger(Player player)
		{
			// 위치가 같으면
			return player.position == this.position;
		}

		public override void OnTrigger(Player player)
		{
			// 푸키먼 배틀
		}
	}
}
