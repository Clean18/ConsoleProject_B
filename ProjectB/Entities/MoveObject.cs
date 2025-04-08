using ProjectB.Interfaces;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Entities
{
	public class MoveObject : Entity, IInteract
	{
		Position startPos;	// 시작 위치 (첫 위치)
		Position endPos;	// 도착 위치
		Position targetPos; // 이동할 위치

		bool isStart;
		public MoveObject(char sprite,
			Position position,
			Position endPos,
			Direction direction,
			ConsoleColor color = ConsoleColor.White,
			ConsoleColor bgColor = ConsoleColor.Black)
			: base(sprite, position, direction, color, bgColor)
		{
			this.startPos = position;
			this.endPos = endPos;
			this.isStart = true;
			this.targetPos = endPos;
		}

		public void Interact(Player player)
		{
			// 말걸면 배틀
		}

		public void Move()
		{
			// 도착위치에 도착했으면
			if (position == endPos)
			{
				isStart = false;
				targetPos = startPos;
			}
			// 시작위치에 도착했으면
			if (position == startPos)
			{
				isStart = true;
				targetPos = endPos;
			}

			// isStart에 따라 x먼저 y먼저 이동
			if (!isStart)
			{
				if (position.x < targetPos.x) MoveCheck(Direction.Right);	   // position.x++; // 우
				else if (position.x > targetPos.x) MoveCheck(Direction.Left);  // position.x--; // 좌
				else if (position.y < targetPos.y) MoveCheck(Direction.Down);  // position.y++; // 하
				else if (position.y > targetPos.y) MoveCheck(Direction.Up);    // position.y--; // 상
			}
			else
			{
				if (position.y > targetPos.y) MoveCheck(Direction.Up);		   // position.y--; // 상
				else if (position.y < targetPos.y) MoveCheck(Direction.Down);  // position.y++; // 하
				else if (position.x > targetPos.x) MoveCheck(Direction.Left);  // position.x--; // 좌
				else if (position.x < targetPos.x) MoveCheck(Direction.Right); // position.x++; // 우
			}
		}

		void MoveCheck(Direction dir)
		{
			this.direction = dir;
			Position nextPos = this.position + dir;
			List<string> mapData = Data.GetMapData(Game.sceneTable.Peek());

			if ((nextPos.x < 0) || (nextPos.x >= mapData[position.y].Length) || (nextPos.y < 0) || (nextPos.y >= mapData.Count))
				return;

			char tile = mapData[nextPos.y][nextPos.x];
			switch (tile)
			{
				// 제한할 타일
				case '@':
					return;
			}

			if (nextPos == Game.Player.position)
				return;

			position = nextPos;
		}
	}
}
