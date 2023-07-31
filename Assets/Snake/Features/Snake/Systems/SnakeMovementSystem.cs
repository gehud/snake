using ME.ECS;
using Snake.Features.Grid.Components;

namespace Snake.Features.Snake.Systems {
	using Snake.Components;
	using System;
	using UnityEngine;

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class SnakeMovementSystem : ISystem, IAdvanceTick {
		public static event Action OnSelfEating;

		private const int GRID_SIZE = 32;        
		private const float TICK_TIME = 0.15f;        

        private int directionX = 0;
        private int directionY = 0;
        
        public World world { get; set; }

        private Filter snakeFilter;
        private Filter snakeBodyFilter;

        private float elapsedTime = 0.0f;

        void ISystemBase.OnConstruct() {
            snakeFilter = Filter
				.Create()
				.With<SnakeBody>()
				.With<Cell>()
				.Push();

			snakeBodyFilter = Filter
                .Create()
                .With<SnakeBody>()
                .With<Cell>()
                .Without<SnakeHead>()
                .Push();

            Controls.OnUp += Up;
            Controls.OnDown += Down;
            Controls.OnRight += Right;
            Controls.OnLeft += Left;
			GameManager.OnGameOver += OnGameOver;
        }

		void ISystemBase.OnDeconstruct() {
			Controls.OnUp -= Up;
			Controls.OnDown -= Down;
			Controls.OnRight -= Right;
			Controls.OnLeft -= Left;
			GameManager.OnGameOver -= OnGameOver;
		}

		private void OnGameOver() {
            directionX = 0;
            directionY = 0;
		}
    
        void IAdvanceTick.AdvanceTick(in float deltaTime) {
            if (directionX == 0 && directionY == 0) {
                return;
            }

            if (elapsedTime < TICK_TIME) {
                elapsedTime += deltaTime;
                return;
            }

            elapsedTime = 0.0f;

            foreach (var entity in snakeFilter) {
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

                    if (directionX != 0 || directionY != 0) {
                        foreach (var bodyEntity in snakeBodyFilter) {
                            if (bodyEntity.Get<Cell>().Coordinate == nextCoordinate) {
                                OnSelfEating?.Invoke();
                            }
                        }
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