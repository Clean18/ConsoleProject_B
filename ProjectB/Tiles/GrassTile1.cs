using ProjectB.Entities;
using ProjectB.Interfaces;
using ProjectB.Structs;

namespace ProjectB.Tiles
{
	class GrassTile1 : Tile, IWildEncounter
	{
		public int EncounterRate { get; private set; }
		public int MinLevel { get; private set; }
		public int MaxLevel { get; private set; }

		// 푸키먼들도 가지고 있어야함

		public GrassTile1(Position position, int rate = 0, int minLevel = 1, int maxLevel = 5, ConsoleColor bgColor = ConsoleColor.Black)
			: base('w', position, ConsoleColor.Green, bgColor)
		{
			EncounterRate = rate;
			MinLevel = minLevel;
			MaxLevel = maxLevel;
		}

		public bool IsTrigger(Player player)
		{
			// 위치가 같으면
			return player.position == this.position;
		}

		public void OnTrigger(Player player)
		{
			// 푸키먼 배틀
			if (IsTrigger(player))
			{
				int ran = Game.globalRandom.Next(100);
				if (ran < EncounterRate)
				{
					// 배틀
					// 어떻게 랜덤으로 관리하지
				}
			}
		}
	}
}
