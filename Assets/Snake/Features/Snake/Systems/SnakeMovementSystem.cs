using ME.ECS;
using Snake.Features.Grid.Components;

namespace Snake.Features.Snake.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
	using UnityEngine;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class SnakeMovementSystem : ISystemFilter, IUpdate {
        private const float TICK_TIME = 0.15f;
        private const int GRID_SIZE = 32;
        
        private SnakeFeature feature;

        private float elapsed = 0.0f;
        private int directionX = 0;
        private int directionY = 0;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            this.GetFeature(out this.feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter() {
            return Filter
                .Create("Filter-SnakeMovementSystem")
                .With<SnakeHead>()
                .With<SnakeTail>()
                .With<Cell>()
                .Push();
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) {
			if (elapsed < TICK_TIME) {
                elapsed += deltaTime;
                return;
            }

            elapsed = 0.0f;

            var coordinate = entity.Get<Cell>().Coordinate;
            var nextX = coordinate.x + directionX;
            var nextY = coordinate.y + directionY;
            if (nextX < 0) {
                nextX = GRID_SIZE - 1;
            } else if (nextY < 0) {
                nextY = GRID_SIZE - 1;
            } else if (nextX == GRID_SIZE) {
                nextX = 0;
            } else if (nextY == GRID_SIZE) {
                nextY = 0;
            }

			entity.Set(new Cell {
                Coordinate = new Vector2Int(nextX, nextY)
            });

            var tail = entity.Get<SnakeTail>();
            entity.Set(new SnakeTail { 
                Next = tail.Next,
                LastCoordinate = coordinate,
                Sync = true
            });
        }

		void IUpdate.Update(in float deltaTime) {
			if (Input.GetKeyDown(KeyCode.W) && directionY != -1) {
				directionY = 1;
				directionX = 0;
			} else if (Input.GetKeyDown(KeyCode.S) && directionY != 1) {
				directionY = -1;
				directionX = 0;
			} else if (Input.GetKeyDown(KeyCode.D) && directionX != -1) {
				directionY = 0;
				directionX = 1;
			} else if (Input.GetKeyDown(KeyCode.A) && directionX != 1) {
				directionY = 0;
				directionX = -1;
			}
		}
	}
}