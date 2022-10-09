using ML_Sentiment;

//Load sample data
var sampleData = new MLSentimentModel.ModelInput()
{
    Comment = @"What the fuck is this?",
};

//Load model and predict output
var result = MLSentimentModel.Predict(sampleData);

Console.WriteLine($"Text: {sampleData.Comment} | Prediction: {(Convert.ToBoolean(result.PredictedLabel) ? "Toxic" : "Non Toxic")} | Probability: {result.Score.Max()}");


//Load sample data
var sampleDataESP = new MLSentimentModelESP.ModelInput()
{
    Comentario = @"¿Qué mierda es esto?",
};

//Load model and predict output
var resultESP = MLSentimentModelESP.Predict(sampleDataESP);

Console.WriteLine($"Text ESP: {sampleDataESP.Comentario} | Prediction: {(Convert.ToBoolean(resultESP.PredictedLabel) ? "Toxic" : "Non Toxic")} | Probability: {resultESP.Score.Max()}");
