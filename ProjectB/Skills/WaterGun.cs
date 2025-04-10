﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Skills
{
	class WaterGun : Skill
	{
		public WaterGun(int curPP = 25) : base("물대포", "물을 기세 좋게 상대에게 발사하여 공격한다.", PokeType.Water, SkillType.Special, 40, 100, 25, curPP)
		{
		}
		public override void Use(Pokemon attacker, Pokemon defender, Skill skill)
		{
			string battleText = $" {attacker.Name}의 {this.Name} 공격!";
			Print.PrintBattleText(battleText, 2, 1);

			this.CurPP--;

			// 명중률
			if (Game.globalRandom.Next(0, 101) > this.Accuracy)
			{
				// 빗나감
				string missText = $"그러나 {attacker.Name}의 공격은 빗나갔다!";
				Print.PrintBattleText(battleText, 2, 2);
			}
			else
			{
				float damageRate = Battle.TypesCalculator(skill.PokeType, defender.PokeType1, defender.PokeType2);
				string damageText;
				Print.PrintBattleText(battleText, 2, 2);
				switch (damageRate)
				{
					case 0f: damageText = $"그러나 {defender.Name}에게는 효과가 없었다..."; break;

					case 0.5f: damageText = $"효과는 조금 부족한 듯 하다"; break;

					case 2f: damageText = $"효과는 뛰어났다!"; break;

					case 4f: damageText = $"효과는 굉장했다!!"; break;

					default: damageText = ""; break;
				}
				int totalDamage = Battle.GetTotalDamage(attacker, defender, this);
				defender.TakeDamage(attacker, totalDamage);
			}
		}
	}
}
