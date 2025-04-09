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
		public int[] PokemonIds { get; set; }

		public GrassTile1(Position position, int[] ids, int rate = 0, int minLevel = 1, int maxLevel = 5)
			: base('w', position, ConsoleColor.Green, ConsoleColor.Black)
		{

			this.EncounterRate = rate;
			this.MinLevel = minLevel;
			this.MaxLevel = maxLevel;
			this.PokemonIds = ids;
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
					// 어떻게 랜덤으로 관리하지
					int level = Game.globalRandom.Next(MinLevel, MaxLevel + 1);
					int id = PokemonIds[Game.globalRandom.Next(PokemonIds.Length)];

					Pokemon pokemon = Pokemon.Create(id, level);
					// 도감에 없으면 리턴
					if (pokemon == null)
						return;

					// 플레이어가 가진 모든 포켓몬들이 기절이면 리턴
					if (player.MyFirstPoke() == null)
						return;

					// 배틀
					// 야생
					Battle.enemyPokemon = pokemon;
					Battle.enemyParty = null;
					Battle.isTrainer = false;
					Battle.enemyName = pokemon.Name!;
					Game.sceneTable.Push(Scene.WildBattleIntro);

					// 트레이너
					//Battle.enemyPokemon = null;
					//Battle.enemyParty = new List<Pokemon>() { pokemon };
					//Battle.isTrainer = true;
					//Battle.enemyName = "트레이너이름";
					//Game.sceneTable.Push(Scene.TrainerBattleIntro);
				}
			}
		}
	}
}
