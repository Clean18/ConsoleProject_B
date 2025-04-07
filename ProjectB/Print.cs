using ProjectB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    public static class Print
    {
		public static void PrintStart()
		{
			Console.Clear();
			Console.WriteLine("푸키먼");
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("1. 게임 시작");
			Console.WriteLine("2. 종료");

			// 여기서 키입력을 받아야 무한출력 안됨
			while (true)
			{
				var key = Console.ReadKey(true).Key;

				switch (key)
				{
					case ConsoleKey.D1:
						Game.sceneTable.Push(Scene.Field);
						return;

					case ConsoleKey.D2:
						Environment.Exit(0);
						break;
				}
			}
		}
		public static void PrintMap()
		{
			var player = Game.Player;
			var currentMap = Map.GetMapData(Game.sceneTable.Peek());

			for (int y = player.position.y - player.visionY; y <= player.position.y + player.visionY; y++)
			{
				for (int x = player.position.x - player.visionX; x <= player.position.x + player.visionX; x++)
				{
					// 콘솔 출력 위치
					int drawX = x - (player.position.x - player.visionX);
					int drawY = y - (player.position.y - player.visionY);
					Console.SetCursorPosition(drawX, drawY);

					// 맵 범위를 벗어나면 공백
					if (x < 0 || x >= currentMap[0].Length || y < 0 || y >= currentMap.Count)
						Console.Write(' ');
					else
						Console.Write(currentMap[y][x]);
				}
			}
		}

		public static void PrintObject()
		{
			var player = Game.Player;
			var currentObjects = Map.GetMoveObjects(Game.sceneTable.Peek());
			foreach (var moveObj in currentObjects)
			{
				// 시야 범위 안에 있는 오브젝트만 출력
				int dx = moveObj.position.x - player.position.x;
				int dy = moveObj.position.y - player.position.y;

				if (Math.Abs(dx) <= player.visionX && Math.Abs(dy) <= player.visionY)
				{
					int drawX = dx + player.visionX;
					int drawY = dy + player.visionY;

					PrintEntity(moveObj, drawX, drawY);
				}
			}
		}

		public static void PrintPlayer(Player player)
		{
			PrintEntity(player, player.visionX, player.visionY);
		}

		public static void PrintEntity(Entity entity, int cursorX, int cursorY)
		{
			Console.SetCursorPosition(cursorX, cursorY);
			Console.ForegroundColor = entity.color;
			Console.BackgroundColor = entity.bgColor;
			Console.Write(entity.sprite);
			Console.ResetColor();
		}
	}
}
