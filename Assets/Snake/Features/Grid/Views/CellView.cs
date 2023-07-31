using ME.ECS;
using ME.ECS.Views.Providers;
using Snake.Features.Grid.Components;
using UnityEngine;

namespace Snake.Features.Grid.Views {
	public class CellView : MonoBehaviourView {
        [SerializeField]
        private new Renderer renderer;

		public override bool applyStateJob => true;

        public override void OnInitialize() {}
        
        public override void OnDeInitialize() {}

		public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {
            if (!entity.IsAlive()) {
                return;
            }
            
            var coordinate = entity.Read<Cell>().Coordinate;
            transform.position = new Vector3(coordinate.x, 0.0f, coordinate.y);
        }
        
        public override void ApplyState(float deltaTime, bool immediately) {
			if (!entity.IsAlive()) {
				return;
			}

			var material = entity.Read<CellMaterial>().Value;
            if (renderer.material != material) {
                renderer.material = material;
            }
        }   
    }
}