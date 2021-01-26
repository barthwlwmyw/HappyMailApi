using HappyMailApi.SentimentAnalysis;

namespace HappyMailApi.Services
{
    public interface ISentimentAnalysisService
    {
        bool IsToxic(string text);
    }

    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        public bool IsToxic(string text)
        {
            ModelOutput result = ModelConsumer.Predict(new ModelInput
            {
                SentimentText = text
            });

            return result.Prediction == "1";
        }
    }
}
