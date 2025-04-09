using ProjectB.Entities;
using System.IO;

namespace ProjectB
{
	public static class Print
	{
		static int startX = 5;
		static int startY = 3;

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
			// 포켓기어
			// 세이브
			// 옵션
			// 종료


			int currentMenu = 1;
			while (Game.sceneTable.Peek() == Scene.Menu)
			{
				int menuX = player.visionX * 2 + 6; // 맵 오른쪽 여백 두고
				int menuY = 0;

				Console.SetCursorPosition(menuX, menuY + 0); Console.WriteLine("┌────────────────┐");
				Console.SetCursorPosition(menuX, menuY + 1); Console.WriteLine("│      메뉴      │");
				Console.SetCursorPosition(menuX, menuY + 2); Console.WriteLine("┢─━─━─━─━─━─━─━─━┪");
				//Console.SetCursorPosition(menuX, menuY + 3);  Console.WriteLine("┃                ┃");
				Console.SetCursorPosition(menuX, menuY + 3); Console.WriteLine(currentMenu == 1 ? "┃   ▶ 포켓몬     ┃" : "┃     포켓몬     ┃");
				Console.SetCursorPosition(menuX, menuY + 4); Console.WriteLine("┃                ┃");
				Console.SetCursorPosition(menuX, menuY + 5); Console.WriteLine(currentMenu == 2 ? "┃   ▶  가방      ┃" : "┃      가방      ┃");
				Console.SetCursorPosition(menuX, menuY + 6); Console.WriteLine("┃                ┃");
				Console.SetCursorPosition(menuX, menuY + 7); Console.WriteLine(currentMenu == 3 ? "┃   ▶  골드      ┃" : "┃      골드      ┃");
				Console.SetCursorPosition(menuX, menuY + 8); Console.WriteLine("┃                ┃");
				Console.SetCursorPosition(menuX, menuY + 9); Console.WriteLine(currentMenu == 4 ? "┃   ▶  종료      ┃" : "┃      종료      ┃");
				Console.SetCursorPosition(menuX, menuY + 10); Console.WriteLine("┖─━─━─━─━─━─━─━─━┚");
				Console.SetCursorPosition(menuX, menuY + 11); Console.WriteLine("  Z:선택  X:취소");
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
								Game.sceneTable.Push(Scene.Party);
								break;

							case 2:
								Console.SetCursorPosition(0, player.visionY * 2 + 1);
								Console.WriteLine("가방씬으로");
								// 가방 씬
								break;

							case 3:
								Console.SetCursorPosition(0, player.visionY * 2 + 1);
								Console.WriteLine("내정보씬으로");
								// 내정보 씬
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

		public static void PrintParty(Player player)
		{
			// TODO : 파티 출력
			List<Pokemon> party = player.Party;

			Console.Clear();

			int partyIndex = 0;

			while (Game.sceneTable.Peek() == Scene.Party)
			{
				// 플레이어가 가진 포켓몬들 전부 출력
				for (int i = 0; i < party.Count; i++)
				{
					Console.SetCursorPosition(startX, startY + i); // 줄마다 위치 이동
					PrintPokemonStatus(party[i], i == partyIndex); // 포켓몬 스탯 출력(선택 표시)
				}
				// [ ] 취소
				Console.SetCursorPosition(startX, startY + party.Count);
				Console.WriteLine(partyIndex == party.Count ? "[▶]  취소" : "[ ]  취소");
				// ------------------------------
				Console.SetCursorPosition(startX, startY + party.Count + 2);
				Console.WriteLine("==============================");
				// Z:선택 X:취소
				//Console.SetCursorPosition(startX, startY + party.Count + 3);
				//Console.WriteLine("  Z:선택   X:취소  ");

				ConsoleKey partyKey = Console.ReadKey(true).Key;

				switch (partyKey)
				{
					case ConsoleKey.UpArrow:
					case ConsoleKey.LeftArrow:
						// i --
						partyIndex--;
						if (partyIndex < 0)
							partyIndex = party.Count;
						break;

					case ConsoleKey.DownArrow:
					case ConsoleKey.RightArrow:
						// i ++
						partyIndex++;
						if (partyIndex > party.Count)
							partyIndex = 0;
						break;

					case ConsoleKey.Z:
						// 선택
						if (partyIndex == party.Count)
						{
							Game.sceneTable.Pop();
							Console.Clear();
						}
						else
						{
							// 포켓몬 파티메뉴 출력
							Game.sceneTable.Push(Scene.PartyMenu);
							int partyMenuIndex = 1;
							while (Game.sceneTable.Peek() == Scene.PartyMenu)
							{
								// [ ] 능력치
								Console.SetCursorPosition(startX, startY + party.Count + 3);
								Console.WriteLine(partyMenuIndex == 1 ? "[▶]  능력치    " : "[ ]  능력치    ");
								// [ ] 순서바꾸기
								Console.SetCursorPosition(startX, startY + party.Count + 4);
								Console.WriteLine(partyMenuIndex == 2 ? "[▶]  순서바꾸기" : "[ ]  순서바꾸기");
								// [ ] 기술
								Console.SetCursorPosition(startX, startY + party.Count + 5);
								Console.WriteLine(partyMenuIndex == 3 ? "[▶]  기술      " : "[ ]  기술      ");
								// [ ] 취소
								Console.SetCursorPosition(startX, startY + party.Count + 6);
								Console.WriteLine(partyMenuIndex == 4 ? "[▶]  취소      " : "[ ]  취소      ");

								ConsoleKey partyMenuKey = Console.ReadKey(true).Key;

								switch (partyMenuKey)
								{
									case ConsoleKey.UpArrow:
									case ConsoleKey.LeftArrow:
										// i --
										partyMenuIndex--;
										if (partyMenuIndex < 1)
											partyMenuIndex = 4;
										break;

									case ConsoleKey.DownArrow:
									case ConsoleKey.RightArrow:
										// i ++
										partyMenuIndex++;
										if (partyMenuIndex > 4)
											partyMenuIndex = 1;
										break;

									case ConsoleKey.Z:
										if (partyMenuIndex == 1)
										{
											// 포켓몬 디테일 정보
											Pokemon pokemon = party[partyIndex];
											Game.sceneTable.Push(Scene.PokemonDetail);
											PrintPokemonDetail(pokemon, startX, startY + party.Count + 3);
										}
										else if (partyMenuIndex == 2)
										{
											// 교체
											Pokemon pokemon = party[partyIndex];
										}
										else if (partyMenuIndex == 3)
										{
											// 가진 기술
											Pokemon pokemon = party[partyIndex];
										}
										else
										{
											Game.sceneTable.Pop();
											Console.Clear();
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
						break;

					case ConsoleKey.X:
					case ConsoleKey.Escape:
						Game.sceneTable.Pop();
						Console.Clear();
						break;
				}
			}
		}

		static void PrintPokemonStatus(Pokemon pokemon, bool isSelected = false)
		{
			// 가지고 있는 포켓몬 정보 출력

			string marker = isSelected ? "[▶]" : "[ ]";
			string genderSymbol = pokemon.Gender == Gender.Male ? "♂" : "♀";
			Console.Write($"{marker} {pokemon.Name,6}\t {genderSymbol} Lv{pokemon.Level,3}   HP:{pokemon.Hp,3}/{pokemon.MaxHp,3}");

			PrintHpBar(pokemon, false);

			// [▶]   이상해씨      ♀ Lv  5   HP:  20/ 20 [■■■■■■■■■■]
			// [ ]   이상해씨      ♀ Lv  5   HP:  20/ 20 [■■■■■■■■■■]
		}

		static void PrintHpBar(Pokemon pokemon, bool addLine)
		{
			int barSize = 10;
			int filled = (int)(((float)pokemon.Hp / pokemon.MaxHp) * barSize);
			int empty = barSize - filled;

			Console.Write("[");
			Console.ForegroundColor = GetHpBarColor(pokemon);
			Console.Write(new string('■', filled));
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write(new string(' ', empty));
			Console.ResetColor();
			Console.Write("]");
			if (addLine) Console.WriteLine();
		}

		static ConsoleColor GetHpBarColor(Pokemon pokemon)
		{
			// 체력 퍼센트에 따라 체력바 색 변경
			float hpPer = (float)pokemon.Hp / pokemon.MaxHp;

			if (hpPer > 0.5f) return ConsoleColor.Green;        // 100 ~ 51
			else if (hpPer > 0.25f) return ConsoleColor.Yellow; // 50 ~ 26
			else return ConsoleColor.Red;                       // 25 ~ 0
		}

		static void PrintPokemonDetail(Pokemon pokemon, int startX, int startY)
		{

			ClearLine(startX, startY, 30, 6);

			int pageIndex = 1;
			
			// 포켓몬 디테일 정보 출력
			while (Game.sceneTable.Peek() == Scene.PokemonDetail)
			{
				int line = startY;
				// 도감번호 레벨 성별
				Console.SetCursorPosition(startX, line++); //1
				Console.WriteLine($"No. {pokemon.Id,3} Lv. {pokemon.Level,3} {(pokemon.Gender == Gender.Male ? "♂" : "♀")}");
				Console.SetCursorPosition(startX, line++); //2
				Console.WriteLine("                                                    ");
				// 이름
				Console.SetCursorPosition(startX, line++); //3
				Console.WriteLine($"{pokemon.Name}");
				Console.SetCursorPosition(startX, line++); //4
				Console.WriteLine("                                                    ");
				// ◀ ■ □ □ ▶
				Console.SetCursorPosition(startX, line++); //5
				Console.Write("◀ ");
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Magenta;
				Console.Write(pageIndex == 1 ? " ■ " : " □ ");
				Console.BackgroundColor = ConsoleColor.Green;
				Console.Write(pageIndex == 2 ? " ■ " : " □ ");
				Console.BackgroundColor = ConsoleColor.Blue;
				Console.Write(pageIndex == 3 ? " ■ " : " □ ");
				Console.ResetColor();
				Console.Write(" ▶");
				Console.WriteLine("                                                    ");
				Console.SetCursorPosition(startX, line++); //7
				Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
				switch (pageIndex)
				{
					case 1:
						ClearLine(startX, line, 30, 8);
						// 1페이지
						// HP 바
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"HP:{pokemon.Hp,3}/{pokemon.MaxHp,3}");
						Console.SetCursorPosition(startX, line++);
						PrintHpBar(pokemon, true);
						// 상태/	보통
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"상태/    {pokemon.State}");
						// 타입/	타입1
						//			타입2
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"타입/    {pokemon.PokeType1}");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"         {(pokemon.PokeType2 == PokeType.None ? "" : pokemon.PokeType2)}");
						break;

					case 2:
						ClearLine(startX, line, 30, 8);
						// 2페이지
						// 기술/	기술명 pp cur/max
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"사용할 수 있는 기술");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"기술명/             PP");
						for (int i = 0; i < 4; i++)
						{
							Console.SetCursorPosition(startX, line++);

							if (i < pokemon.Skills!.Count && pokemon.Skills != null && pokemon.Skills[i] != null)
							{
								// 기술명	25/25
								Skill skill = pokemon.Skills[i];
								Console.WriteLine($"{skill.Name,-12}\t{skill.CurPP,2}/{skill.MaxPP,2}");
							}
							else
							{
								// -		--
								Console.WriteLine($"{"-",-12}\t{"--",2}/{"--",2}");
							}
						}
						break;

					case 3:
						ClearLine(startX, line, 30, 8);
						// 3페이지
						// 능력치	체력/	n
						//			공격/	n
						//			방어/	n
						//			특공/	n
						//			특방/	n
						//			스피드/	n
						var stats = pokemon.PokemonStat;
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine("능력치");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"체력/\t{stats.hp,3}");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"공격/\t{stats.attack,3}");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"방어/\t{stats.defense,3}");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"특수공격/\t{stats.speAttack,3}");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"특수방어/\t{stats.speDefense,3}");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"스피드/\t{stats.speed,3}");
						break;
				}

				ConsoleKey key = Console.ReadKey(true).Key;

				switch(key)
				{
					// pageIndex 1~3
					case ConsoleKey.UpArrow:
					case ConsoleKey.LeftArrow:
						pageIndex--;
						if (pageIndex < 1)
							pageIndex = 3;
						break;

					case ConsoleKey.DownArrow:
					case ConsoleKey.RightArrow:
						pageIndex++;
						if (pageIndex > 3)
							pageIndex = 1;
						break;
				}

			}
		}

		public static void ClearLine(int x, int y, int width, int height)
		{
			Console.SetCursorPosition(x, y);
			for (int i = 0; i < height; i++)
			{
				Console.SetCursorPosition(x, y + i);
				Console.Write(new string(' ', width));
			}
			Console.SetCursorPosition(x, y);
		}

		public static void PrintInventory(Player player)
		{
			// TODO : 인벤토리 출력
		}

		public static void PrintMyInfo(Player player)
		{
			// TODO : 내정보 출력
		}
	}
}
