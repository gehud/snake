namespace ME.ECS {

    public static partial class ComponentsInitializer {

        static partial void InitTypeIdPartial() {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.Cell>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellAuthoring>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellMaterial>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.Food>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.FoodAuthoring>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeAuthoring>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeHead>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeTail>(true, false, false, false, false, false, false);

        }

        static partial void Init(ref ME.ECS.StructComponentsContainer structComponentsContainer) {

            WorldUtilities.ResetTypeIds();

            CoreComponentsInitializer.InitTypeId();


            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.Cell>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellAuthoring>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Grid.Components.CellMaterial>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.Food>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.FoodAuthoring>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeAuthoring>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeBody>(false, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeHead>(true, false, false, false, false, false, false);
            WorldUtilities.InitComponentTypeId<Snake.Features.Snake.Components.SnakeTail>(true, false, false, false, false, false, false);

            ComponentsInitializerWorld.Setup(ComponentsInitializerWorldGen.Init);
            CoreComponentsInitializer.Init(ref structComponentsContainer);


            structComponentsContainer.Validate<Snake.Features.Grid.Components.Cell>(false);
            structComponentsContainer.Validate<Snake.Features.Grid.Components.CellAuthoring>(false);
            structComponentsContainer.Validate<Snake.Features.Grid.Components.CellMaterial>(false);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.Food>(false);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.FoodAuthoring>(false);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.SnakeAuthoring>(true);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.SnakeBody>(false);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.SnakeHead>(true);
            structComponentsContainer.Validate<Snake.Features.Snake.Components.SnakeTail>(true);

        }

    }

    public static class ComponentsInitializerWorldGen {

        public static void Init(Entity entity) {


            entity.ValidateData<Snake.Features.Grid.Components.Cell>(false);
            entity.ValidateData<Snake.Features.Grid.Components.CellAuthoring>(false);
            entity.ValidateData<Snake.Features.Grid.Components.CellMaterial>(false);
            entity.ValidateData<Snake.Features.Snake.Components.Food>(false);
            entity.ValidateData<Snake.Features.Snake.Components.FoodAuthoring>(false);
            entity.ValidateData<Snake.Features.Snake.Components.SnakeAuthoring>(true);
            entity.ValidateData<Snake.Features.Snake.Components.SnakeBody>(false);
            entity.ValidateData<Snake.Features.Snake.Components.SnakeHead>(true);
            entity.ValidateData<Snake.Features.Snake.Components.SnakeTail>(true);

        }

    }

}
