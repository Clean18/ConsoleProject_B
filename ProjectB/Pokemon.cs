using ProjectB.Pokemons;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectB
{
    public enum PokeType // 타입
    {
		None,		// 없음
        Normal,     // 노말
        Fire,       // 불꽃
        Water,      // 물
        Grass,      // 풀
        Electric,   // 전기
        Ice,        // 얼음
        Fighting,   // 격투
        Poison,     // 독
        Ground,     // 땅
        Flying,     // 비행
		Psychic,    // 에스퍼
		Bug,        // 벌레
        Rock,       // 바위
        Ghost,      // 고스트
        Dragon,     // 드래곤
        Dark,       // 악
        Steel,      // 강철
    }
	public enum Gender // 성별
	{ Male, Female }
	public enum State // 상태이상
	{
		OK,			// 노말
		Poison,		// 독
		Freeze,		// 빙결
		Burn,		// 화상
		Sleep,		// 수면
		Paralysis	// 마비
	}
	
    public abstract class Pokemon
    {
        public int Id { get; set; } // 도감번호
		public string? Name { get; set; } // 이름

		public int Level { get; set; } // 레벨
		public Gender Gender { get; set; } // 성별
		public int CurExp { get; set; } // 현재경험치
		public int NextExp { get; set; } // 다음경험치
		public int Hp { get; set; } // 현재 체력
		public int MaxHp => PokemonStat.hp; // 최대 체력

		// TODO : 스프라이트
		// TODO : 친밀도
		// TODO : 성격
		// TODO : 보유 도구
		// TODO : 노력치

		public BaseStat BaseStat { get; set; } // 종족값
		public IV IV { get; set; } // 개체값
		public PokemonStat PokemonStat { get; set; } // 레벨, 개체, 종족 계산된 수치

		public State State { get; set; } // 상태
		public PokeType PokeType1 { get; set; } // 타입1
		public PokeType PokeType2 { get; set; } // 타입2

		public bool IsDead { get; set; }

		public int BaseExp => (BaseStat.hp + BaseStat.attack + BaseStat.defense + BaseStat.speAttack + BaseStat.speDefense + BaseStat.speed) / 6; // 종족값 평균

		public Pokemon(int id, string name, int level, BaseStat baseStat, IV iv, PokeType type1, PokeType type2)
		{
			Id = id;
			Name = name;
			Level = level;
			Gender = GetGender();
			BaseStat = baseStat;
			IV = iv;
			PokeType1 = type1;
			PokeType2 = type2;

			PokemonStat = GetStat();
			CurExp = 0;
			NextExp = GetNextEXP(level);
			State = State.OK;
			Hp = MaxHp;

			IsDead = false;
		}

		// 개체값 종족값 레벨을 계산해서 기본 스탯 반환
		private PokemonStat GetStat()
		{
			return new PokemonStat(
				hp: ((BaseStat.hp * 2 + IV.hp) * Level) / 100 + Level + 10,
				attack: ((BaseStat.attack * 2 + IV.attack) * Level) / 100 + 5,
				defense: ((BaseStat.defense * 2 + IV.defense) * Level) / 100 + 5,
				speAttack: ((BaseStat.speAttack * 2 + IV.speAttack) * Level) / 100 + 5,
				speDefense: ((BaseStat.speDefense * 2 + IV.speDefense) * Level) / 100 + 5,
				speed: ((BaseStat.speed * 2 + IV.speed) * Level) / 100 + 5
			);
		}

		private int GetNextEXP(int level)
		{
			// TODO : 경험치 테이블
			if (0 <= level && level < 20) // 1 ~ 19
				return level * level * 2;
			else if (20 <= level && level < 40) // 20 ~ 39
				return level * level * 4;
			else if (40 <= level && level < 59) // 40 ~ 59
				return level * level * 6;
			else if (60 <= level && level < 79) // 60 ~ 79
				return level * level * 8;
			else if (80 <= level && level <= 100) // 80 ~ 100
				return level * level * 10;
			return level * level * 10;
		}

		private Gender GetGender()
		{
			return Game.globalRandom.Next(2) == 0 ? Gender.Male : Gender.Female;
		}

		public void Levelup()
		{
			// 100이 아닐때만 레벨업
			if (this.Level < 100)
				this.Level++;

			int oldMaxHp = this.MaxHp; // 레벨업 전 최대체력

			this.PokemonStat = GetStat(); // 스탯 재할당
			NextExp = GetNextEXP(this.Level); // 다음 필요경험치 변경

			// 체력은 레벨업 전 체력에서 레벨업 후 상승한 체력만큼만 증가
			int newHp = oldMaxHp - this.MaxHp;

			this.Hp += newHp;
			// 증가한 체력이 최대체력 안넘게
			if (this.Hp > this.MaxHp)
				this.Hp = this.MaxHp;

			// 레벨업 메시지 출력
			string levelupText = $"{this.Name}의 레벨이 올랐다!";
			Print.PrintBattleText(levelupText, 2, 1);

			string statText = $"체력/ {this.PokemonStat.hp} 공격/ {this.PokemonStat.attack} 방어/ {this.PokemonStat.defense} 특공/ {this.PokemonStat.speAttack} 특방/ {this.PokemonStat.speDefense} 스피드/ {this.PokemonStat.speed}";
			Print.PrintBattleText(statText, 2, 1);
		}

		// 기술 4개 보관
		public List<Skill> Skills { get; set; } = new List<Skill>(4);
		public abstract void UseSkill(Pokemon attacker, Skill skill, Pokemon defender);

		public void SkillChange(int from, int to)
		{
			// 기수ㅡㄹ 위치 교체
			var temp = Skills[from];
			this.Skills[from] = Skills[to];
			this.Skills[to] = temp;
		}

		// TODO : 도감추가
		public static Pokemon Create(int id, int level)
		{
			switch (id)
			{
				case 1: return new Bulbasaur(level);
				case 4: return new Charmander(level);
				case 7: return new Squirtle(level);

				default: return null;
			}
		}

		public void TakeDamage(Pokemon attacker, int damage)
		{
			// 이미죽어있으면 리턴
			if (this.IsDead)
				return;

			this.Hp -= damage;

			// 푸키먼 체력 갱신
			// 내 푸키먼인지 아닌지 골라야되네
			if (this == Battle.myPokemon)
				Print.PrintMyPokemon(this);
			else if (this == Battle.enemyPokemon)
				Print.PrintEnemyPokemon(this);
			Thread.Sleep(2000);

			if (this.Hp <= 0)
			{
				this.Hp = 0;
				this.IsDead = true;
				// 배틀중이면 교체씬으로
				string text = $"{this.Name}는(은) 쓰러졌다!";
				Print.PrintBattleText(text, 2, 1);
			}
		}

		public void GetEXP(int exp)
		{
			string expText = $"{this.Name}는(은) {exp} 경험치를 얻었다!";
			Print.PrintBattleText(expText, 2, 1);
			this.CurExp += exp;
			// 레벨업 반복
			while (this.CurExp >= this.NextExp)
			{
				this.CurExp -= this.NextExp;
				this.Levelup(); // 레벨업 이벤트 실행
			}
		}
	}
}
