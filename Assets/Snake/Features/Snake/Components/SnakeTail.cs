using ME.ECS;
using UnityEngine;

namespace Snake.Features.Snake.Components {
	public struct SnakeTail : IComponent {
        public Entity Next;
        public Vector2Int LastCoordinate;
        public bool Sync;
    }
}