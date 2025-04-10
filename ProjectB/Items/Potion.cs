using ProjectB.Interfaces;

namespace ProjectB.Items
{
	class Potion : Item, IUse
	{
		public Potion(int curCount = 1) : base(2, "상처약", "체력을 20 회복시킨다", 99, 300, ItemType.Item, curCount)
		{
		}

		public override void Use(Pokemon pokemon)
		{
			bool isUsed = pokemon.PokemonHeal(20);

			if (Game.sceneTable.Peek() == Scene.Inventory)
			{
				int count = Game.Player.Party.Count;
				Console.SetCursorPosition(Print.startX, 6 + count);
				if (isUsed)
				{
					this.CurCount--;    // 아이템 개수 감소
					Game.Player.InventoryUpdate();  // 인벤토리 업데이트

					Console.WriteLine($" {this.Name}를(을) 사용했다! \n      {pokemon.Name}이(가) {20} 회복했다!");
				}
				else if (!isUsed)
				{
					// 사용을 못했을 떄
					Console.WriteLine(" 체력이 가득차있어 사용할 수 없다!");
				}
			}
			else if (Game.sceneTable.Peek() == Scene.Battle && Battle.state == BattleState.PlayerTurn)
			{
				if (isUsed)
				{
					this.CurCount--;    // 아이템 개수 감소
					Game.Player.InventoryUpdate();  // 인벤토리 업데이트
													// 배틀이고 내턴일 때
					Print.PrintMyPokemon(pokemon);
					int count = Game.Player.Party.Count;
					Print.PrintBattleText($" {this.Name}를(을) 사용했다! \n {pokemon.Name}이(가) {20} 회복했다!", 2, 1);
				}
				else if (!isUsed)
				{
					Print.PrintBattleText(" 체력이 가득차있어 사용할 수 없다!", 2, 1);
				}
			}
			// 배틀중일 때
			// 푸키먼리스트 6마리 뜨고 누구에게 사용할지 선택
			// 필드일 때
			// 메뉴 > 가방 > 아이템탭 > 상처약 선택중일 때 사용
			// 푸키먼 리스트 6마리 뜨고 누구에게 사용할지 선택

			// 사용
		}

		public override void Use()
		{

		}
	}
}
