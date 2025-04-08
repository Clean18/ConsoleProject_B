using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    public enum SkillType
    {
        Physical,   // 물리
        Special,    // 특수
        Status      // 특수기
    }
    public class Skill
    {
		public string? Name { get; set; }			// 기술명
		public PokeType PokeType { get; set; }		// 포켓몬 타입 ex 불꽃
		public SkillType SkillType { get; set; }	// 물리/특공/보조
		public int Damage { get; set; }				// 위력
		public int Accuracy { get; set; }			// 명중률
		public int MaxPP { get; set; }				// 최대 PP
		public int CurPP { get; set; }				// 현재 PP

		public Skill(string name, PokeType pokeType, SkillType skillType, int damage, int accuracy, int maxPP, int curPP)
		{
			Name = name;
			PokeType = pokeType;
			SkillType = skillType;
			Damage = damage;
			Accuracy = accuracy;
			MaxPP = maxPP;
			CurPP = curPP;
		}

		public void Use()
		{

		}
	}
}
