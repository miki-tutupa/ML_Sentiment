using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace ML_Sentiment
{
    public partial class MLSentimentModelESP
    {
        /// <summary>
        /// model input class for MLSentimentModelESP.
        /// </summary>
        #region model input class
        public class ModelInputESP
        {
            [ColumnName(@"Etiqueta")]
            public float Etiqueta { get; set; }

            [ColumnName(@"rev_id")]
            public float Rev_id { get; set; }

            [ColumnName(@"comentario")]
            public string Comentario { get; set; }

            [ColumnName(@"a�o")]
            public string? A_o { get; set; }

            [ColumnName(@"conectado")]
            public string? Conectado { get; set; }

            [ColumnName(@"ns")]
            public string? Ns { get; set; }

            [ColumnName(@"muestra")]
            public string? Muestra { get; set; }

            [ColumnName(@"separar")]
            public string? Separar { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for MLSentimentModelESP.
        /// </summary>
        #region model output class
        public class ModelOutputESP
        {
            [ColumnName(@"PredictedLabel")]
            public float PredictedLabel { get; set; }

            public string PredictedResult { get; set; } = null!;

            [ColumnName(@"Score")]
            public float[] Score { get; set; } = null!;
        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("MLSentimentModelESP.zip");

        public static readonly Lazy<PredictionEngine<ModelInputESP, ModelOutputESP>> PredictEngine = new Lazy<PredictionEngine<ModelInputESP, ModelOutputESP>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInputESP"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref="ModelOutputESP"/></returns>
        public static ModelOutputESP Predict(ModelInputESP input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInputESP, ModelOutputESP> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInputESP, ModelOutputESP>(mlModel);
        }
    }
}
