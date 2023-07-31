using Snake.Features.Snake.Systems;
using UnityEngine;

namespace Snake {
	[RequireComponent(typeof(AudioSource))]
	public class SoundPlayer : MonoBehaviour {
		[SerializeField]
		private AudioClip apple;
		[SerializeField]
		private AudioClip banana;
		[SerializeField]
		private AudioClip death;

		public void Apple() {
			audioSource.clip = apple;
			audioSource.Play();
		}

		public void Banana() {
			audioSource.clip = banana;
			audioSource.Play();
		}

		public void Death() {
			audioSource.clip = death;
			audioSource.Play();
		}

		private AudioSource audioSource;
		
		private void Awake() {
			audioSource = GetComponent<AudioSource>();
		}

		private void OnEnable() {
			FoodEatingSystem.OnEatingFood += OnEatingFood;
			SnakeMovementSystem.OnSelfEating += OnSelfEating;
		}

		private void OnDisable() {
			FoodEatingSystem.OnEatingFood -= OnEatingFood;
			SnakeMovementSystem.OnSelfEating -= OnSelfEating;
		}

		private void OnSelfEating() {
			Death();
		}

		private void OnEatingFood(FoodType foodType) {
			switch (foodType) {
				case FoodType.Apple:
					Apple();
					break;
				case FoodType.Banana:
					Banana();
					break;
			}
		}
	}
}