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
    public sealed class SnakeMovementSystem : ISystemFilter {
        private const int GRID_SIZE = 32;        

        private int directionX = 0;
        private int directionY = 0;
        
        public World world { get; set; }

        void ISystemBase.OnConstruct() {
            Controls.OnUp += Up;
            Controls.OnDown += Down;
            Controls.OnRight += Right;
            Controls.OnLeft += Left;
        }
        
        void ISystemBase.OnDeconstruct() {
			Controls.OnUp -= Up;
			Controls.OnDown -= Down;
			Controls.OnRight -= Right;
			Controls.OnLeft -= Left;
		}
        
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter() {
            return Filter
                .Create("Filter-SnakeMovementSystem")
                .With<SnakeBody>()
                .With<Cell>()
                .Push();
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) {
			var body = entity.Get<SnakeBody>();
			bool isHead = entity.Has<SnakeHead>();
            if (!isHead && body.Next.IsEmpty()) {
                return;
            }

            var coordinate = entity.Get<Cell>().Coordinate;
            Vector2Int nextCoordinate;
            if (isHead) {
                nextCoordinate = new Vector2Int {
                    x = coordinate.x + directionX,
                    y = coordinate.y + directionY
				};

                if (nextCoordinate.x < 0) {
					nextCoordinate.x = GRID_SIZE - 1;
                } else if (nextCoordinate.y < 0) {
					nextCoordinate.y = GRID_SIZE - 1;
                } else if (nextCoordinate.x == GRID_SIZE) {
					nextCoordinate.x = 0;
                } else if (nextCoordinate.y == GRID_SIZE) {
					nextCoordinate.y = 0;
                }
            } else {
				nextCoordinate = body.Next.Get<SnakeBody>().LastCoordinate;
            }

			entity.Set(new Cell {
                Coordinate = nextCoordinate
			});

            body.LastCoordinate = coordinate;
			entity.Set(body);
        }

        private void Up() {
            if (directionY == -1) {
                return;
            }

			directionY = 1;
			directionX = 0;
		}

		private void Down() {
			if (directionY == 1) {
				return;
			}

			directionY = -1;
			directionX = 0;
		}

		private void Right() {
			if (directionX == -1) {
				return;
			}

			directionY = 0;
			directionX = 1;
		}

		private void Left() {
			if (directionX == 1) {
				return;
			}

			directionY = 0;
			directionX = -1;
		}
	}
}