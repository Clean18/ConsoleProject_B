using System;
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
	}
}
