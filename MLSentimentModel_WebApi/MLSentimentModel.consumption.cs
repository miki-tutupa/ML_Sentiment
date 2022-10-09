using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace ML_Sentiment
{
    public partial class MLSentimentModel
    {
        /// <summary>
        /// model input class for MLSentimentModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"Label")]
            public float Label { get; set; }

            [ColumnName(@"rev_id")]
            public float Rev_id { get; set; }

            [ColumnName(@"comment")]
            public string Comment { get; set; }

            [ColumnName(@"year")]
            public float Year { get; set; }

            [ColumnName(@"logged_in")]
            public bool Logged_in { get; set; }

            [ColumnName(@"ns")]
            public string? Ns { get; set; }

            [ColumnName(@"sample")]
            public string? Sample { get; set; }

            [ColumnName(@"split")]
            public string? Split { get; set; }
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

            public string PredictedResult { get; set; } = null!;

            [ColumnName(@"Score")]
            public float[] Score { get; set; } = null!;
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
}
