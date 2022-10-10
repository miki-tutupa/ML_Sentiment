using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using ML_Sentiment;
using System;
using System.IO;
using System.Reflection;

#region Builder
// Configure app
var builder = WebApplication.CreateBuilder(args);

#region Machine Learning Datasets
// ENG
string modelPath = Path.Combine(AppContext.BaseDirectory, "MLModels", "MLSentimentModel.zip");
builder.Services.AddPredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput>()
    .FromFile("MLSentimentModel", modelPath);

// ESP
string modelPathESP = Path.Combine(AppContext.BaseDirectory, "MLModels", "MLSentimentModelESP.zip"); ;
builder.Services.AddPredictionEnginePool<MLSentimentModelESP.ModelInputESP, MLSentimentModelESP.ModelOutputESP>()
    .FromFile("MLSentimentModelESP", modelPathESP);
#endregion

builder.Services.AddEndpointsApiExplorer();

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sentimentator", Description = "Machine Learning API to predict the sentiment of a given text.", Version = "v1.04" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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