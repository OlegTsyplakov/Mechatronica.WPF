﻿using SignalRServer.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddSignalR();
var app = builder.Build();

app.UseCors(builder => builder
           .WithOrigins("null")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials());




app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<StreamHub>("/streamHub");
});


app.Run();