using ProjectB.Entities;
using ProjectB.Interfaces;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Tiles
{
	class GrassTile2 : Tile, IWildEncounter
	{
		public int EncounterRate { get; private set; }
		public int MinLevel { get; private set; }
		public int MaxLevel { get; private set; }

		public GrassTile2(Position position, int rate = 0, int minLevel = 1, int maxLevel = 5, ConsoleColor bgColor = ConsoleColor.Black)
			: base('W', position, ConsoleColor.DarkGreen, bgColor)
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
		}
	}
}
