using Asp.Versioning;
using Asp.Versioning.Conventions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Endpoints;
using MinimalApi.Models;
using MinimalApi.Validators;
using MinimalApi.Versioning;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = new HeaderApiVersionReader("apiversion");
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
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

//app.MapGet("/get_person_data_with_validation", ([FromBody] Person data) =>
//{
//    var result = Validators.Validate(data);
//    if (result.isValid)
//        return Results.Ok(data);
//    return Results.BadRequest(result.errors);
//});


app.MapGet("/get_person_data_with_validation", ([FromBody] Person data,IValidator<Person> validator) =>
{
    var result = validator.Validate(data);
    if(result.IsValid)
        return Results.Ok(data);
    return Results.ValidationProblem(result.ToDictionary(), statusCode: (int)HttpStatusCode.BadRequest);
});


var versionSet = app.NewApiVersionSet()
 .HasApiVersion(1.0)
 .HasApiVersion(2.0)
 .Build();


app.MapGet("/hello_world_version_header", () => "Hello world from v1.0")
    .WithApiVersionSet(versionSet).MapToApiVersion(1.0);

app.MapGet("/hello_world_version_header", () => "Hello version v2.0")
    .WithApiVersionSet(versionSet).MapToApiVersion(2.0);

app.MapGroup("/v1").GroupVersion1();
app.MapGroup("/v2").GroupVersion2();

app.UseAntiforgery();

app.Run();
