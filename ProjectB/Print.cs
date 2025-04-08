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
		// TODO : 맵출력을 항상 하다보니 깜빡임 해결하기 > 더블 버퍼링
		public static void PrintMap(Player player)
		{
			List<string> currentMap = Map.GetMapData(Game.sceneTable.Peek());

			// 플레이어 시야 내의 맵의 부분만 출력
			for (int y = player.position.y - player.visionY; y <= player.position.y + player.visionY; y++)
			{
				for (int x = player.position.x - player.visionX; x <= player.position.x + player.visionX; x++)
				{
					// 콘솔 출력 위치 조정
					int drawX = x - (player.position.x - player.visionX);
					int drawY = y - (player.position.y - player.visionY);

					char tile;

					// 예외처리 맵 범위를 벗어나면 공백
					if ((x < 0) || (x >= currentMap[0].Length) || (y < 0) || (y >= currentMap.Count))
						tile = ' ';
					else
						tile = currentMap[y][x];

					Console.SetCursorPosition(drawX, drawY);
					Console.Write(tile);
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

				// 절대값으로 플레이어 시야 안에 있으면 출력
				if ((Math.Abs(dx) <= player.visionX) && (Math.Abs(dy) <= player.visionY))
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

		// 업캐스팅 출력
		public static void PrintEntity(Entity entity, int cursorX, int cursorY)
		{
			Console.SetCursorPosition(cursorX, cursorY);
			Console.ForegroundColor = entity.color;
			Console.BackgroundColor = entity.bgColor;
			Console.Write(entity.sprite);
			Console.ResetColor();
		}


		public static void PrintAll(Player player)
		{
			// 출력할 맵, 오브젝트, 플레이어(매개변수)
			List<string> currentMap = Map.GetMapData(Game.sceneTable.Peek());
			List<MoveObject> currentObjects = Map.GetMoveObjects(Game.sceneTable.Peek());

			// 플레이어 시야 범위 = 출력 크기
			int width = player.visionX * 2 + 1;
			int height = player.visionY * 2 + 1;

			// 맵 쌓고 오브젝트 쌓고 플레이어 쌓고 한번에 출력
			char[,] buffer = new char[height, width];					// 출력할 배열
			ConsoleColor[,] fgBuffer = new ConsoleColor[height, width]; // 폰트 색
			ConsoleColor[,] bgBuffer = new ConsoleColor[height, width]; // 배경 색

			Console.ResetColor();

			// 출력용 맵 타일 세팅
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					// 출력할 맵의 좌표 계산
					int mapX = player.position.x - player.visionX + x;
					int mapY = player.position.y - player.visionY + y;

					// 맵 범위 초과시
					if ((mapX < 0) || (mapY < 0) || (mapY >= currentMap.Count) || (mapX >= currentMap[0].Length))
					{
						buffer[y, x] = ' ';
						fgBuffer[y, x] = ConsoleColor.Black;
						bgBuffer[y, x] = ConsoleColor.DarkBlue;
					}
					else
					{
						// 맵 타일 출력
						buffer[y, x] = currentMap[mapY][mapX];
						fgBuffer[y, x] = ConsoleColor.White;
						bgBuffer[y, x] = ConsoleColor.Black;
					}
				}
			}

			// 출력용 오브젝트 세팅
			foreach (var obj in currentObjects)
			{
				// 오브젝트 위치 - 플레이어 위치로 상대좌표 구한 후 시야를 더해서 시야 내에 출력
				int dx = obj.position.x - player.position.x + player.visionX;
				int dy = obj.position.y - player.position.y + player.visionY;

				if ((dx >= 0) && (dx < width) && (dy >= 0) && (dy < height))
				{
					buffer[dy, dx] = obj.sprite;
					fgBuffer[dy, dx] = obj.color;
					bgBuffer[dy, dx] = obj.bgColor;
				}
			}

			// 출력용 플레이어 세팅
			// 플레이어 위치는 항상 고정
			int px = player.visionX;
			int py = player.visionY;
			buffer[py, px] = player.sprite;
			fgBuffer[py, px] = player.color;
			bgBuffer[py, px] = player.bgColor;

			// 누적된 데이터 전부 출력 (맵, 무브오브젝트, 플레이어)
			Console.SetCursorPosition(0, 0);
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Console.ForegroundColor = fgBuffer[y, x];
					Console.BackgroundColor = bgBuffer[y, x];
					Console.Write(buffer[y, x]);
				}
				Console.WriteLine();
			}
			Console.ResetColor();
		}
	}
}
