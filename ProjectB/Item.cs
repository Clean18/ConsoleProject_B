using ProjectB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    public enum ItemType
    {
        Item,       // 소비템 ex 상처약
        Ball,       // 볼 ex 몬스터볼
        KeyItem,    // 중요아이템 ex 퀘템? 낚싯대
        TMHM        // 기술머신 TM 비전머신 HM
    }
    public abstract class Item : IUse
    {
        public int Id { get; set; }         // 번호
        public string Name { get; set; }    // 이름
        public string Desc { get; set; }    // 설명
        public int MaxCount { get; set; }   // 전체 개수
        public int CurCount { get; set; }   // 현재 개수
        public int BuyPrice { get; set; }   // 구매 가격
        public int SellPrice { get; set; }  // 판매 가격
        public ItemType Type { get; set; }  // 아이템 종류

		public Item(int id, string name, string desc, int maxCount, int price, ItemType type, int curCount = 1)
		{
            Id = id;
			Name = name;
			Desc = desc;
			MaxCount = maxCount;
            BuyPrice = price;
			SellPrice = price / 2;
			Type = type;
			CurCount = curCount;
		}

		public abstract void Use();

		public abstract void Use(Pokemon pokemon);
	}
}
