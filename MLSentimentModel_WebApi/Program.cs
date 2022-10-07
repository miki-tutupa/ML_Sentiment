using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;

// Configure app
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput>()
    .FromFile("MLSentimentModel.zip");
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sentimentator", Description = "Sentimentator: Machine Learning API to predict the sentiment of a text.", Version = "v1" });
});
var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sentimentator");
});

// Define prediction route & handler
/// <summary>
/// Predicts the sentiment of the text. Returns a JSON object with the sentiment and the probability.
/// 0 = Non Toxic, 1 = Toxic
/// </summary>
app.MapPost("/predict",
    async (PredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput> predictionEnginePool, MLSentimentModel.ModelInput input) =>
        await Task.FromResult(predictionEnginePool.Predict(input)));

// Run app
app.Run();
