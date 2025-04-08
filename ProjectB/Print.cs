using ProjectB.Entities;

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
						Console.Clear();
						return;

					case ConsoleKey.D2:
						Environment.Exit(0);
						break;
				}
			}
		}

		public static void PrintAll(Player player)
		{
			// 출력할 맵, 오브젝트, 플레이어(매개변수)
			List<string> currentMap = Data.GetMapData(Game.sceneTable.Peek());
			List<Entity> currentObjects = Data.GetEntitiesData(Game.sceneTable.Peek());

			// 일정 여백 띄우기
			// TODO : 일정 여백 띄운 만큼 채우기
			int startX = 5;
			int startY = 3;

			// 플레이어 시야 범위 = 출력 크기
			int width = player.visionX * 2 + 1;
			int height = player.visionY * 2 + 1;

			// 맵 쌓고 오브젝트 쌓고 플레이어 쌓고 한번에 출력
			char[,] buffer = new char[height, width];                   // 출력할 배열
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
						fgBuffer[y, x] = ConsoleColor.Black;
						bgBuffer[y, x] = ConsoleColor.DarkBlue;
						buffer[y, x] = ' ';
					}
					else
					{
						// 맵 타일 출력
						fgBuffer[y, x] = ConsoleColor.White;
						bgBuffer[y, x] = ConsoleColor.Black;
						buffer[y, x] = currentMap[mapY][mapX];
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
					fgBuffer[dy, dx] = obj.color;
					bgBuffer[dy, dx] = obj.bgColor;
					buffer[dy, dx] = obj.sprite;
				}
			}

			// 출력용 플레이어 세팅
			// 플레이어 위치는 항상 고정
			int px = player.visionX;
			int py = player.visionY;
			fgBuffer[py, px] = player.color;
			bgBuffer[py, px] = player.bgColor;
			buffer[py, px] = player.sprite;

			// 누적된 데이터 전부 출력 (맵, 무브오브젝트, 플레이어)
			Console.SetCursorPosition(0, 0);
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Console.SetCursorPosition(startX + x, startY + y);
					Console.ForegroundColor = fgBuffer[y, x];
					Console.BackgroundColor = bgBuffer[y, x];
					Console.Write(buffer[y, x]);
				}
				Console.WriteLine();
			}
			Console.ResetColor();
		}

		public static void PrintMenu(Player player)
		{
			// TODO : 메뉴 리스트 / 씬추가해야됨
			// 도감
			// 현재 파티
			// 가방
			// 포켓기어
			// 골드
			// 세이브
			// 옵션
			// 나가기
			// 종료


			int currentMenu = 1;
			while (Game.sceneTable.Peek() == Scene.Menu)
			{
				int menuX = player.visionX * 2 + 6; // 맵 오른쪽 여백 두고
				int menuY = 3;

				Console.SetCursorPosition(menuX, menuY + 0);  Console.WriteLine("==================");
				Console.SetCursorPosition(menuX, menuY + 1);  Console.WriteLine("====== 메뉴 ======");
				Console.SetCursorPosition(menuX, menuY + 2);  Console.WriteLine("==================");
				Console.SetCursorPosition(menuX, menuY + 3);  Console.WriteLine("=                =");
				Console.SetCursorPosition(menuX, menuY + 4);  Console.WriteLine(currentMenu == 1 ? "=   ▶ 포켓몬     =" : "=     포켓몬     =");
				Console.SetCursorPosition(menuX, menuY + 5);  Console.WriteLine("=                =");
				Console.SetCursorPosition(menuX, menuY + 6);  Console.WriteLine(currentMenu == 2 ? "=   ▶  가방      =" : "=      가방      =");
				Console.SetCursorPosition(menuX, menuY + 7);  Console.WriteLine("=                =");
				Console.SetCursorPosition(menuX, menuY + 8);  Console.WriteLine(currentMenu == 3 ? "=   ▶  골드      =" : "=      골드      =");
				Console.SetCursorPosition(menuX, menuY + 9);  Console.WriteLine("=                =");
				Console.SetCursorPosition(menuX, menuY + 10); Console.WriteLine(currentMenu == 4 ? "=   ▶  종료      =" : "=      종료      =");
				Console.SetCursorPosition(menuX, menuY + 11); Console.WriteLine("==================");
				Console.SetCursorPosition(menuX, menuY + 12);

				ConsoleKey key = Console.ReadKey(true).Key;
				switch (key)
				{
					case ConsoleKey.UpArrow:
					case ConsoleKey.LeftArrow:
						currentMenu--;
						if (currentMenu < 1)
							currentMenu = 4;
						break;

					case ConsoleKey.DownArrow:
					case ConsoleKey.RightArrow:
						currentMenu++;
						if (currentMenu > 4)
							currentMenu = 1;
						break;

					case ConsoleKey.Z:
						switch (currentMenu)
						{
							case 1:
								// 내 파티 씬
								Console.SetCursorPosition(0, player.visionY * 2 + 1);
								Console.WriteLine("내파티씬으로");
								break;

							case 2:
								Console.SetCursorPosition(0, player.visionY * 2 + 1);
								Console.WriteLine("가방씬으로");
								// 가방 씬
								break;

							case 3:
								Console.SetCursorPosition(0, player.visionY * 2 + 1);
								Console.WriteLine("골드배지씬으로");
								// 골드 배지 씬
								break;

							case 4:
								// 종료
								Game.sceneTable.Pop();
								Console.Clear();
								break;

						}
						break;

					case ConsoleKey.X:
					case ConsoleKey.Escape:
						Game.sceneTable.Pop();
						Console.Clear();
						break;
				}
			}
		}
	}
}
