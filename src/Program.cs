using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
await using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

Character[] data = new[]
{
   new Character ("1", "Luke Skywalker","https://www.starwars.com/databank/luke-skywalker"),
   new Character ("2", "Leia Organa",   "https://www.starwars.com/databank/leia-organa"),
   new Character ("3", "Han Solo",      "https://www.starwars.com/databank/han-solo"  ),
   new Character ("4", "Chewbacca",     "https://www.starwars.com/databank/chewbacca"  ),
   new Character ("5", "Obi-Wan Kenobi","https://www.starwars.com/databank/obi-wan-kenobi"  ),
   new Character ("6", "Darth Vader",   "https://www.starwars.com/databank/darth-vader"  ),
   new Character ("7", "Yoda",          "https://www.starwars.com/databank/yoda"  ),
   new Character ("8", "C-3PO",         "https://www.starwars.com/databank/c-3po"  ),
   new Character ("9", "R2-D2",         "https://www.starwars.com/databank/r2-d2"  ),
};

app.MapGet("/api/hello", (Func<string>)(() => "Hello Space!"));

app.MapGet("/api/characters/", (Func<IActionResult>)(() => new OkObjectResult(data)));

app.MapGet("/api/characters/{id}", (Func<string, IActionResult>)((id) => !(data.FirstOrDefault(c => c.Id == id) is { } chr) ? new NotFoundResult() : new OkObjectResult(chr)));

app.MapDelete("/api/characters/{id}", (Func<string, IActionResult>)((id) =>
{
    data = data.Where(c => c.Id != id).ToArray();
    return new OkResult();
}));

await app.RunAsync();

public record Character(string Id, string Name, string Link);
