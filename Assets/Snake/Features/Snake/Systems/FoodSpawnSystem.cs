using ME.ECS;
using Snake.Features.Grid.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Features.Snake.Systems {
	using Components;

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class FoodSpawnSystem : ISystemFilter, IAdvanceTickPre, IAdvanceTickPost {
		private SnakeFeature feature;
		public World world { get; set; }

		private Filter snakeFilter;
		private readonly List<Vector2Int> ocupedCells = new List<Vector2Int>();
		private readonly List<Vector2Int> freeCells = new List<Vector2Int>();

		private const int GRID_SIZE = 32;

#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;

		int ISystemFilter.jobsBatchCount => 64;
#endif

		Filter ISystemFilter.filter { get; set; }

		void ISystemBase.OnConstruct() {
			Filter
				.Create()
				.With<SnakeBody>()
				.With<Cell>()
				.Push(ref snakeFilter);

			this.GetFeature(out feature);
		}

		void ISystemBase.OnDeconstruct() { }

		Filter ISystemFilter.CreateFilter() {
			return Filter
				.Create("Filter-FoodSpawnSystem")
				.With<FoodAuthoring>()
				.Push();
		}

		void IAdvanceTickPre.AdvanceTickPre(in float deltaTime) {
			foreach (var entity in snakeFilter) {
				ocupedCells.Add(entity.Get<Cell>().Coordinate);
			}

			for (int x = 0; x < GRID_SIZE; x++) {
				for (int y = 0; y < GRID_SIZE; y++) {
					var coordinate = new Vector2Int(x, y);
					if (!ocupedCells.Contains(coordinate)) {
						freeCells.Add(coordinate);
					}
				}
			}
		}

		private Vector2Int GetFreeRandomCoordinate() {
			if (freeCells.Count == 0) {
				return -Vector2Int.one;
			}

			return freeCells[Random.Range(0, freeCells.Count)];
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) {
			var food = entity.Get<FoodAuthoring>();
			Material material = null;
			switch (food.Type) {
				case FoodType.Apple:
					material = feature.AppleMaterial;
					break;
				case FoodType.Banana:
					material = feature.BanabaMaterial;
					break;
			}

			entity.Set(new Food { 
				Type = food.Type
			});

			entity.Set(new CellAuthoring { 
				Coordinate = GetFreeRandomCoordinate(),
				Material = material
			});

			entity.Remove<FoodAuthoring>();
		}

		void IAdvanceTickPost.AdvanceTickPost(in float deltaTime) {
			ocupedCells.Clear();
			freeCells.Clear();
		}
	}
}