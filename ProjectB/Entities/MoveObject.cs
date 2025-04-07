using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Entities
{
	public class MoveObject : Entity
	{
		Position startPos;
		Position endPos;
		Position targetPos;

		bool isStart;
		public MoveObject(char sprite,
			Position position,
			Position endPos,
			ConsoleColor color = ConsoleColor.White,
			ConsoleColor bgColor = ConsoleColor.Black)
			: base(sprite, position, color, bgColor)
		{
			this.startPos = position;
			this.endPos = endPos;
			this.isStart = true;
			this.targetPos = endPos;
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
				if (position.x < targetPos.x) position.x++;      // 우
				else if (position.x > targetPos.x) position.x--; // 좌
				else if (position.y < targetPos.y) position.y++; // 하
				else if (position.y > targetPos.y) position.y--; // 상
			}
			else
			{
				if (position.y > targetPos.y) position.y--;      // 상
				else if (position.y < targetPos.y) position.y++; // 하
				else if (position.x > targetPos.x) position.x--; // 좌
				else if (position.x < targetPos.x) position.x++; // 우
			}
		}
	}
}
