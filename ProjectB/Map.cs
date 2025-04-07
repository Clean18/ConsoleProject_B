using ProjectB.Entities;

namespace ProjectB
{
	public static class Map
	{
		private static Dictionary<Scene, List<string>> mapData = new Dictionary<Scene, List<string>>()
		{
			[Scene.Field] = new List<string>
			{
				"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@                                                                                             @",
				"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@",
			},
		};

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
	}
}
