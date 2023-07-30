using UnityEngine;
using Zenject;

namespace Snake {
	public class MainMenuManager : MonoBehaviour {
		[SerializeField]
		private GameObject loadingScreen;

		[Inject]
		private readonly IGameDataPayload gameDataPayload;
		[Inject]
		private readonly ISceneLoader sceneLoader;

		public void Play() {
			loadingScreen.SetActive(true);
			WebSocket.CreateGame();
		}

		private void OnEnable() {
			WebSocket.OnGameCreated += OnGameCreated;
		}

		private void OnDisable() {
			WebSocket.OnGameCreated -= OnGameCreated;
		}

		private void OnGameCreated(int id) {
			gameDataPayload.Id = id;
			sceneLoader.LoadScene("Game");
		}
	}
}