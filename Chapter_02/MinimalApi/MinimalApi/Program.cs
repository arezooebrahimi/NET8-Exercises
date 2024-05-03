using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints;
using MinimalApi.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAntiforgery();
var app = builder.Build();


//Route Constraints
app.MapGet("/", () => "Hello world");
//app.MapGet("/provinces/{provinceId:int}", (int provinceId) => $"ProvinceId {provinceId}");
app.MapGet("/provinces/{provinceId:int:max(12)}", (int provinceId) => $"ProvinceId {provinceId}");
//app.MapGet("/info/{name:alpha}", (string name) => $"my name is {name}");
app.MapGet("/info/{name:regex(^[a-zA-Z]+$)}", (string name) => $"my name is {name}");
//Route Constraints


//RouteGroups
app.MapGroup("/countries").GroupsCountries();


//Parameter Binding
app.MapGet("/get_person_data_from_body", ([FromBody] Person data) =>
{
    return Results.Ok(data);
});

app.MapGet("/get_person_data_from_form", ([FromForm] Person data) =>
{
    return Results.Ok(data);
});


app.MapGet("/get_lang_from_header", ([FromHeader(Name = "lang")] string lang) =>
{
    return Results.Ok(lang);
});


app.MapGet("/get_langs_from_header", ([FromHeader(Name = "lang")] string[] langs) =>
{
    return Results.Ok(langs);
});

//app.MapGet("/get_address", ([FromQuery] int? limitCountSearch) => {
//    return Results.Ok(limitCountSearch);
//});

app.MapGet("/get_address", ([FromHeader] string coordinates, [FromQuery] int? limitCountSearch) => {
    return Results.Ok(new {coordinates,limitCountSearch});
});


app.MapGet("/get_ids", ([FromQuery] int[] id) =>
{
    return Results.Ok(id);
});


app.UseAntiforgery();

app.Run();
