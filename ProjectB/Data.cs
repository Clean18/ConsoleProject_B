using ProjectB.Entities;
using ProjectB.Items;
using ProjectB.Structs;

namespace ProjectB
{
	public static class Data
	{
		private static Dictionary<Scene, List<string>>? mapData;
		private static Dictionary<Scene, List<Entity>>? mapEntityData;
		private static Dictionary<string, Item>? itemData;
		private static Dictionary<Scene, FieldItem>? fieldItemData;

		public static void DataInit()
		{
			MapInit();
			ItemInit();
			MapEntityInit();
		}

		static void MapInit()
		{
			mapData = new Dictionary<Scene, List<string>>();

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
				if (Enum.TryParse<Scene>(fileName, out var scene))
				{
					// 텍스트 파일 내용으로 리스트로 만들고 인덱서로 mapData에 추가
					mapData[scene] = File.ReadAllLines(file).ToList();
				}
				else
				{
					Console.Clear();
					Console.WriteLine($"씬에 없는 이름입니다.: {fileName}");
					Environment.Exit(0);
				}
			}
		} // 모든 txt 불러와서 Map 초기화

		static void MapEntityInit() // MoveObject 초기화
		{
			mapEntityData = new Dictionary<Scene, List<Entity>>()
			{
				[Scene.Field] = new List<Entity>
				{
					new MoveObject('O', new Position(3, 3), new Position(3, 7), Direction.Down, color: ConsoleColor.Red),
					new FieldItem('B', new Position(2, 2), Direction.Down, itemData!["몬스터볼"], color: ConsoleColor.Red),
					new FieldItem('B', new Position(2, 3), Direction.Down, itemData!["몬스터볼"], color: ConsoleColor.Red),

				},
			};
		}

		static void ItemInit()
		{
			itemData = new Dictionary<string, Item>
			{
				["몬스터볼"] = new PokeBall(),
			};
		}

		public static List<string> GetMapData(Scene scene) => mapData![scene];
		public static List<MoveObject> GetMoveObjects(Scene scene)
		{
			// 없는 씬이 있어도 에러안나게 새로운 리스트 반환
			if (!mapEntityData!.ContainsKey(scene))
				return new List<MoveObject>();
			// Entity 테이블에서 MoveObject만 반환
			else
				return mapEntityData[scene].OfType<MoveObject>().ToList();
		}
		public static List<Entity> GetEntitiesData(Scene scene) => mapEntityData![scene];
	}
}
