using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using ML_Sentiment;
using System;

namespace MLSentimentModel_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictController : ControllerBase
    {
        private readonly PredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput> _predictionEnginePool;
                
        public PredictController(PredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        /// <summary>
        /// Predicts the sentiment of the comment in ENGLISH.
        /// 0 = No toxic, 1 = Toxic
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MLSentimentModel.ModelOutput> Post([FromBody] MLSentimentModel.ModelInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            MLSentimentModel.ModelOutput prediction = _predictionEnginePool.Predict(modelName: "MLSentimentModel", example: input);

            prediction.PredictedResult = Convert.ToBoolean(prediction.PredictedLabel) ? "Toxic" : "No toxic";

            return Ok(prediction);
        }
    }
}
