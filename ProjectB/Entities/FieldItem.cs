using ProjectB.Interfaces;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Entities
{
	class FieldItem : Entity, IInteract
	{
		Item Item { get; set; }
		public FieldItem(char sprite, Position position, Direction direction, Item item, ConsoleColor color = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black) : base(sprite, position, direction, color, bgColor)
		{
			Item = item;
		}

		public void Interact(Player player)
		{
			player.AddItem(Item);

			// 필드에서 아이템 삭제
			List<Entity> entityList = Data.GetEntitiesData(Game.sceneTable.Peek());
			entityList.Remove(this); // self 제거
		}
	}
}
