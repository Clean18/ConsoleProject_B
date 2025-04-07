using ProjectB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB
{
    // 각 씬에서 메인 화면들
	public enum Scene { Start, Field }

	public static class Game
    {
		public static Stack<Scene> sceneTable = new(); // 씬을 보관할 테이블

		static int timer; // 비동기용 타이머

		static Player? player; // 플레이어 필드
		public static Player Player { get { return player!; } } // 플레이어 프로퍼티, 콜하는 시점에선 null일 수가 없음 진짜임
		static List<MoveObject> currentObjects => Map.GetMoveObjects(sceneTable.Peek()); // 현재 씬에 존재하는 무브 오브젝트 데이터

		public static void Run()
        {
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
			timer = Environment.TickCount; // 첫 타이머

			// TODO : 플레이어 클래스 시작 위치를 맵별로 적용하게 하기
			player = new Player('P', new Position(3, 3), color: ConsoleColor.Green); // 플레이어 초기화

			sceneTable.Push(Scene.Start); // 첫 시작은 Start
		}

		static void Render()
		{
			switch (sceneTable.Peek())
			{
				case Scene.Start:
					Print.PrintStart();
					break;

				case Scene.Field:
					Print.PrintMap();
					Print.PrintObject();
					Print.PrintPlayer();
					break;
			}
		}

		static void Update(ConsoleKey input)
		{

		}
    }
}
