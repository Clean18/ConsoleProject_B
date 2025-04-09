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

		public override void Use()
		{
			//
		}
	}
}
