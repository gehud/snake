using UnityEngine;

#region Namespaces
namespace Snake.Generator.Systems {} namespace Snake.Generator.Components {} namespace Snake.Generator.Modules {} namespace Snake.Generator.Features {} namespace Snake.Generator.Markers {} namespace Snake.Generator.Views {}
#endregion

namespace Snake.Generator {
    
    using TState = SnakeState;
    using Snake.Modules;
    using ME.ECS;
    using ME.ECS.Views.Providers;
    using Snake.Generator.Modules;
	using Snake.Features;

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	[DefaultExecutionOrder(-1000)]
    public sealed class SnakeInitializer : InitializerBase {
        private World world;
        public float tickTime = 0.2f;
        public uint inputTicks = 3;

		public void OnDrawGizmos() {
            if (world != null) {
                world.OnDrawGizmos();
            }
        }

        public void Update() {
            if (world == null) {
                // Initialize world
                WorldUtilities.CreateWorld<TState>(ref world, tickTime);
                {
                    #if FPS_MODULE_SUPPORT
                    this.world.AddModule<FPSModule>();
                    #endif
                    world.AddModule<StatesHistoryModule>();
                    world.GetModule<StatesHistoryModule>().SetTicksForInput(inputTicks);
                    world.AddModule<NetworkModule>();
                    
                    // Add your custom modules here
                    
                    // Create new state
                    world.SetState<TState>(WorldUtilities.CreateState<TState>());
                    world.SetSeed(1u);
                    ComponentsInitializer.DoInit();
                    Initialize(world);

                    // Add your custom systems here
                    world.AddFeature(Resources.Load<GridFeature>("GridFeature"));
                    world.AddFeature(Resources.Load<SnakeFeature>("SnakeFeature"));
                }
                
                world.Load(() => {
                    // Save initialization state
                    world.SaveResetState<TState>();
                });
            }

            if (world != null && world.IsLoaded() == true) {
                var dt = Time.deltaTime;
                world.PreUpdate(dt);
                world.Update(dt);
            }
        }

        public void LateUpdate() {
            if (world != null && world.IsLoaded() == true) {
                world.LateUpdate(Time.deltaTime);
            }
        }

        public void OnDestroy() {
            if (world == null || world.isActive == false) { 
                return;
            }
            
            DeInitializeFeatures(world);
            // Release world
            WorldUtilities.ReleaseWorld<TState>(ref world);
        }
    }
}

namespace ME.ECS {
    
    public static partial class ComponentsInitializer {

        public static void InitTypeId() {
            
            ComponentsInitializer.InitTypeIdPartial();
            
        }
        
        static partial void InitTypeIdPartial();
        
        public static void DoInit() {
            
            ComponentsInitializer.Init(ref Worlds.currentWorld.GetStructComponents());
            
        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer);

    }

}