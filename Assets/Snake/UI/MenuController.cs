using UnityEngine;

namespace Snake.UI {
	public class MenuController : MonoBehaviour {
		[SerializeField]
		private GameObject menu;

		public void ToggleVisibility() {
			menu.SetActive(!menu.activeSelf);
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				ToggleVisibility();
			}
		}
	}
}