using ProjectB.Skills;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Pokemons
{
	public class Squirtle : Pokemon
	{
		public Squirtle(int level)
			: base(
				id: 1,
				name: "꼬부기",
				level: level,
				baseStat: new BaseStat(
					hp: 44,
					attack: 48,
					defense: 65,
					speAttack: 50,
					speDefense: 64,
					speed: 43),
				iv: IV.GetRandomIV(), // 개체값은 랜덤 생성
				type1: PokeType.Water,
				type2: PokeType.None
			)
		{
			Skills = new List<Skill>()
			{
				new Tackle(),
				new WaterGun(),
			};
		}

		public override void UseSkill(Pokemon attacker, Skill skill, Pokemon defender)
		{
			//skill.Use(attacker, defender);
		}
	}
}
