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
		// TODO : 돈

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

		public void KeyHandler(ConsoleKey key, List<string> mapData, List<MoveObject> moveObjects)
		{
			switch (key)
			{
				// TODO : 이동 제한 추가하기
				case ConsoleKey.UpArrow:
					Move(Direction.Up, mapData, moveObjects);
					break;

				case ConsoleKey.DownArrow:
					Move(Direction.Down, mapData, moveObjects);
					break;

				case ConsoleKey.LeftArrow:
					Move(Direction.Left, mapData, moveObjects);
					break;

				case ConsoleKey.RightArrow:
					Move(Direction.Right, mapData, moveObjects);
					break;
			}
		}

		void Move(Direction direction, List<string> mapData, List<MoveObject> moveObject)
		{
			// 이동은 못해도 방향전환은 해야함
			this.direction = direction;
			Position nextPos = position + direction;

			// 맵 초과 제한
			if ((nextPos.x < 0) || (nextPos.x >= mapData[position.y].Length ) || (nextPos.y < 0) || (nextPos.y >= mapData.Count))
				return;

			// 맵 체크
			char tile = mapData[nextPos.y][nextPos.x];
			switch (tile)
			{
				// 제한할 타일
				case '@':
					return;
			}

			// 오브젝트 체크
			foreach (var obj in moveObject)
			{
				// 이동할 위치에 오브젝트가 있으면 제한
				if (obj.position == nextPos)
					return;
			}

			position = nextPos;
		}
	}
}
