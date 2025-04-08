using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Structs
{ 
	public struct IV
	{
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
			return new IV
				(
					Game.globalRandom.Next(0, 32),	// 체력
					Game.globalRandom.Next(0, 32),	// 공격
					Game.globalRandom.Next(0, 32),	// 방어
					Game.globalRandom.Next(0, 32),	// 특공
					Game.globalRandom.Next(0, 32),	// 특방
					Game.globalRandom.Next(0, 32)	// 스핏
				);
		}
	}
}
