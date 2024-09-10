using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PT_EDII_POS.Application.Items;
using PT_EDII_POS.Domain.Items;
using static Microsoft.AspNetCore.Http.TypedResults;

namespace PT_EDII_POS.API.Features.Items;

public static class ItemEndpoint
{
    public static void MapItemEndpoint(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/items").WithOpenApi().WithTags("Items");

        endpoints.MapGet("", GetItems)
            .Produces<List<Item>>();

        endpoints.MapPost("", PostItems)
            .ProducesProblem(400)
            .Produces<Item>(statusCode: StatusCodes.Status201Created);

        endpoints.MapPut("/{id:int}", UpdateItem)
            .ProducesProblem(400)
            .Produces<Item>();

        endpoints.MapDelete("/{id:int}", DeleteItem)
            .ProducesProblem(400)
            .Produces<Item>();
    }

    private static async Task<IResult> GetItems(ItemServices services)
    {
        var result = await services.GetItems();
        return Ok(result);
    }
    private static async Task<IResult> PostItems(
        ItemServices services,
        // IFormFile formFile,
        CreateItemCommand command,
        IValidator<CreateItemCommand> validator)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return ValidationProblem(validationResult.ToDictionary());

        Item item = command.ToItem();

        var result = await services.CreateItem(item);

        return result.Match<IResult>(
            value => Created($"/api/items/{value.Id}", value: value),
            error => Problem(statusCode: 400));
    }

    private static async Task<IResult> UpdateItem(
        ItemServices services,
        int id,
        UpdateItemCommand command,
        IValidator<UpdateItemCommand> validator)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
            return ValidationProblem(validationResult.ToDictionary());

        Item item = command.MapToItem();

        var result = await services.UpdateItem(id, item);
        return result.Match<IResult>(
            Ok,
            error => Problem(statusCode: StatusCodes.Status400BadRequest));
    }
    private static async Task<IResult> DeleteItem(ItemServices services, int id)
    {
        var result = await services.DeleteItem(id);

        return result.Match<IResult>(
            Ok,
            error => BadRequest(error));
    }

}
