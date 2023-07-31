using ME.ECS;
using UnityEngine;

namespace Snake.Features {
	using Snake.Components;
	using Snake.Systems;
	namespace Snake.Components { }
	namespace Snake.Modules { }
	namespace Snake.Systems { }
	namespace Snake.Markers { }

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class SnakeFeature : Feature {
		public Material SnakeMaterial => snakeMaterial;
		public Material AppleMaterial => appleMaterial;
		public Material BanabaMaterial => bananaMaterial;

		[SerializeField]
		private Material snakeMaterial;
		[SerializeField]
		private Material appleMaterial;
		[SerializeField]
		private Material bananaMaterial;

		protected override void OnConstruct() {
			AddSystem<SnakeMovementSystem>();
			AddSystem<FoodEatingSystem>();
			AddSystem<SnakeSpawnSystem>();
			AddSystem<FoodSpawnSystem>();
			AddSystem<BananaCountdownSystem>();

			world.AddEntity().Set<SnakeAuthoring>();
			world.AddEntity().Set<SnakeAuthoring>();
			world.AddEntity().Set<SnakeAuthoring>();

			world.AddEntity().Set(new FoodAuthoring { Type = FoodType.Apple });
		}

		protected override void OnDeconstruct() {}
	}
}