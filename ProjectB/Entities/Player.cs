﻿using ProjectB.Interfaces;
using ProjectB.Structs;
using System.Numerics;

namespace ProjectB.Entities
{
	public class Player : Entity
	{
		int vision;         // 플레이어 시야
		public int visionX; // 플레이어 x 시야
		public int visionY; // 플레이어 y 시야
							// TODO : 돈
		public Dictionary<ItemType, List<Item>> Inventory { get; set; }
		public List<Pokemon> Party { get; private set; } // 가지고 있는 푸키먼들

		// 필드데이터
		List<string> mapData;
		List<Entity> entityData;
		List<Tile> tileData;

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

		// 맵 이동시마다 호출
		public void SetCurrentField()
		{
			mapData = Data.GetMapData(Game.currentMap);
			entityData = Data.GetEntitiesData(Game.currentMap);
			tileData = Data.GetTilesData(Game.currentMap);
		}

		public void KeyHandler(ConsoleKey key)
		{
			switch (Game.sceneTable.Peek())
			{
				// 우선 필드만
				case Scene.Field:
					FieldInput(key);
					break;
			}
		}

		void FieldInput(ConsoleKey key)
		{
			switch (key)
			{
				// 이동
				case ConsoleKey.UpArrow: Move(Direction.Up); break;
				case ConsoleKey.DownArrow: Move(Direction.Down); break;
				case ConsoleKey.LeftArrow: Move(Direction.Left); break;
				case ConsoleKey.RightArrow: Move(Direction.Right); break;
				// 상호작용
				case ConsoleKey.Z: Z(this.direction); break;
				case ConsoleKey.Escape: ESC(); break;
			}
		}

		void Move(Direction direction)
		{
			// 이동은 못해도 방향전환은 해야함
			this.direction = direction;
			Position nextPos = position + direction;

			// 맵 초과 제한
			if ((nextPos.x < 0) || (nextPos.x >= mapData[position.y].Length) || (nextPos.y < 0) || (nextPos.y >= mapData.Count))
				return;

			// 맵 체크
			char mapTile = mapData[nextPos.y][nextPos.x];
			switch (mapTile)
			{
				// 제한할 맵 타일
				case '@':
					return;
			}

			// 오브젝트 체크
			foreach (var obj in entityData)
			{
				// 이동할 위치에 오브젝트가 있으면 제한
				if (obj.position == nextPos)
					return;
			}

			position = nextPos;

			// 이동 후 풀숲체크
			foreach (var tileObj in tileData)
			{
				if (tileObj is IWildEncounter wildEncounter)
				{
					// 1마리 이상 가지고 있을때만
					if (Party.Count > 0)
						wildEncounter.OnTrigger(this);
				}
			}
		}

		void Z(Direction direction)
		{
			this.direction = direction;
			Position nextPos = position + direction;

			// 맵 초과 제한
			if ((nextPos.x < 0) || (nextPos.x >= mapData[position.y].Length) || (nextPos.y < 0) || (nextPos.y >= mapData.Count))
				return;

			foreach (var obj in entityData)
			{
				// 이동할 위치에 오브젝트가 있으면 상호작용
				if (obj.position == nextPos && obj is IInteract interactable)
				{
					interactable.Interact(this);
					return;
				}
			}
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
				Game.sceneTable.Push(Scene.Menu);
			}
		}
		public void AddPokemon(Pokemon pokemon)
		{
			// TODO : 가진 푸키먼이 6마리 초과면 박스로 보냄
			if (Party.Count >= 6)
				return;

			Party.Add(pokemon);

			Print.PrintBattleText($" {pokemon.Name} 을/를 얻었습니다.\n  파티에 추가됩니다.", 1, 1);
			Print.ClearInput();
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
					hasItem.CurCount = hasItem.CurCount + item.CurCount;

					// 최대 수량만큼만
					// 바닥에 있던 아이템 사라짐
					if (hasItem.CurCount > hasItem.MaxCount)
						hasItem.CurCount = hasItem.MaxCount;

					Print.PrintBattleText($" {item.Name} 을/를 {item.CurCount} 개 얻었습니다. ({hasItem.CurCount}개)", 1, 1);
					Print.ClearInput();
					return;
				}
			}

			// 없으면 새로 추가
			inven.Add(item);

			Print.PrintBattleText($" {item.Name} 을/를 {item.CurCount} 개 얻었습니다.", 1, 1);
			Print.ClearInput();
		}

		public Pokemon MyFirstPoke()
		{
			foreach (var poke in Party)
			{
				if (poke.Hp > 0)
					return poke;
			}
			return null;
		}

		public void InventoryUpdate()
		{
			// 키값으로 반복
			foreach (ItemType type in Inventory.Keys)
			{
				List<Item> newList = new List<Item>();

				// 개수가 1개이상인 아이템들로 인벤토리 갱신
				foreach (var item in Inventory[type])
				{
					if (item.CurCount > 0)
					{
						newList.Add(item);
					}
				}
				Inventory[type] = newList;
			}
		}
	}
}
