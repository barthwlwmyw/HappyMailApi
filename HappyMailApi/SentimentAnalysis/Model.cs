using Microsoft.ML.Data;

namespace HappyMailApi.SentimentAnalysis
{
    public class ModelInput
    {
        [ColumnName("Sentiment"), LoadColumn(0)]
        public string Sentiment { get; set; }

        [ColumnName("SentimentText"), LoadColumn(1)]
        public string SentimentText { get; set; }

        [ColumnName("LoggedIn"), LoadColumn(2)]
        public string LoggedIn { get; set; }
    }

    public class ModelOutput
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }

        public float[] Score { get; set; }
    }
}
