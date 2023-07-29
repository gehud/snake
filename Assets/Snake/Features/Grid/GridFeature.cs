using ME.ECS;
using UnityEngine;

namespace Snake.Features {

    using Components; using Modules; using Systems; using Features; using Markers;
    using Grid.Components; using Grid.Modules; using Grid.Systems; using Grid.Markers;
	using Grid.Views;

	namespace Grid.Components {}
    namespace Grid.Modules {}
    namespace Grid.Systems {}
    namespace Grid.Markers {}
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class GridFeature : Feature {
        public ViewId CellViewId => cellViewId;
        private ViewId cellViewId;

        [SerializeField]
        private CellView cellView;

        protected override void OnConstruct() {
            cellViewId = world.RegisterViewSource(cellView);
            AddSystem<CellSpawnSystem>();
        }

        protected override void OnDeconstruct() {
            
        }
    }

}