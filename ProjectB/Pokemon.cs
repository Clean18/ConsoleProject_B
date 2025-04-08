using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    public enum PokeType
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
	public enum Gender { Male, Female }
	public enum State
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

		
		public Pokemon(int id, string name, int level, Gender gender, PokemonStat pokemonStat, BaseStat bastStat, IV iv, PokeType type1, PokeType type2)
		{
			Id = id;
			Name = name;
			Level = level;
			Gender = gender;
			BaseStat = bastStat;
			IV = iv;
			PokeType1 = type1;
			PokeType2 = type2;

			PokemonStat = GetStat();
			CurExp = 0;
			NextExp = GetNextEXP(level);
			State = State.OK;

			Hp = MaxHp;
		}

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
			return level * level * 10;
		}

		// 기술 4개 보관
		public Skill[]? Skills { get; set; }
		public virtual void UseSkill(Pokemon attacker, Skill skill, Pokemon defender)
		{
			// pp 체크 후 소모
			if (skill.CurPP < 0)
				return;
			// 명중 체크
			// 대미지 계산 (물리 or 특공) && 자속
			// defender 체력 감소
			// status 체크
			// 기절 체크
		}


	}
}
