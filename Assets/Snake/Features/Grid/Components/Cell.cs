using ME.ECS;
using UnityEngine;

namespace Snake.Features.Grid.Components {
	public struct Cell : IComponent {
		public Vector2Int Coordinate;
	}
}