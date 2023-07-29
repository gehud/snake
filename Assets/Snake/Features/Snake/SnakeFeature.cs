using ME.ECS;
using UnityEngine;
using Snake.Features.Grid.Components;

namespace Snake.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;

	namespace Snake.Components {}
    namespace Snake.Modules {}
    namespace Snake.Systems {}
    namespace Snake.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class SnakeFeature : Feature {
        public Material SnakeMaterial => snakeMaterial;

        [SerializeField]
        private Material snakeMaterial;

        protected override void OnConstruct() {
            AddSystem<SnakeMovementSystem>();
            AddSystem<SnakeTailSystem>();

            var snake = world.AddEntity("Snake");
            snake.Set(new SnakeHead());
            snake.Set(new CellAuthoring { Coordinate = Vector2Int.zero, Material = SnakeMaterial });
			snake.Set(new SnakeTail { Next = Entity.Empty, LastCoordinate = Vector2Int.zero });

            var tail_0 = world.AddEntity();
			tail_0.Set(new CellAuthoring { Coordinate = Vector2Int.zero, Material = SnakeMaterial });
			tail_0.Set(new SnakeTail { Next = snake, LastCoordinate = Vector2Int.zero });

			var tail_1 = world.AddEntity();
			tail_1.Set(new CellAuthoring { Coordinate = Vector2Int.zero, Material = SnakeMaterial });
			tail_1.Set(new SnakeTail { Next = tail_0, LastCoordinate = Vector2Int.zero });
		}

        protected override void OnDeconstruct() {
            
        }

    }

}