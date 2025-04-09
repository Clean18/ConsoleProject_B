using ProjectB.Skills;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Pokemons
{
	public class Charmander : Pokemon
	{
		public Charmander(int level)
			: base(
				id: 1,
				name: "파이리",
				level: level,
				baseStat: new BaseStat(
					hp: 39,
					attack: 52,
					defense: 43,
					speAttack: 60,
					speDefense: 50,
					speed: 65),
				iv: IV.GetRandomIV(), // 개체값은 랜덤 생성
				type1: PokeType.Fire,
				type2: PokeType.None
			)
		{
			Skills = new List<Skill>()
			{
				new Tackle(),
				new Ember(),
				new WaterGun(),
			};
		}

		public override void UseSkill(Pokemon attacker, Skill skill, Pokemon defender)
		{
			//skill.Use(attacker, defender);
		}
	}
}
