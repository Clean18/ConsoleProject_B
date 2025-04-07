using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Entities
{
	public class Player : Entity
	{
		int vision;			// 플레이어 시야
		public int visionX; // 플레이어 x 시야
		public int visionY; // 플레이어 y 시야

		public Player(char sprite,
			Position position,
			ConsoleColor color = ConsoleColor.White,
			ConsoleColor bgColor = ConsoleColor.Black)
			: base(sprite, position, color, bgColor)
		{
			vision = 4;
			visionX = vision * 2;
			visionY = vision;
		}
	}
}
