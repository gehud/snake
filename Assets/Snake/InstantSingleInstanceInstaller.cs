using UnityEngine;
using Zenject;

namespace Snake {
	public class InstantSingleInstanceInstaller<TInterface, T> : MonoInstaller where T : TInterface {
		[SerializeField]
		private T instance;

		public override void InstallBindings() {
			Container
				.Bind<TInterface>()
				.FromInstance(instance)
				.AsSingle()
				.NonLazy();
		}
	}
}