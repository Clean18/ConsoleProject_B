using ProjectB.Entities;
using ProjectB.Structs;

namespace ProjectB
{
	public static class Map
	{
		public static void LoadAllMaps()
		{
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
		}

		private static Dictionary<Scene, List<string>> mapData = new Dictionary<Scene, List<string>>();
		//{
		//	[Scene.Field] = new List<string>
		//	{
		//		"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@                                                                                             @",
		//		"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@",
		//	},
		//};

		public static List<string> GetMapData(Scene scene)
		{
			// 무조건 있어야함
			return mapData[scene];
		}

		private static Dictionary<Scene, List<MoveObject>> mapMoveObjectData = new Dictionary<Scene, List<MoveObject>>()
		{
			[Scene.Field] = new List<MoveObject>
			{
				new MoveObject('O', new Position(3, 3), new Position(3, 7), color: ConsoleColor.Red),

			},
		};

		public static List<MoveObject> GetMoveObjects(Scene scene)
		{
			// 없는 씬이 있어도 에러안나게 새로운 리스트 반환
			return mapMoveObjectData.ContainsKey(scene) ? mapMoveObjectData[scene] : new List<MoveObject>();
		}

		// 벽체크
		public static bool IsCanMove(char tile)
		{
			switch (tile)
			{
				case '@':
					return false;
				default:
					return true;
			}
		}
	}
}
