using ProjectB.Skills;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Pokemons
{
	public class Bulbasaur : Pokemon
	{
		public Bulbasaur(int level)
			: base(
				id: 1,
				name: "이상해씨",
				level: level,
				baseStat: new BaseStat(hp: 45, attack: 49, defense: 49, speAttack: 65, speDefense: 65, speed: 45),
				iv: IV.GetRandomIV(), // 개체값은 랜덤 생성
				type1: PokeType.Grass,
				type2: PokeType.Poison
			)
		{
			Skills = new List<Skill>()
			{
				new Tackle(),
				new VineWhip(15),
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
