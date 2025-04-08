using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    public abstract class Item : IUse
    {
        public string Name { get; set; }    // 이름
        public string Desc { get; set; }    // 설명
        public int CurCount { get; set; }   // 현재 개수
        public int MaxCount { get; set; }   // 전체 개수

        public Item(string name, string desc, int curCount, int maxCount)
        {
            Name = name;
            Desc = desc;
            CurCount = curCount;
            MaxCount = maxCount;
        }

        public abstract void Use();
    }
}
