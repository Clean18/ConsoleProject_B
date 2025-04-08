using ProjectB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Items
{
	public class PokeBall : Item, IUse
	{
		public PokeBall(int curCount = 1) : base(1, "몬스터볼", "푸키먼을 잡기 위한 도구", 99, 200, ItemType.Ball, curCount)
		{
		}

		public override void Use()
		{
			// 배틀중일 때
				// 상대가 트레이너가 아닐 때
			// curCount가 1 이상일 때

			// 볼던지기
		}
	}
}
