using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
			Direction direction,
			ConsoleColor color = ConsoleColor.White,
			ConsoleColor bgColor = ConsoleColor.Black)
			: base(sprite, position, direction, color, bgColor)
		{
			vision = 4;
			visionX = vision * 2;
			visionY = vision;
		}

		public void KeyHandler(ConsoleKey key, char[,] mapData)
		{
			switch (key)
			{
				// TODO : 이동 제한 추가하기
				case ConsoleKey.UpArrow:
					//this.position.y--;
					Move(Direction.Up, mapData);
					break;

				case ConsoleKey.DownArrow:
					//this.position.y++;
					Move(Direction.Down, mapData);
					break;

				case ConsoleKey.LeftArrow:
					//this.position.x--;
					Move(Direction.Left, mapData);
					break;

				case ConsoleKey.RightArrow:
					//this.position.x++;
					Move(Direction.Right, mapData);
					break;
			}
		}

		void Move(Direction direction, char[,] mapData)
		{
			Position nextPos = position + direction;
			char tile = mapData[nextPos.y, nextPos.x];

			switch (tile)
			{
				// 제한할 타일
				// 맵 데이터만 가져오다보니 @는 이동못하지만
				// O는 맵데이터에 없음
				// 즉 > 버퍼맵을 가지고 있어야됨
				case '@':
				case 'O':
					break;

				// 이동 가능 타일
				case ' ':
					position = nextPos;
					break;
			}
		}
	}
}
