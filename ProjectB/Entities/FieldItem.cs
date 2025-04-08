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
		}
	}
}
