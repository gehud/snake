using ME.ECS;
using Snake.Features.Grid.Components;

namespace Snake.Features.Snake.Systems {
#pragma warning disable
	using Components;
	using Markers;
	using Modules;
	using Snake.Components;
	using Snake.Markers;
	using Snake.Modules;
	using Snake.Systems;
	using Systems;
	using UnityEngine;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class SnakeTailSystem : ISystemFilter {
		private SnakeFeature feature;

		public World world { get; set; }

		void ISystemBase.OnConstruct() {

			this.GetFeature(out this.feature);

		}

		void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;

		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }

		Filter ISystemFilter.CreateFilter() {
			return Filter
				.Create("Filter-SnakeTailSystem")
				.With<Cell>()
				.With<SnakeTail>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) {
			var tail = entity.Get<SnakeTail>();
			var next = tail.Next;
			if (next.IsEmpty()) {
				return;
			}

			var nextTail = next.Get<SnakeTail>();
			if (!nextTail.Sync) {
				return;
			}

			var lastCoordinate = entity.Get<Cell>().Coordinate;
			var nextCoordinate = nextTail.LastCoordinate;

			entity.Set(new Cell {
				Coordinate = nextCoordinate
			});

			entity.Set(new SnakeTail {
				Next = next,
				LastCoordinate = lastCoordinate,
				Sync = true
			});

			nextTail.Sync = false;
			next.Set(nextTail);
		}
	}
}