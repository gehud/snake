using System;
using UnityEngine;

namespace Snake {
	public class Controls : MonoBehaviour {
		public static event Action OnUp;
		public static event Action OnDown;
		public static event Action OnRight;
		public static event Action OnLeft;

		public void Up() {
			OnUp?.Invoke();	
		}

		public void Down() {
			OnDown?.Invoke();
		}

		public void Right() {
			OnRight?.Invoke();
		}

		public void Left() {
			OnLeft?.Invoke();
		}

		private void OnEnable() {
			GameManager.OnGameOver += OnGameOver;
		}

		private void OnDisable() {
			GameManager.OnGameOver -= OnGameOver;
		}

		private void OnGameOver() {
			gameObject.SetActive(false);
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.W)) {
				Up();
			} else if (Input.GetKeyDown(KeyCode.S)) {
				Down();
			} else if (Input.GetKeyDown(KeyCode.D)) {
				Right();
			} else if (Input.GetKeyDown(KeyCode.A)) {
				Left();
			}
		}
	}
}