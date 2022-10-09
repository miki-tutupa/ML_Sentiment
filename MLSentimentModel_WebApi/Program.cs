﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using ML_Sentiment;
using System.Threading.Tasks;
using System.Xml.Linq;

// Configure app
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput>()
    .FromFile("MLSentimentModel.zip");
builder.Services.AddPredictionEnginePool<MLSentimentModelESP.ModelInput, MLSentimentModelESP.ModelOutput>()
    .FromFile("MLSentimentModelESP.zip");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sentimentator", Description = "Sentimentator: Machine Learning API to predict the sentiment of a given text.", Version = "v1" });
});
var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sentimentator");
});

// Define prediction route & handler
app.MapPost("/predict",
    async (PredictionEnginePool<MLSentimentModel.ModelInput, MLSentimentModel.ModelOutput>  predictionEnginePool, MLSentimentModel.ModelInput input) =>
        await Task.FromResult(predictionEnginePool.Predict(input)));
app.MapPost("/predecir",
    async (PredictionEnginePool<MLSentimentModelESP.ModelInput, MLSentimentModelESP.ModelOutput> predictionEnginePoolESP, MLSentimentModelESP.ModelInput inputESP) =>
        await Task.FromResult(predictionEnginePoolESP.Predict(inputESP)));

// Run app
app.Run();
