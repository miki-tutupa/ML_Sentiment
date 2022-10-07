using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
public partial class MLSentimentModel
{
    /// <summary>
    /// model input class for MLSentimentModel.
    /// </summary>
    #region model input class
    public class ModelInput
    {
        [ColumnName(@"comment")]
        public string Comment { get; set; }
    }

    #endregion

    /// <summary>
    /// model output class for MLSentimentModel.
    /// </summary>
    #region model output class
    public class ModelOutput
    {
        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")]
        public float[] Score { get; set; }
    }

    #endregion

    private static string MLNetModelPath = Path.GetFullPath("MLSentimentModel.zip");

    public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

    /// <summary>
    /// Use this method to predict on <see cref="ModelInput"/>.
    /// </summary>
    /// <param name="input">model input.</param>
    /// <returns><seealso cref=" ModelOutput"/></returns>
    public static ModelOutput Predict(ModelInput input)
    {
        var predEngine = PredictEngine.Value;
        return predEngine.Predict(input);
    }

    private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
    {
        var mlContext = new MLContext();
        ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
        return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
    }
}
