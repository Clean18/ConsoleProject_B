using ProjectB.Entities;
using ProjectB.Structs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    // 각 씬에서 메인 화면들
	public enum Scene
	{
		Start,	// 시작화면
		Field,	// 플레이어를 조작가능한 필드
		Menu,	// 메뉴
		Party,	// 메뉴 > 가지고 있는 푸키먼들
		PartyMenu,	// 메뉴 > 파티 아래에 출력하는 옵션들 / 능력치, 순서바꾸기, 기술, 취소
		PokemonDetail,	// 능력치
		PokemonHasSkill,	// 능력치 > 기술
		PokemonSkillInfo,	// 기술
		WildBattleIntro,	// 야생 배틀 진입시
		TrainerBattleIntro,	// 트레이너 배틀 진입시
		Battle,	// 배틀 중일 떄 / 싸우다, 가방, 포켓몬, 도망가기
		Inventory,
		MyInfo
	}

	public static class Game
    {
		public static Random globalRandom = new Random();

		public static Stack<Scene> sceneTable = new(); // 씬을 보관할 테이블

		static int timer; // 비동기용 타이머

		static Player? player; // 플레이어 필드
		public static Player Player { get { return player!; } } // 플레이어 프로퍼티, 콜하는 시점에선 null일 수가 없음 진짜임
		static List<MoveObject> currentObjects => Data.GetMoveObjects(currentMap); // 현재 씬에 존재하는 무브 오브젝트 데이터

		public static Map currentMap;

		public static void Run()
        {
			Console.CursorVisible = false;
			Init(); // 초기화

			// Game Logic
			while (true)
			{
				if (sceneTable.Peek() == Scene.Field && Environment.TickCount - timer >= 500)
				{
					// 비동기 액션
					foreach (var moveObj in currentObjects)
					{
						moveObj.Move();
					}

					// 타이머 재설정
					timer = Environment.TickCount;
				}

				Render();
				
				if (Console.KeyAvailable)
				{
					Update(Console.ReadKey(true).Key);
				}
			}
		}

		static void Init()
		{
			Data.DataInit(); // 모든 데이터 초기화

			timer = Environment.TickCount; // 첫 타이머

			// TODO : 플레이어 클래스 시작 위치를 맵별로 적용하게 하기
			player = new Player('P', new Position(3, 3), Direction.Down, color: ConsoleColor.Green); // 플레이어 초기화

			sceneTable.Push(Scene.Start); // 첫 시작은 Start
		}

		static void Render()
		{
			switch (sceneTable.Peek())
			{
				// 시작 씬
				case Scene.Start:
					Print.PrintStart();
					break;

				// 필드 씬
				case Scene.Field:
					Print.PrintAll(player!);
					break;

				// 메뉴 씬
				case Scene.Menu:
					Print.PrintMenu(player!);
					break;

				// 파티 씬
				case Scene.Party:
					Print.PrintParty(player!);
					break;

				// 인벤토리 씬
				case Scene.Inventory:
					Print.PrintInventory(player!);
					break;

				// 내정보 씬
				case Scene.MyInfo:
					Print.PrintMyInfo(player!);
					break;

				// 야생 배틀 인트로 씬
				case Scene.WildBattleIntro:
					Print.PrintWildBattleIntro(player!);
					break;

				// 트레이너 배틀 인트로 씬
				case Scene.TrainerBattleIntro:
					break;

				// 배틀 씬
				case Scene.Battle:
					Print.PrintBattle(player!);
					break;
			}
		}

		static void Update(ConsoleKey input)
		{
			player!.KeyHandler(input);
		}

		
    }
}
