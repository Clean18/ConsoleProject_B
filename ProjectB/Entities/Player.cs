using ProjectB.Structs;

namespace ProjectB.Entities
{
	public class Player : Entity
	{
		int vision;         // 플레이어 시야
		public int visionX; // 플레이어 x 시야
		public int visionY; // 플레이어 y 시야
							// TODO : 돈
		public Dictionary<ItemType, List<Item>> Inventory { get; set; }
		public List<Pokemon> Party { get; private set; }

		public Player(char sprite,
			Position position,
			Direction direction,
			ConsoleColor color = ConsoleColor.White,
			ConsoleColor bgColor = ConsoleColor.Black)
			: base(sprite, position, direction, color, bgColor)
		{
			vision = 4;
			visionX = vision * 2;
			visionY = vision;

			Inventory = new Dictionary<ItemType, List<Item>>()
			{
				[ItemType.Item] = new List<Item>(),     // 소비템
				[ItemType.Ball] = new List<Item>(),     // 몬스터볼
				[ItemType.KeyItem] = new List<Item>(),  // 중요아이템
				[ItemType.TMHM] = new List<Item>(),     // 기술머신
			};
			Party = new List<Pokemon>(6);
		}

		public void KeyHandler(ConsoleKey key, List<string> mapData, List<Entity> entity)
		{
			// TODO : 씬에 따른 방향조절
			// 필드일때 > 이동
			// 배틀중일 떄 > 메뉴 이동
			// 인벤토리일때 > 메뉴 이동
			// 메뉴가 열려있을 떄 > 메뉴이동

			switch (Game.sceneTable.Peek())
			{
				// 우선 필드만
				case Scene.Field:

					break;

			}
			
			switch (key)
			{
				case ConsoleKey.UpArrow:
					Move(Direction.Up, mapData, entity);
					break;

				case ConsoleKey.DownArrow:
					Move(Direction.Down, mapData, entity);
					break;

				case ConsoleKey.LeftArrow:
					Move(Direction.Left, mapData, entity);
					break;

				case ConsoleKey.RightArrow:
					Move(Direction.Right, mapData, entity);
					break;

				case ConsoleKey.Z:  // 선택, 예
					Z();
					break;

				case ConsoleKey.X:  // 취소, 아니오
					X();
					break;

				case ConsoleKey.Escape: // esc 메뉴
					ESC();
					break;
			}
		}

		void FieldInput(ConsoleKey key, List<string> mapData, List<Entity> entity)
		{
			switch (key)
			{
				case ConsoleKey.UpArrow: Move(Direction.Up, mapData, entity); break;
				case ConsoleKey.DownArrow: Move(Direction.Down, mapData, entity); break;
				case ConsoleKey.LeftArrow: Move(Direction.Left, mapData, entity); break;
				case ConsoleKey.RightArrow: Move(Direction.Right, mapData, entity); break;
				case ConsoleKey.Z: Z(this.direction, mapData, entity); break;
			}
		}

		void Move(Direction direction, List<string> mapData, List<Entity> entity)
		{
			// 이동은 못해도 방향전환은 해야함
			this.direction = direction;
			Position nextPos = position + direction;

			// 맵 초과 제한
			if ((nextPos.x < 0) || (nextPos.x >= mapData[position.y].Length) || (nextPos.y < 0) || (nextPos.y >= mapData.Count))
				return;

			// 맵 체크
			char tile = mapData[nextPos.y][nextPos.x];
			switch (tile)
			{
				// 제한할 타일
				case '@':
					return;
			}

			// 오브젝트 체크
			foreach (var obj in entity)
			{
				// 이동할 위치에 오브젝트가 있으면 제한
				if (obj.position == nextPos)
					return;
			}

			position = nextPos;
		}

		void Z(Direction direction, List<string> mapData, List<Entity> entity)
		{
			// TODO : Z 키 인풋

			// 필드일때
			// 배틀중일떄
			// 인벤토리일떄
			// 메뉴가 열려있을 때
			// 대화중일 때
			// 진화중일 때
			// 맵보기
			// 도감
		}

		void X(Direction direction, List<string> mapData, List<Entity> entity)
		{
			// TODO : X 키 인풋
		}

		void ESC()
		{
			// 필드일떄만 메뉴활성화
			if (Game.sceneTable.Peek() == Scene.Field)
			{
				// TODO : 메뉴 활성화
			}
		}

		public void AddPokemon(Pokemon pokemon)
		{
			if (Party.Count >= 6)
				return;

			// TODO : 박스로
			Party.Add(pokemon);
			pokemon.OnLevelup += (pokemon) =>
			{
				// 레벨업
			};
		}

		public void AddItem(Item item)
		{
			// 타입에 맞는 인벤토리 가져오기
			var inven = this.Inventory[item.Type];

			// 이미 존재하는 아이템 있는지 확인
			foreach (var hasItem in inven)
			{
				if (hasItem.Name == item.Name)
				{
					hasItem.CurCount += item.CurCount;

					// 최대 수량만큼만
					// 바닥에 아이템 사라짐
					if (hasItem.CurCount > hasItem.MaxCount)
						hasItem.CurCount = hasItem.MaxCount;

					return;
				}
			}

			// 없으면 새로 추가
			inven.Add(item);
		}
	}
}
