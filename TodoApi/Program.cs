using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{ c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Version = "v1" });
});


var connectionString = builder.Configuration.GetConnectionString("ToDoDB");
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.36-mysql")), ServiceLifetime.Singleton);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors("MyPolicy");

// app.MapGet("/", () => "Hello World!");
app.MapGet("/items",async( ToDoDbContext context)=>{ 
    var list= await context.Items.ToListAsync();
    return list;}
);
app.MapPost("/items",async( ToDoDbContext context ,string name)=>{ 
    var item=new Item(name);
    await context.Items.AddAsync(item);
    await context.SaveChangesAsync();
    return Results.Ok(item);
   }
);
app.MapPut("/items/{id}", async( ToDoDbContext context ,int id,bool isComplete)=>{ 
    
    Console.WriteLine(id+" "+isComplete);
    Console.WriteLine("lllllllllllllllllllllllll");
    //לקחת את הID מה URL
   var item= await context.Items.FindAsync(id);
   if(item!=null){
    item.IsComplete=isComplete;
    await context.SaveChangesAsync();
    return Results.Ok(item);
   }
   else{
    return Results.BadRequest("id not found");
   }
   });
app.MapDelete("/items/{id}", async( ToDoDbContext context ,int id)=>{ 
   var item= await context.Items.FindAsync(id);
   if(item!=null){
    context.Items.Remove(item);
    await context.SaveChangesAsync();
    return Results.Ok(item);
   }
   else{
    return Results.BadRequest("id not found");
   }
   
   }
);
app.Run();