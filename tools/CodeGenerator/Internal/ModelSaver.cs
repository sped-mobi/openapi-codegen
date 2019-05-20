namespace Microsoft.OpenApi.CodeGeneration.Internal
{
    public static class ModelSaver
    {
        public static void SaveModel(GeneratedModel model)
        {
            foreach (var file in model.Models)
                FileWriter.WriteFile(file);

            foreach (var file in model.GeneratorInterfaces)
                FileWriter.WriteFile(file);

            foreach (var file in model.GeneratorClasses)
                FileWriter.WriteFile(file);

            foreach (var file in model.ScaffolderInterfaces)
                FileWriter.WriteFile(file);

            foreach (var file in model.ScaffolderClasses)
                FileWriter.WriteFile(file);

            FileWriter.WriteFile(model.ScaffoldedModelClass);
            FileWriter.WriteFile(model.ServicesClassFile);
            //FileWriter.WriteFile(model.SupervisorClass);
            //FileWriter.WriteFile(model.SupervisorInterface);
            //FileWriter.WriteFile(model.SupervisorModelClass);
            //FileWriter.WriteFile(model.ContextClass);
            //FileWriter.WriteFile(model.ContextInterface);
            //FileWriter.WriteFile(model.ContextModelClass);
        }
    }
}
