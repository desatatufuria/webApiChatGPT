using webApiChatGPT.Interfaces;
using webApiChatGPT.Models;
using webApiChatGPT.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
      builder => builder.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod());


}
);

var configuracion = builder.Configuration;
builder.Services.Configure<OpenAISettings>(configuracion.GetSection("OpenAI"));


builder.Services.AddScoped<IChatGptService, ChatGptService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");
app.UseRouting();   

app.UseAuthorization();

app.MapControllers();

app.Run();
