using System;
using Microsoft.ML;

namespace HappyMailApi.SentimentAnalysis
{
    public class ModelConsumer
    {
        public static ModelOutput Predict(ModelInput input)
        {
            ModelOutput result = PredictionEngine.Value.Predict(input);
            return result;
        }

        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            MLContext mlContext = new MLContext();
            string modelPath = @"MLModel.zip"; 
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            return predEngine;
        }
    }
}
