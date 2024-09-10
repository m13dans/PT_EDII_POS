using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PT_EDII_POS.API.Extension;
using PT_EDII_POS.API.Features.Items;
using PT_EDII_POS.Application.Items;
using PT_EDII_POS.Infrastructure.DataContext;
using PT_EDII_POS.Infrastructure.Repository.Items;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});



builder.Services.AddScoped<ItemServices>();

builder.Services.AddScoped<IValidator<CreateItemCommand>, CreateItemValidator>();
builder.Services.AddScoped<IValidator<UpdateItemCommand>, UpdateItemValidator>();

builder.Services.AddScoped<IItemRepository, ItemRepository>();

var app = builder.Build();

app.ApplyMigration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapItemEndpoint();

app.Run();