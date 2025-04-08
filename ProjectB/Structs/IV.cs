using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Structs
{ 
	public struct IV
	{
		static Random ran = new Random();

		public int hp;
		public int attack;
		public int defense;
		public int speAttack;
		public int speDefense;
		public int speed;

		public IV(int hp, int attack, int defense, int speAttack, int speDefense, int speed)
		{
			this.hp = hp;
			this.attack = attack;
			this.defense = defense;
			this.speAttack = speAttack;
			this.speDefense = speDefense;
			this.speed = speed;
		}

		public static IV GetRandomIV()
		{
			// 개체값 랜덤 반환
			return new IV(ran.Next(0, 32), ran.Next(0, 32), ran.Next(0, 32), ran.Next(0, 32), ran.Next(0, 32), ran.Next(0, 32));
		}
	}
}
