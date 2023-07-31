using Snake.Features.Snake.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Snake.UI {
	public class BananaCountdownView : MonoBehaviour {
		[SerializeField]
		private Image[] images;

		private void Awake() {
			foreach (var image in images) {
				image.fillAmount = 0.0f;
			}
		}

		private void OnEnable() {
			BananaCountdownSystem.OnCountdown += OnCountdown;
		}

		private void OnDisable() {
			BananaCountdownSystem.OnCountdown -= OnCountdown;
		}

		private void OnCountdown(float time) {
			foreach (var image in images) {
				image.fillAmount = time;
			}
		}
	}
}