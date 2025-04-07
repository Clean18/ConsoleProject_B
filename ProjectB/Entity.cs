using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    public abstract class Entity
    {
		public char sprite;			 // 출력한 문자
		public Position position;	 // 포지션
		public ConsoleColor color;	 // 색
		public ConsoleColor bgColor; // 배경색

		public Entity(char sprite,
			Position position,
			ConsoleColor color = ConsoleColor.White,
			ConsoleColor bgColor = ConsoleColor.Black)
		{
			this.sprite = sprite;
			this.color = color;
			this.bgColor = bgColor;
			this.position = position;
		}
	}
}
