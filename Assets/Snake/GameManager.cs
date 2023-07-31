using Snake.Features.Snake.Systems;
using Snake.UI;
using System;
using UnityEngine;
using Zenject;

namespace Snake {
	public class GameManager : MonoBehaviour {
		public static event Action OnGameOver;

		[SerializeField]
		private MenuController menuController;

		[Inject]
		private readonly IGameDataPayload gameDataPayload;

		private int applesEaten = 0;
		private int snakeLength = 3;

		private void OnEnable() {
			SnakeMovementSystem.OnSelfEating += OnSelfEating;
			FoodEatingSystem.OnEatingFood += OnEatingFood;
		}

		private void OnDisable() {
			SnakeMovementSystem.OnSelfEating -= OnSelfEating;
			FoodEatingSystem.OnEatingFood -= OnEatingFood;
		}

		private void OnSelfEating() {
			OnGameOver?.Invoke();
			menuController.ToggleVisibility();
		}

		private void OnEatingFood(FoodType foodType) {
			switch (foodType) {
				case FoodType.Apple:
					applesEaten += 1;
					snakeLength += 1;
					break;
				case FoodType.Banana:
					applesEaten += 2;
					snakeLength += 2;
					break;
			}

			WebSocket.SaveGame(applesEaten, snakeLength, gameDataPayload.Id);
		}

		private void OnDestroy() {
			WebSocket.EndGame(gameDataPayload.Id);
		}
	}
}