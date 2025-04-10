using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Skills
{
	class VineWhip : Skill
	{
		public VineWhip(int curPP = 10) : base("덩굴채찍", "채찍처럼 휘어지는 가늘고 긴 덩굴로 상대를 힘껏 쳐서 공격한다.", PokeType.Grass, SkillType.Physical, 35, 100, 10, curPP)
		{
		}
		public override void Use(Pokemon attacker, Pokemon defender, Skill skill)
		{
			string battleText = $" {attacker.Name}의 {this.Name} 공격!";
			Print.PrintBattleText(battleText, 2, 1);

			this.CurPP--;

			// 명중률
			if (Game.globalRandom.Next(0, 10) >= this.Accuracy)
			{
				// 빗나감
				string missText = $"그러나 {attacker.Name}의 공격은 빗나갔다!";
				Print.PrintBattleText(missText, 2, 2);
			}
			else
			{
				float damageRate = Battle.TypesCalculator(skill.PokeType, defender.PokeType1, defender.PokeType2);
				string damageText;
				switch (damageRate)
				{
					case 0f: damageText = $"그러나 {defender.Name}에게는 효과가 없었다..."; break;

					case 0.25f:
					case 0.5f: damageText = $"효과는 조금 부족한 듯 하다"; break;

					case 2f: damageText = $"효과는 뛰어났다!"; break;

					case 4f: damageText = $"효과는 굉장했다!!"; break;

					default: damageText = ""; break;
				}
				Print.PrintBattleText(damageText, 2, 2);
				int totalDamage = Battle.GetTotalDamage(attacker, defender, this);
				defender.TakeDamage(attacker, totalDamage);

				// 상대가 기절했으면
				if (defender.IsDead)
				{
					// 경험치 증가
					attacker.GetEXP(defender.BaseExp);
					// 상대 교체 체크
					bool isChange = Battle.EnemyPokemonChange();
					Battle.state = isChange ? BattleState.EnemyTurn : BattleState.Win;
					return;
				}

				Battle.state = BattleState.EnemyTurn;
			}
		}
	}
}
