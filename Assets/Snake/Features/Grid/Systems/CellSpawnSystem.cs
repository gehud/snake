using ME.ECS;

namespace Snake.Features.Grid.Systems {

    #pragma warning disable
    using Snake.Components; using Snake.Modules; using Snake.Systems; using Snake.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class CellSpawnSystem : ISystemFilter {
        
        private GridFeature feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            this.GetFeature(out feature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;

        int ISystemFilter.jobsBatchCount => 64;
        #endif

        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter() {
            return Filter
                .Create("Filter-CellSpawnSystem")
                .With<CellAuthoring>()
                .Push();
        }
    
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) {
            var authoring = entity.Get<CellAuthoring>();
			entity.Set(new Cell { Coordinate = authoring.Coordinate });
			entity.Set(new CellMaterial { Value = authoring.Material });
            entity.InstantiateView(feature.CellViewId);
            entity.Remove<CellAuthoring>();
        }
	}
}