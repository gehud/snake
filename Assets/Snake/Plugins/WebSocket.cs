using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Snake {
	public class WebSocket : MonoBehaviour {
        public static event Action<int> OnGameCreated;

        public static void CreateGame() {
#if !UNITY_EDITOR
            WebSocketCreateGame();
#else
            FakeCreateGame();
#endif
		}

        [DllImport("__Internal")]
        private static extern void WebSocketOpen(string url);

        [DllImport("__Internal")]
        private static extern void WebSocketCreateGame();

        [DllImport("__Internal")]
        private static extern void WebSocketClose();

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
			CreateGame(0);
		}
#endif

		private static void CreateGame(int id) {
            Debug.Log($"Game created. Id: {id}");
            OnGameCreated?.Invoke(id);
        }

		private void OnDestroy() {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebSocketClose();
#endif
		}
	}
}