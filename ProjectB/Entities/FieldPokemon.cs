using ProjectB.Interfaces;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectB.Data;

namespace ProjectB.Entities
{
    public class FieldPokemon : Entity, IInteract
    {
		Pokemon Pokemon { get; set; }
		public FieldPokemon(char sprite, Position position, Direction direction, Pokemon pokemon, ConsoleColor color = ConsoleColor.White, ConsoleColor bgColor = ConsoleColor.Black) : base(sprite, position, direction, color, bgColor)
		{
			Pokemon = pokemon;
		}

		public void Interact(Player player)
		{
			player.AddPokemon(Pokemon);

			// 필드에서 아이템 삭제
			List<Entity> entityList = Data.GetEntitiesData(Game.currentMap);
			entityList.Remove(this); // self 제거
		}
	}
}
