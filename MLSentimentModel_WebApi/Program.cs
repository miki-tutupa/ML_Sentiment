using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using System.IO;
using ML_Sentiment;

#region Builder
// Configure app
var builder = WebApplication.CreateBuilder(args);

#region Machine Learning Datasets
// ENG
string modelPath = Path.GetFullPath("MLSentimentModel.zip");
builder.Services.AddPredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput>()
    .FromFile("MLSentimentModel", modelPath);

// ESP
string modelPathESP = Path.GetFullPath("MLSentimentModelESP.zip");
builder.Services.AddPredictionEnginePool<MLSentimentModelESP.ModelInputESP, MLSentimentModelESP.ModelOutputESP>()
    .FromFile("MLSentimentModelESP", modelPathESP);
#endregion

builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sentimentator", Description = "Sentimentator: Machine Learning API to predict the sentiment of a given text.", Version = "v1" });
});
#endregion
builder.Services.AddControllers();
#endregion

#region App
var app = builder.Build();
#region Swagger
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sentimentator");
});
#endregion
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
#endregion