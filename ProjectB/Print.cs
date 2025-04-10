using ProjectB.Entities;
using ProjectB.Structs;

namespace ProjectB
{
	public static class Print
	{
		public const int startX = 5;    // 화면 여백
		public const int startY = 3;    // 화면 여백
		public const int battleStartY = 12; // 배틀 출력시 시작 Y
		public static Position battleLogPos = new Position(25, 14);

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
						Game.sceneTable.Push(Scene.Field);  // 필드씬 전환
						Game.currentMap = Map.Field;   // 필드맵으로 이동
						Game.Player.SetCurrentField();      // 현재 맵 데이터 필드에 보관
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
			List<string> currentMap = Data.GetMapData(Game.currentMap);
			List<Entity> currentObjects = Data.GetEntitiesData(Game.currentMap);
			List<Tile> currentTiles = Data.GetTilesData(Game.currentMap);

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

			foreach (var tile in currentTiles)
			{
				int dx = tile.position.x - player.position.x + player.visionX;
				int dy = tile.position.y - player.position.y + player.visionY;

				if ((dx >= 0) && (dx < width) && (dy >= 0) && (dy < height))
				{
					fgBuffer[dy, dx] = tile.color;
					bgBuffer[dy, dx] = tile.bgColor;
					buffer[dy, dx] = tile.sprite;
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
							PrintPartyMenu(partyIndex, party.Count);

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

		static void PrintPartyMenu(int partyIndex, int partyCount)
		{
			int partyMenuIndex = 1;
			var party = Game.Player.Party;
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
							// TODO : 교체
							Pokemon pokemon = party[partyIndex];
							//Game.sceneTable.Push(Scene.PokemonHasSkill);
						}
						else if (partyMenuIndex == 3)
						{
							// 가진 기술
							Game.sceneTable.Push(Scene.PokemonHasSkill);
							PrintPokemonHasSkill(partyIndex, startX, startY + party.Count + 3); // index를 넘겨야 변경가능
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

		static void PrintPokemonStatus(Pokemon pokemon, bool isSelected = false)
		{
			// 가지고 있는 포켓몬 정보 출력
			string marker = isSelected ? "[▶]" : "[ ]";
			string genderSymbol = pokemon.Gender == Gender.Male ? "♂" : "♀";
			Console.Write($"{marker} {pokemon.Name,-6}\t{genderSymbol} Lv{pokemon.Level,3}   HP:{pokemon.Hp,3}/{pokemon.MaxHp,3}");

			PrintHpBar(pokemon, false);

			// [▶]   이상해씨      ♀ Lv  5   HP:  20/ 20 [■■■■■■■■■■]
			// [ ]   이상해씨      ♀ Lv  5   HP:  20/ 20 [■■■■■■■■■■]
		}

		static void PrintHpBar(Pokemon pokemon, bool addLine)
		{
			// 체력이 음수가 되면 터짐
			if (pokemon.Hp <= 0)
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("[□□□□□□□□□□]");
				Console.ResetColor();
				if (addLine) Console.WriteLine();
				return;
			}

			int barSize = 10;
			int filled = (int)(((float)pokemon.Hp / pokemon.MaxHp) * barSize);
			int empty = barSize - filled;

			Console.Write("[");
			Console.ForegroundColor = GetHpBarColor(pokemon);
			Console.Write(new string('■', filled));
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(new string('□', empty));
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
						// 능력치
						var stats = pokemon.PokemonStat;
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine("능력치");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"체력/\t{stats.hp,3}");                //			공격/	n
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"공격/\t{stats.attack,3}");
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"방어/\t{stats.defense,3}");           //			방어/	n
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"특수공격/\t{stats.speAttack,3}");     //			특공/	n
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"특수방어/\t{stats.speDefense,3}");    //			특방/	n
						Console.SetCursorPosition(startX, line++);
						Console.WriteLine($"스피드/\t{stats.speed,3}");           //			스피드/	n
						break;
				}

				ConsoleKey key = Console.ReadKey(true).Key;

				switch (key)
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
					case ConsoleKey.X:
					case ConsoleKey.Escape:
						Game.sceneTable.Pop();
						ClearLine(startX, startY, 30, 14); // ==== 줄 밑으로 전부 지우기
						break;
				}
			}
		}

		public static void ClearLine(int startX, int startY, int width, int height)
		{
			Console.SetCursorPosition(startX, startY);
			for (int i = 0; i < height; i++)
			{
				Console.SetCursorPosition(startX, startY + i);
				Console.Write(new string(' ', width));
			}
			Console.SetCursorPosition(startX, startY);
		}

		static void PrintPokemonHasSkill(int partyIndex, int startX, int startY)
		{
			ClearLine(startX, startY, 30, 6);
			var pokemon = Game.Player.Party[partyIndex];
			int skillIndex = 0;

			// 포켓몬이 가진 스킬 정보 출력
			while (Game.sceneTable.Peek() == Scene.PokemonHasSkill)
			{
				int line = startY;
				Console.SetCursorPosition(startX, line++); //1
				Console.WriteLine($"{pokemon.Name} :Lv {pokemon.Level}");
				Console.SetCursorPosition(startX, line++); //2
				Console.WriteLine();
				for (int i = 0; i < 4; i++)
				{
					Console.SetCursorPosition(startX, line++); //6

					if (i < pokemon.Skills!.Count && pokemon.Skills != null && pokemon.Skills[i] != null)
					{
						// 기술명	25/25
						Skill skill = pokemon.Skills[i];
						Console.WriteLine($"{(i == skillIndex ? "[▶]" : "[ ]")} {skill.Name,-6}\t{skill.CurPP,2}/{skill.MaxPP,2}");
					}
					else
					{
						// -		--
						Console.WriteLine($"    {"-",-6}\t{"--",2}/{"--",2}");
					}
				}
				Console.SetCursorPosition(startX, line++);
				Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━");
				Console.SetCursorPosition(startX, line++);
				Console.WriteLine("Z:선택 X:나가기");
				ConsoleKey key = Console.ReadKey(true).Key;

				switch (key)
				{
					case ConsoleKey.UpArrow:
					case ConsoleKey.LeftArrow:
						skillIndex--;
						if (skillIndex < 0)
							skillIndex = pokemon.Skills!.Count - 1;
						break;

					case ConsoleKey.DownArrow:
					case ConsoleKey.RightArrow:
						skillIndex++;
						if (skillIndex >= pokemon.Skills!.Count)
							skillIndex = 0;
						break;

					case ConsoleKey.Z:
						// Z 누르면 스킬 설명
						// 타입/ 노말 물리 or 특수
						// 위력/ 123
						// 스킬 설명
						Game.sceneTable.Push(Scene.PokemonSkillInfo);
						PrintPokemonSkillInfo(partyIndex, skillIndex, startX, line);
						ClearLine(startX, startY, 30, 14);
						break;

					case ConsoleKey.X:
					case ConsoleKey.Escape:
						Game.sceneTable.Pop();
						ClearLine(startX, startY, 30, 14); // ==== 줄 밑으로 전부 지우기
						break;
				}
			}
		}

		static void PrintPokemonSkillInfo(int partyIndex, int skillIndex, int startX, int startY)
		{
			var pokemon = Game.Player.Party[partyIndex];
			int changeIndex = -1; // 0번스킬
			bool isChange = false;
			int upLine = 6;
			int changeY = startY - upLine + changeIndex; // 0번스킬 위치

			while (Game.sceneTable.Peek() == Scene.PokemonSkillInfo)
			{
				var curSkill = isChange == false ? pokemon.Skills![skillIndex] : pokemon.Skills?[changeIndex];
				int line = startY;

				Console.SetCursorPosition(startX, line++);
				Console.WriteLine($"타입/ {curSkill!.PokeType} / {(curSkill.SkillType == SkillType.Physical ? "물리" : "특수")}");
				Console.SetCursorPosition(startX, line++);
				Console.WriteLine($"위력/ {curSkill.Damage}");
				Console.SetCursorPosition(startX, line++);
				Console.WriteLine($"{curSkill.Description}");
				Console.SetCursorPosition(startX, line++);
				Console.SetCursorPosition(startX, line++);
				Console.WriteLine($"{(isChange == false ? "Z : 바꾸기 X:나가기" : "바꾸시겠습니까?")}"); // isChange 에 따라 설명변경
				Console.SetCursorPosition(startX, line++);
				Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━");
				ConsoleKey skillInfoKey = Console.ReadKey(true).Key;

				switch (skillInfoKey)
				{
					case ConsoleKey.UpArrow:
					case ConsoleKey.LeftArrow:
						if (isChange)
						{
							ClearLine(startX, startY, 100, 7);
							Console.SetCursorPosition(startX, changeY);
							Console.Write("[ ]");

							changeIndex--;

							if (changeIndex < 0)
								changeIndex = pokemon.Skills!.Count - 1;

							if (changeIndex == skillIndex) // 선택된 기술 넘기기
							{
								changeIndex--;
								if (changeIndex < 0)
									changeIndex = pokemon.Skills!.Count - 1;
							}

							changeY = startY - upLine + changeIndex;
							Console.SetCursorPosition(startX, changeY);
							Console.Write("[▷]");
						}
						break;

					case ConsoleKey.DownArrow:
					case ConsoleKey.RightArrow:
						if (isChange)
						{
							ClearLine(startX, startY, 100, 7);
							Console.SetCursorPosition(startX, changeY);
							Console.Write("[ ]");

							changeIndex++;

							if (changeIndex >= pokemon.Skills!.Count)
								changeIndex = 0;

							if (changeIndex == skillIndex)
							{
								changeIndex++;
								if (changeIndex >= pokemon.Skills.Count)
									changeIndex = 0;
							}

							changeY = startY - upLine + changeIndex;
							Console.SetCursorPosition(startX, changeY);
							Console.Write("[▷]");
						}
						break;

					case ConsoleKey.Z:
						// 기술 위치 바꾸기
						if (isChange)
						{
							// changeIndex를 skillIndex로
							Game.Player.Party[partyIndex].SkillChange(changeIndex, skillIndex);
							ClearLine(startX, startY, 100, 7);
							Game.sceneTable.Pop();
						}
						if (!isChange)
						{
							if (pokemon.Skills!.Count > 1)
							{
								isChange = true;
								ClearLine(startX, startY, 100, 7);
								for (int i = 0; i < pokemon.Skills.Count; i++)
								{
									if (i != skillIndex)
									{
										changeIndex = i;
										break;
									}
								}
								changeY = startY - upLine + changeIndex;
								Console.SetCursorPosition(startX, changeY);
								Console.Write("[▷]");
							}
							else if (pokemon.Skills.Count <= 1)
							{
								Console.SetCursorPosition(startX, line++);
								Console.WriteLine("교체할 기술이 없습니다.");
							}
						}
						break;

					case ConsoleKey.X:
						Game.sceneTable.Pop();
						ClearLine(startX, startY, 100, 7);
						break;
				}
			}
		}

		public static void PrintInventory(Player player)
		{
			// TODO : 인벤토리 출력
		}

		public static void PrintMyInfo(Player player)
		{
			// TODO : 내정보 출력
		}

		public static void PrintWildBattleIntro(Player player)
		{
			// TODO : 트레이너, 필드인카운트에 딸 ㅏ다름
			// 전부 출력 후
			Pokemon myPoke = player.MyFirstPoke();
			if (myPoke == null)
				return;

			for (int y = 0; y < Console.WindowHeight; y++)
			{
				Console.SetCursorPosition(0, y);
				Console.BackgroundColor = ConsoleColor.Black;
				Console.Write(new string(' ', Console.WindowWidth));
				Thread.Sleep(20);
			}
			Console.BackgroundColor = ConsoleColor.Gray;
			Console.Clear();
			Thread.Sleep(100);

			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.Clear();
			Thread.Sleep(100);

			Console.BackgroundColor = ConsoleColor.Black;
			Console.Clear();
			Thread.Sleep(100);

			// 여기서 텍스트 출력
			Console.Clear();

			// 상대방 출력
			Pokemon enemyPoke = Battle.enemyPokemon!;
			PrintEnemyPokemon(enemyPoke);

			// 내 푸키먼 출력
			int nextLine = PrintMyPokemon(myPoke);
			PrintBattleTextLine();
			Console.SetCursorPosition(1, nextLine + 1); //13
			string text = $"앗! 야생의 {Battle.enemyPokemon!.Name} 이(가) 튀어나왔다!";
			foreach (char c in text)
			{
				Console.Write(c);
				Thread.Sleep(100);
			}
			Console.SetCursorPosition(1, nextLine + 2); //14
			string text2 = $"가랏! {myPoke.Name}!";
			foreach (char c in text2)
			{
				Console.Write(c);
				Thread.Sleep(100);
			}
			Thread.Sleep(1000);

			// Battle 클래스에 정보 저장
			// 상대 푸키먼 정보는 타일에서 저장
			Battle.myPokemon = myPoke;
			Battle.myParty = player.Party;

			Game.sceneTable.Pop();  // BattleIntro Pop
			Game.sceneTable.Push(Scene.Battle); // Battle Push > 렌더에서 PrintBattle 함수 실행
		}

		public static int PrintEnemyPokemon(Pokemon pokemon)
		{
			Console.SetCursorPosition(startX, startY);
			Console.Write($"┃ {pokemon.Name} {(pokemon.Gender == Gender.Male ? "♂" : "♀")} Lv.{pokemon.Level}");
			Console.SetCursorPosition(startX, startY + 1);
			Console.Write("┃ HP: ");
			PrintHpBar(pokemon, false);
			Console.SetCursorPosition(startX, startY + 2);
			Console.WriteLine("┗━━━━━━━━━━━━━━━━━▶");
			// 다음줄은 y4
			return startY + 3;
		}

		public static int PrintMyPokemon(Pokemon pokemon)
		{
			int line = 5;

			Console.SetCursorPosition(startX, startY + line + 0);
			Console.Write($" {pokemon.Name} {(pokemon.Gender == Gender.Male ? "♂" : "♀")} Lv.{pokemon.Level}");
			Console.SetCursorPosition(startX, startY + line + 1);
			Console.Write(" HP: ");
			PrintHpBar(pokemon, false);
			Console.Write(" ┃");
			Console.SetCursorPosition(startX + 6, startY + line + 2);
			Console.Write($"{pokemon.Hp,3} / {pokemon.MaxHp,3}   ┃");
			Console.SetCursorPosition(startX, startY + line + 3);
			Console.WriteLine("◀━━━━━━━━━━━━━━━━━┛");
			// 다음줄은 y12
			return startY + line + 4;
		}

		public static void PrintTrainerIntro(Player player)
		{
			// TODO : 트레이너 상대 배틀인트로 출력

			Game.sceneTable.Pop();  // BattleIntro Pop
			Game.sceneTable.Push(Scene.Battle); // Battle Push > 렌더에서 PrintBattle 함수 실행
		}

		public static void PrintBattle(Player player)
		{
			// TODO : 배틀
			//ClearLine(0, battleStartY + 1, 50, 2);
			ClearBattleText();

			//string text1 = "┏━━━━━━━━━━━━━━━━━━━━━━━━━━┓";
			//string text2 = "┃ [▶] 싸우다  [▶] 가방     ┃";
			//string text3 = "┃ [▶] 포켓몬  [▶] 도망치다 ┃";
			//string text4 = "┗━━━━━━━━━━━━━━━━━━━━━━━━━━┛";

			switch (Battle.state)
			{
				case BattleState.Intro:
					Battle.state = BattleState.PlayerTurn;
					//Battle.state = BattleState.EnemyTurn;
					//Battle.state = BattleState.SpeedCheck;
					break;
				case BattleState.SpeedCheck:
					Battle.SpeedCheck();
					break;

				case BattleState.PlayerTurn:
					PrintPlayerTurn(player);
					break;

				case BattleState.EnemyTurn:
					// 상대가 먼저 공격
					Battle.EnemyAction();
					break;

				case BattleState.PlayerSkill:
					// 보유 기술 출력
					PrintPlayerBattle();
					break;

				case BattleState.PlayerInventory:
					break;

				case BattleState.PlayerPokemon:
					break;

				case BattleState.PlayerRun:
					break;

				case BattleState.Win:
					// TODO : 승리시
					Battle.state = BattleState.Intro;
					Game.sceneTable.Pop();
					break;

				case BattleState.Lose:
					break;
			}
		}

		static void PrintPlayerTurn(Player player)
		{
			int menuIndex = 1;
			while (Battle.state == BattleState.PlayerTurn)
			{
				PrintBattleTextLine();
				Console.SetCursorPosition(1, battleStartY + 4); // battleStartY = 12
				Console.WriteLine("┏━━━━━━━━━━━━━━━━━━━━━━━━━━┓");
				Console.SetCursorPosition(1, battleStartY + 5);
				Console.WriteLine($"┃ [{(menuIndex == 1 ? "▶" : " ")}] 싸우다  [{(menuIndex == 2 ? "▶" : " ")}] 가방     ┃");
				Console.SetCursorPosition(1, battleStartY + 6);
				Console.WriteLine($"┃ [{(menuIndex == 3 ? "▶" : " ")}] 포켓몬  [{(menuIndex == 4 ? "▶" : " ")}] 도망치다 ┃");
				Console.SetCursorPosition(1, battleStartY + 7); // 19
				Console.WriteLine("┗━━━━━━━━━━━━━━━━━━━━━━━━━━┛");

				ConsoleKey key = Console.ReadKey(true).Key;

				switch (key)
				{
					case ConsoleKey.UpArrow:
						switch (menuIndex)
						{
							case 3: menuIndex = 1; break;
							case 4: menuIndex = 2; break;
						}
						break;

					case ConsoleKey.DownArrow:
						switch (menuIndex)
						{
							case 1: menuIndex = 3; break;
							case 2: menuIndex = 4; break;
						}
						break;

					case ConsoleKey.LeftArrow:
						switch (menuIndex)
						{
							case 2: menuIndex = 1; break;
							case 4: menuIndex = 3; break;
						}
						break;

					case ConsoleKey.RightArrow:
						switch (menuIndex)
						{
							case 1: menuIndex = 2; break;
							case 3: menuIndex = 4; break;
						}
						break;

					case ConsoleKey.Z:
						switch (menuIndex)
						{
							case 1: // 싸우다
								Battle.state = BattleState.PlayerSkill;
								break;

							case 2: // 가방
								Battle.state = BattleState.PlayerInventory;
								break;

							case 3: // 포켓몬
								Battle.state = BattleState.PlayerPokemon;
								break;

							case 4: // 도망치다
								Battle.state = BattleState.PlayerRun;
								break;
						}
						break;
				}
			}
		}

		public static void PrintBattleText(string text, int wait, int line)
		{
			int y = line == 2 ? 14 : 13;
			Console.SetCursorPosition(1, y);
			foreach (var c in text)
			{
				Console.Write(c);
				Thread.Sleep(100);
			}
			Thread.Sleep(wait * 1000);
			Console.SetCursorPosition(1, y);
			ClearBattleText();
		}

		static void PrintPlayerBattle()
		{
			int skillIndex = 0;
			Pokemon pokemon = Battle.myPokemon!;

			// 기술 null 예외처리
			string[] skillNames = new string[4];
			for (int i = 0; i < 4; i++)
			{
				var skill = pokemon.Skills.Count > i? pokemon.Skills[i] : null;
				skillNames[i] = skill == null ? "    --    " : skill.Name!;
			}

			while (Battle.state == BattleState.PlayerSkill)
			{
				Console.SetCursorPosition(1, battleStartY + 8);
				Console.WriteLine($" [{(skillIndex == 0 ? "▶" : " ")}] {skillNames[0],-5}  [{(skillIndex == 1 ? "▶" : " ")}] {skillNames[1],-5}");
				Console.SetCursorPosition(1, battleStartY + 9);
				Console.WriteLine($" [{(skillIndex == 2 ? "▶" : " ")}] {skillNames[2],-5}  [{(skillIndex == 3 ? "▶" : " ")}] {skillNames[3],-5}");

				ConsoleKey key = Console.ReadKey(true).Key;

				switch (key)
				{
					case ConsoleKey.UpArrow:
						switch (skillIndex)
						{
							case 2: skillIndex = 0; break;
							case 3: skillIndex = 1; break;
						}
						break;

					case ConsoleKey.DownArrow:
						switch (skillIndex)
						{
							case 0: skillIndex = 2; break;
							case 1: skillIndex = 3; break;
						}
						break;

					case ConsoleKey.LeftArrow:
						switch (skillIndex)
						{
							case 1: skillIndex = 0; break;
							case 3: skillIndex = 2; break;
						}
						break;

					case ConsoleKey.RightArrow:
						switch (skillIndex)
						{
							case 0: skillIndex = 1; break;
							case 2: skillIndex = 3; break;
						}
						break;

					case ConsoleKey.Z:
						if (0 <= skillIndex && skillIndex < pokemon.Skills.Count)
						{
							// 기술 사용
							// 임시로 전부 공격
							ClearBattleText();
							var skill = pokemon.Skills[skillIndex];
							if (skill.CurPP > 0)
							{
								// 공격
								skill.Use(Battle.myPokemon!, Battle.enemyPokemon!, skill);
								// 상대 기절 체크
								// if 기절
								// 내 경험치 상승
								// if 레벨업 스탯 증가
								// 끝내기
								Thread.Sleep(2000);
							}
						}
						break;

					case ConsoleKey.X:
						// 다시 메뉴로
						Battle.state = BattleState.PlayerTurn;
						ClearLine(1, battleStartY + 8, 100, 3);
						break;
				}

				// 현재 커서▶가 있는 기술 정보 출력
				ClearBattleText();
				if (0 <= skillIndex && skillIndex < pokemon.Skills.Count)
				{
					var skill = pokemon.Skills[skillIndex];
					Console.SetCursorPosition(1, battleStartY + 1); //13
					Console.WriteLine($"{skill.Name}/ 타입/ {skill.PokeType} {(skill.SkillType == SkillType.Physical ? "물리" : "특수")}");
					Console.SetCursorPosition(1, battleStartY + 2); //14
					Console.WriteLine($"위력/ {skill.Damage} PP/ {skill.CurPP}/{skill.MaxPP}");
				}
			}
		}

		static void PrintBattleTextLine()
		{
			Console.SetCursorPosition(1, battleStartY + 0);
			Console.WriteLine("===========================");
			Console.SetCursorPosition(1, battleStartY + 3);
			Console.WriteLine("===========================");
		}

		static void ClearBattleText()
		{
			ClearLine(1, 13, 100, 2);
		}
	}
}
