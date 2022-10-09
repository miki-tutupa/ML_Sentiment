﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using ML_Sentiment;
using System;

namespace MLSentimentModel_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredecirController : ControllerBase
    {
        private readonly PredictionEnginePool<MLSentimentModelESP.ModelInputESP, MLSentimentModelESP.ModelOutputESP> _predictionEnginePool;
                
        public PredecirController(PredictionEnginePool<MLSentimentModelESP.ModelInputESP, MLSentimentModelESP.ModelOutputESP> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        /// <summary>
        /// Predice el sentimiento del comentario en ESPAÑOL.
        /// 0 = No tóxico, 1 = Tóxico
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MLSentimentModelESP.ModelOutputESP> Post([FromBody] MLSentimentModelESP.ModelInputESP input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            MLSentimentModelESP.ModelOutputESP prediction = _predictionEnginePool.Predict(modelName: "MLSentimentModelESP", example: input);

            prediction.PredictedResult = Convert.ToBoolean(prediction.PredictedLabel) ? "Toxic" : "No toxic";

            return Ok(prediction);
        }
    }
}
