using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake {
	public class SceneLoader : MonoBehaviour, ISceneLoader {
		public void LoadScene(string sceneName) {
			SceneManager.LoadScene(sceneName);
		}
	}
}
