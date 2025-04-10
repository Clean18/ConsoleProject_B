using ProjectB.Entities;
using ProjectB.Items;
using ProjectB.Pokemons;
using ProjectB.Structs;
using ProjectB.Tiles;

namespace ProjectB
{
	public enum Map
	{
		Field,  // 기본맵
	}
	public static class Data
	{
		public static int expRate = 10;

		private static Dictionary<Map, List<string>>? mapData;
		private static Dictionary<Map, List<Entity>>? mapEntityData;
		private static Dictionary<Map, List<Tile>>? mapTileData;
		private static Dictionary<string, Item>? itemData;
		private static Dictionary<float, string>? damageTextData;

		public static void DataInit()
		{
			MapInit();
			ItemInit();
			MapEntityInit();
			MapTileInit();
			DamageTextInit();
		}

		static void MapInit()
		{
			mapData = new Dictionary<Map, List<string>>();

			string folderAdress = "Maps";

			// Maps 폴더 없으면 콘솔 종료
			if (!Directory.Exists(folderAdress))
			{
				Console.Clear();
				Console.WriteLine("Maps 폴더가 없습니다.");
				Environment.Exit(0);
				return;
			}

			string[] files = Directory.GetFiles(folderAdress, "*.txt");

			foreach (var file in files)
			{
				// 텍스트.txt 에서 .txt 제거
				string fileName = Path.GetFileNameWithoutExtension(file);

				// Scene enum으로 트라이파싱
				if (Enum.TryParse<Map>(fileName, out var map))
				{
					// 텍스트 파일 내용으로 리스트로 만들고 인덱서로 mapData에 추가
					mapData[map] = File.ReadAllLines(file).ToList();
				}
				else
				{
					Console.Clear();
					Console.WriteLine($"씬에 없는 이름입니다.: {fileName}");
					Environment.Exit(0);
				}
			}
		} // 모든 txt 불러와서 Map 초기화
		static void ItemInit()
		{
			itemData = new Dictionary<string, Item>
			{
				["몬스터볼"] = new PokeBall(),
				["상처약"] = new Potion(),
			};
		}
		static void MapEntityInit() // MoveObject 초기화 데이터 / 필드마다 생성할 아이템, 무브오브젝트 관리
		{
			mapEntityData = new Dictionary<Map, List<Entity>>()
			{
				[Map.Field] = new List<Entity>
				{
					new MoveObject('O', new Position(3, 3), new Position(3, 7), Direction.Down, color: ConsoleColor.Red),
					new FieldItem('B', new Position(2, 2), Direction.Down, new PokeBall(5), color: ConsoleColor.Red),
					new FieldItem('B', new Position(2, 3), Direction.Down, new PokeBall(3), color: ConsoleColor.Red),
					new FieldItem('B', new Position(2, 4), Direction.Down, new PokeBall(2), color: ConsoleColor.Red),
					new FieldPokemon('@', new Position(5, 1), Direction.Down, new Bulbasaur(5), color: ConsoleColor.White, bgColor: ConsoleColor.DarkGreen),
					new FieldPokemon('@', new Position(6, 1), Direction.Down, new Charmander(5), color: ConsoleColor.White, bgColor: ConsoleColor.Red),
					new FieldPokemon('@', new Position(7, 1), Direction.Down, new Squirtle(5), color: ConsoleColor.White, bgColor: ConsoleColor.Blue),

				},
			};
		}
		static void MapTileInit()
		{
			mapTileData = new Dictionary<Map, List<Tile>>()
			{
				[Map.Field] = new List<Tile>
				{
					new GrassTile1(new Position(5, 3), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),
					new GrassTile2(new Position(6, 3), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),
					new GrassTile1(new Position(7, 3), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),

					new GrassTile2(new Position(5, 4), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),
					new GrassTile1(new Position(6, 4), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [1, 4, 7]),
					new GrassTile2(new Position(7, 4), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),

					new GrassTile1(new Position(5, 5), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),
					new GrassTile2(new Position(6, 5), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),
					new GrassTile1(new Position(7, 5), rate: Game.globalRandom.Next(0, 100), minLevel: 1, maxLevel: 5, ids: [ 1, 4, 7 ]),
				},
			};
		}
		static void DamageTextInit()
		{
			damageTextData = new Dictionary<float, string>
			{
				[0f]	= " 그러나 {0}에게는 효과가 없었다...",
				[0.25f] = " 그러나 효과는 미미했다",
				[0.5f]	= " 효과는 조금 부족한 듯 하다",
				[1f]	= " ",
				[2f]	= " 효과는 뛰어났다!",
				[4f]	= " 효과는 굉장했다!!",
			};
		}


		// 씬을 key로 값을 받아오는 함수들
		public static List<string> GetMapData(Map map) => mapData![map];
		public static List<MoveObject> GetMoveObjects(Map map)
		{
			// 없는 씬이 있어도 에러안나게 새로운 리스트 반환
			if (!mapEntityData!.ContainsKey(map))
				return new List<MoveObject>();
			// Entity 테이블에서 MoveObject만 반환
			else
				return mapEntityData[map].OfType<MoveObject>().ToList();
		}
		public static List<Entity> GetEntitiesData(Map map) => mapEntityData![map];
		public static List<Tile> GetTilesData(Map map) => mapTileData![map];
		public static Item GetItems(string itemName) => itemData![itemName];
		public static string GetDamageText(float damageRate) => damageTextData![damageRate];
	}
}
