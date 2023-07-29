namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.Cell>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellAuthoring>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellMaterial>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeHead>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeTail>(false, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.Cell>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellAuthoring>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellMaterial>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeHead>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeTail>(false, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Snake.Features.Grid.Components.Cell>(false);
            structComponentsContainer.Validate<Snake.Features.Grid.Components.CellAuthoring>(false);
            structComponentsContainer.Validate<Snake.Features.Grid.Components.CellMaterial>(false);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.SnakeHead>(true);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.SnakeTail>(false);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Snake.Features.Grid.Components.Cell>(false);
            entity.ValidateData<Snake.Features.Grid.Components.CellAuthoring>(false);
            entity.ValidateData<Snake.Features.Grid.Components.CellMaterial>(false);
            entity.ValidateData<Snake.Features.Snake.Components.SnakeHead>(true);
            entity.ValidateData<Snake.Features.Snake.Components.SnakeTail>(false);

        }

    }

}
