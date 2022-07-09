using BookRepository.Core;
using BookRepository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IBookData, SqlData>();
builder.Services.AddDbContextPool<BookRepoDbContext>(dbContextOptns =>
{
    _ = dbContextOptns.UseSqlServer(builder.Configuration.GetConnectionString("BookConn"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(); // Add HTTP Client Factory.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/listbooks", async (IBookData service) =>
{
    //return await service.ListBooksAsync();
    return Results.Ok(await service.ListBooksAsync());
})
.WithName("List Books");

app.MapPost("/book", async (Book book, IBookData service) =>
{
    //_ = await service.SaveAsync(book);
    var createdBookId = await service.SaveAsync(book);
    return Results.Created($"/book/{createdBookId}", book);
})
.WithName("Add Book");

app.MapPut("/updatebook", async (Book book, IBookData service) =>
{
    _ = await service.UpdateAsync(book);
    return Results.NoContent();
})
.WithName("Update Book");

app.MapDelete("/deletebook", async ([FromBody]Book book, IBookData service) =>
{
    _ = await service.DeleteAsync(book);
})
.WithName("Delete Book");

app.MapDelete("/deletebook/{id}", async (int id, IBookData service) =>
{
    var result = await service.DeleteAsync(id);    
    return result ? Results.Ok() : Results.NotFound();    
})
.WithName("Delete Book by ID");


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapGet("/sa-weather", async (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient();
    var baseUrl = "https://saas.afrigis.co.za/rest/2";
    var endP = "weather.measurements.getByCoord/myapisamples";
    var auth = "bBFMNngfUSqQ80kFWUwmihszdPs";
    var latlong = "-25.808589,28.255833";
    var range = 10000;
    var count = 3;
    
    var response = await client.GetFromJsonAsync<WeatherResults>($"{baseUrl}/{endP}/{auth}/?location={latlong}&location_buffer={range}&station_count={count}");

    return Results.Ok(response);
})
.WithName("Get SA Weather");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}