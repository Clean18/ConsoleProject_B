using ProjectB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
	public enum BattleState
	{
		Intro,
		SpeedCheck,
		PlayerTurn,
		EnemyTurn,
		PokemonChange,
		PlayerSkill,
		PlayerInventory,
		PlayerPokemon,
		PlayerRun,
		Win,
		Lose,
	}
	public static class Battle
	{
		public static Pokemon? enemyPokemon;	// 야생
		public static List<Pokemon>? enemyParty;// 트레이너

		public static Pokemon? myPokemon;		// 선발
		public static List<Pokemon>? myParty;	// 파티

		public static bool isTrainer;			// 구분할 bool, 볼던지기 방지
		public static string? enemyName;        // 상대방 이름

		// 배틀을 어떻게 진행하지
		public static BattleState state = BattleState.Intro;

		public static void SpeedCheck()
		{
			Pokemon? enemyFirst = isTrainer ? enemyParty![0] : enemyPokemon;
			// 내가 더 빠름
			if (myPokemon!.PokemonStat.speed > enemyFirst!.PokemonStat.speed)
			{
				state = BattleState.PlayerTurn;
			}
			// 상대가 빠름
			else if (myPokemon!.PokemonStat.speed < enemyFirst!.PokemonStat.speed)
			{
				state = BattleState.EnemyTurn;
			}
			// 똑같으면 랜덤
			else if (myPokemon!.PokemonStat.speed == enemyFirst!.PokemonStat.speed)
			{
				state = Game.globalRandom.Next(0, 2) == 0 ? BattleState.PlayerTurn : BattleState.EnemyTurn;
			}
		}

		public static void EnemyAction()
		{
			// 텍스트 클리어
			Print.ClearLine(1, 16, 80, 7); //여기서 싹지우네

			// 상대 푸키먼이 가진 기술의 타입이
			// 내 푸키먼의 타입의 상성이면
			Pokemon? enemyFirst = isTrainer ? enemyParty![0] : enemyPokemon;
			Pokemon? myFirst = Game.Player.MyFirstPoke();

			PokeType defense1 = myFirst.PokeType1;
			PokeType defense2 = myFirst.PokeType2;

			int maxDamage = -1;
			Skill strongSkill = null;

			foreach (var skill in enemyFirst!.Skills)
			{
				if (skill == null)
					continue;

				if (skill.CurPP <= 0)
					continue;

				float damageRate = TypesCalculator(skill.PokeType, defense1, defense2);
				float totalDamage = skill.Damage * damageRate;
				if (totalDamage > maxDamage)
				{
					maxDamage = (int)totalDamage;
					strongSkill = skill;
				}
			}

			// strongSkill null 예외처리
			if (strongSkill == null)
			{
				strongSkill = enemyFirst.Skills[Game.globalRandom.Next(0, enemyFirst.Skills.Count)];

				// TODO : 발버둥 사용
				// 기술 포인트가 없어지면 나오는 기술. 자신도 조금 데미지를 입는다
			}

			strongSkill.UseSkill(enemyFirst, myFirst, strongSkill, false);
		}

		public static float TypesCalculator(PokeType attack, PokeType defense1, PokeType defense2)
		{
			float firstDamageRate = TypeCalculator(attack, defense1);
			float secondDamageRate = TypeCalculator(attack, defense2);

			return firstDamageRate * secondDamageRate;
		}

		public static float TypeCalculator(PokeType attack, PokeType defense)
		{
			
			if (defense == PokeType.None) return 1f;
			
			if (attack == PokeType.Normal)
			{
				if (defense == PokeType.Rock || defense == PokeType.Steel) return 0.5f;
				if (defense == PokeType.Ghost) return 0.0f;
			}
			else if (attack == PokeType.Fire)
			{
				if (defense == PokeType.Grass || defense == PokeType.Ice || defense == PokeType.Bug || defense == PokeType.Steel) return 2f;
				if (defense == PokeType.Fire || defense == PokeType.Water || defense == PokeType.Rock || defense == PokeType.Dragon) return 0.5f;
			}
			else if (attack == PokeType.Water)
			{
				if (defense == PokeType.Fire || defense == PokeType.Ground || defense == PokeType.Rock) return 2f;
				if (defense == PokeType.Water || defense == PokeType.Grass || defense == PokeType.Dragon) return 0.5f;
			}
			else if (attack == PokeType.Electric)
			{
				if (defense == PokeType.Water || defense == PokeType.Flying) return 2f;
				if (defense == PokeType.Electric || defense == PokeType.Grass || defense == PokeType.Dragon) return 0.5f;
			}
			else if (attack == PokeType.Grass)
			{
				if (defense == PokeType.Water || defense == PokeType.Ground || defense == PokeType.Rock) return 2f;
				if (defense == PokeType.Fire || defense == PokeType.Grass || defense == PokeType.Poison || defense == PokeType.Flying || defense == PokeType.Bug || defense == PokeType.Dragon || defense == PokeType.Steel) return 0.5f;
			}
			else if (attack == PokeType.Ice)
			{
				if (defense == PokeType.Grass || defense == PokeType.Ground || defense == PokeType.Flying || defense == PokeType.Dragon) return 2f;
				if (defense == PokeType.Fire || defense == PokeType.Water || defense == PokeType.Ice || defense == PokeType.Steel) return 0.5f;
			}
			else if (attack == PokeType.Fighting)
			{
				if (defense == PokeType.Normal || defense == PokeType.Ice || defense == PokeType.Rock || defense == PokeType.Dark || defense == PokeType.Steel) return 2f;
				if (defense == PokeType.Poison || defense == PokeType.Flying || defense == PokeType.Psychic || defense == PokeType.Bug) return 0.5f;
				if (defense == PokeType.Ghost) return 0f;
			}
			else if (attack == PokeType.Poison)
			{
				if (defense == PokeType.Grass) return 2f;
				if (defense == PokeType.Poison || defense == PokeType.Ground || defense == PokeType.Rock || defense == PokeType.Ghost) return 0.5f;
				if (defense == PokeType.Steel) return 0f;
			}
			else if (attack == PokeType.Ground)
			{
				if (defense == PokeType.Fire || defense == PokeType.Electric || defense == PokeType.Poison || defense == PokeType.Rock || defense == PokeType.Steel) return 2f;
				if (defense == PokeType.Grass || defense == PokeType.Bug) return 0.5f;
				if (defense == PokeType.Flying) return 0f;
			}
			else if (attack == PokeType.Flying)
			{
				if (defense == PokeType.Grass || defense == PokeType.Fighting || defense == PokeType.Bug) return 2f;
				if (defense == PokeType.Electric || defense == PokeType.Rock || defense == PokeType.Steel) return 0.5f;
			}
			else if (attack == PokeType.Psychic)
			{
				if (defense == PokeType.Fighting || defense == PokeType.Poison) return 2f;
				if (defense == PokeType.Psychic || defense == PokeType.Steel) return 0.5f;
				if (defense == PokeType.Dark) return 0f;
			}
			else if (attack == PokeType.Bug)
			{
				if (defense == PokeType.Grass || defense == PokeType.Psychic || defense == PokeType.Dark) return 2f;
				if (defense == PokeType.Fire || defense == PokeType.Fighting || defense == PokeType.Poison || defense == PokeType.Flying || defense == PokeType.Ghost || defense == PokeType.Steel) return 0.5f;
			}
			else if (attack == PokeType.Rock)
			{
				if (defense == PokeType.Fire || defense == PokeType.Ice || defense == PokeType.Flying || defense == PokeType.Bug) return 2f;
				if (defense == PokeType.Fighting || defense == PokeType.Ground || defense == PokeType.Steel) return 0.5f;
			}
			else if (attack == PokeType.Ghost)
			{
				if (defense == PokeType.Psychic || defense == PokeType.Ghost) return 2f;
				if (defense == PokeType.Dark || defense == PokeType.Steel) return 0.5f;
				if (defense == PokeType.Normal) return 0f;
			}
			else if (attack == PokeType.Dragon)
			{
				if (defense == PokeType.Dragon) return 2f;
				if (defense == PokeType.Steel) return 0.5f;
			}
			else if (attack == PokeType.Dark)
			{
				if (defense == PokeType.Psychic || defense == PokeType.Ghost) return 2f;
				if (defense == PokeType.Fighting || defense == PokeType.Dark || defense == PokeType.Steel) return 0.5f;
			}
			else if (attack == PokeType.Steel)
			{
				if (defense == PokeType.Ice || defense == PokeType.Rock) return 2f;
				if (defense == PokeType.Fire || defense == PokeType.Water || defense == PokeType.Electric || defense == PokeType.Steel) return 0.5f;
			}

			return 1f;
		}

		public static int GetTotalDamage(Pokemon attacker, Pokemon defender, Skill skill)
		{
			int level = attacker.Level;
			int power = skill.Damage;
			bool isSpecial = skill.SkillType == SkillType.Special;

			int attackStat = isSpecial ? attacker.PokemonStat.speAttack : attacker.PokemonStat.attack;
			int defenseStat = isSpecial ? defender.PokemonStat.speDefense : defender.PokemonStat.defense;

			float damageRate = 1f;

			// 자속 체크
			if (skill.PokeType == attacker.PokeType1 || skill.PokeType == attacker.PokeType2)
				damageRate *= 1.5f;

			// 타입 체크
			damageRate *= TypesCalculator(skill.PokeType, defender.PokeType1, defender.PokeType2);

			// 랜덤 난수 0.85 ~ 1
			damageRate *= Game.globalRandom.Next(85, 101) / 100f;

			// 데미지 계산 공식
			float damage = (((((2f * level) / 5 + 2) * power * attackStat / defenseStat) / 50) + 2) * damageRate;

			// 최소 대미지 1
			return Math.Max(1, (int)damage);
		}

		public static bool EnemyPokemonChange()
		{
			// 상대 기절시 푸키먼 교체

			// 야생이면 바로 끝
			if (!isTrainer)
				return false;

			// 살아있는 푸키먼 체크
			foreach (var poke in enemyParty!)
			{
				if (!poke.IsDead && poke.Hp > 0)
				{
					enemyPokemon = poke;

					// ~는(은)  ~를(을) 차례로 꺼냈다
					Print.PrintBattleText($"{enemyName}는(은) {enemyPokemon.Name}를(을) 차례로 꺼냈다", 2, 1);
					return true;
				}
			}

			// 남은 푸키먼없음
			return false;
		}

		public static bool PlayerPokemonChange()
		{
			// 살아있는 푸키먼 체크
			foreach (var poke in myParty!)
			{
				if (!poke.IsDead && poke.Hp > 0)
				{
					// 살아있는 푸키먼 있으면 교체씬으로
					return true;
				}
			}

			// 남은 푸키먼없음
			return false;
		}

		public static void EndBattle()
		{
			Battle.state = BattleState.Intro;
			Game.sceneTable.Pop();
			Console.Clear();
		}
	}
}
