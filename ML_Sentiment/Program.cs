using ML_Sentiment;

//Load sample data
var sampleData = new MLSentimentModel.ModelInput()
{
    Comment = @"What the fuck is this?",
};

//Load model and predict output
var result = MLSentimentModel.Predict(sampleData);

Console.WriteLine($"Text: {sampleData.Comment} | Prediction: {(Convert.ToBoolean(result.PredictedLabel) ? "Toxic" : "Non Toxic")} | Probability: {result.Score.Max()}");