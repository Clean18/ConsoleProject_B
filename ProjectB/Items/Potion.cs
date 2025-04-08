using ProjectB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Items
{
	class Potion : Item, IUse
	{
		public Potion(int curCount = 1) : base(2, "상처약", "체력을 20 회복시킨다", 99, 300, ItemType.Item, curCount)
		{
		}

		public override void Use()
		{
			// 배틀중일 때
				// 푸키먼리스트 6마리 뜨고 누구에게 사용할지 선택
			// 필드일 때
				// 메뉴 > 가방 > 아이템탭 > 상처약 선택중일 때 사용
				// 푸키먼 리스트 6마리 뜨고 누구에게 사용할지 선택

			// 사용
		}
	}
}
