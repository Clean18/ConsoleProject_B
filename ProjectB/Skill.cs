namespace ProjectB
{
	public enum SkillType
	{
		Physical,   // 물리
		Special,    // 특수
		Status      // 특수기
	}
	public abstract class Skill
	{
		public string? Name { get; set; }           // 기술명
		public string? Description { get; set; }    // 설명
		public PokeType PokeType { get; set; }      // 포켓몬 타입 ex 불꽃
		public SkillType SkillType { get; set; }    // 물리/특공/보조
		public int Damage { get; set; }             // 위력
		public int Accuracy { get; set; }           // 명중률
		public int MaxPP { get; set; }              // 최대 PP
		public int CurPP { get; set; }              // 현재 PP

		public Skill(string name, string description, PokeType pokeType, SkillType skillType, int damage, int accuracy, int maxPP, int curPP)
		{
			Name = name;
			Description = description;
			PokeType = pokeType;
			SkillType = skillType;
			Damage = damage;
			Accuracy = accuracy;
			MaxPP = maxPP;
			if (curPP > MaxPP)
				CurPP = MaxPP;
			else
				CurPP = curPP;
		}

		public void UseSkill(Pokemon attacker, Pokemon defender, Skill skill, bool isEnemy)
		{
			string battleText = isEnemy ? $" 적의 {attacker.Name}의 {this.Name}~" : $" {attacker.Name}의 {this.Name} 공격!";
			Print.PrintBattleText(battleText, 2, 1);

			this.CurPP--;

			// 명중률
			if (Game.globalRandom.Next(0, 10) >= this.Accuracy)
			{
				// 빗나감
				string missText = $"그러나 {attacker.Name}의 공격은 빗나갔다!";
				Print.PrintBattleText(missText, 2, 2);
			}
			else
			{
				// 대미지 계산
				float damageRate = Battle.TypesCalculator(skill.PokeType, defender.PokeType1, defender.PokeType2);
				string damageText = Data.GetDamageText(damageRate);

				// 대미지 처리
				int totalDamage = Battle.GetTotalDamage(attacker, defender, this);
				defender.TakeDamage(attacker, totalDamage);

				// 배틀텍스트 출력
				if (damageRate == 0f) // "그러나 {0}에게는 효과가 없었다..." 보간처리
					Print.PrintBattleText(string.Format(damageText, defender.Name), 2, 2);
				else if (damageRate != 1f)
					Print.PrintBattleText(damageText, 2, 2);

				// 기절 처리
				if (defender.IsDead)
				{
					if (isEnemy) // 상대
					{
						// 상대 푸키먼이 기절했으면
						// 경험치 증가 / 배율
						attacker.GetEXP(defender.BaseExp * Data.expRate);
						bool isChange = Battle.EnemyPokemonChange();
						Battle.state = isChange ? BattleState.PlayerTurn : BattleState.Win;
					}
					else // 나
					{
						// 내 푸키먼이 기절했을 때
						// 내 푸키먼 체크
						// 낼거 없으면 lose
						bool isChange = Battle.PlayerPokemonChange();
						Battle.state = isChange ? BattleState.PokemonChange : BattleState.Lose;
						return;
					}
				}
				else
				{
					Battle.state = isEnemy ? BattleState.PlayerTurn : BattleState.EnemyTurn;
				}
			}
		}
	}
}
