using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Skills
{
	class Tackle : Skill
	{
		public Tackle(int curPP = 35) : base("몸통박치기", "상대를 향해서 몸 전체를 부딪쳐가며 공격한다.", PokeType.Normal, SkillType.Physical, 35, 95, 35, curPP)
		{
		}
	}
}
