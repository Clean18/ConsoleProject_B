using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Skills
{
	class Ember : Skill
	{
		public Ember(int curPP = 25) : base("불꽃세례", "작은 불꽃을 상대에게 발사하여 공격한다.", PokeType.Fire, SkillType.Special, 40, 100, 25, curPP)
		{
		}

		public override void Use()
		{
			//
		}
	}
}
