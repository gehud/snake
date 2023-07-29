using ME.ECS;
using UnityEngine;

namespace Snake.Features.Grid.Components {
    public struct CellAuthoring : IComponent {
        public Vector2Int Coordinate;
        public Material Material;
    }
}