using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Snake {
	public class WebSocket : MonoBehaviour {
        public static event Action<int> OnGameCreated;

        public static void CreateGame() {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebSocketCreateGame();
#else
            FakeCreateGame();
#endif
		}

        public static void SaveGame(int appleCount, int snakeLength, int gameId) {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebSocketSaveGame(appleCount, snakeLength, gameId);
#endif
        }

        public static void EndGame(int gameId) {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebSocketEndGame(gameId);
#endif
        }

		[DllImport("__Internal")]
        private static extern void WebSocketOpen(string url);

        [DllImport("__Internal")]
        private static extern void WebSocketCreateGame();

        [DllImport("__Internal")]
        private static extern void WebSocketClose();

        [DllImport("__Internal")]
        private static extern void WebSocketSaveGame(int appleCount, int snakeLength, int gameId);

        [DllImport("__Internal")]
        private static extern void WebSocketEndGame(int gameId);

		[SerializeField]
		private string url = "wss://dev.match.qubixinfinity.io/snake";

		private void Awake() {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebSocketOpen(url);
#endif
		}

#if UNITY_EDITOR
		private static async void FakeCreateGame() {
			await Task.Delay(5000);
			OnGameCreated?.Invoke(0);
		}
#endif

		private void CreateGame(int id) {
            OnGameCreated?.Invoke(id);
        }

		private void OnDestroy() {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebSocketClose();
#endif
		}
	}
}