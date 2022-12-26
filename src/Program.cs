using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

// Cross-origin resource requests from these clients are permitted.
string[]  _originAllowList = {
    "http://localhost:5091",
    "https://localhost:5091"
    };

// Add services to the container.
builder.Services.AddCors(options =>
{
    // Add a CORS policy that tells the API to include response
    // headers informing browsers that the calls are permitted.
    options.AddPolicy(name: "OriginAllowList",
                      policy  =>
                      {
                          policy.WithOrigins(_originAllowList);
                      });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // For Swagger/OpenAPI UI (see https://aka.ms/aspnetcore/swashbuckle)
builder.Services.AddSwaggerGen();           // For Swagger/OpenAPI UI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("OriginAllowList");
app.UseAuthorization();

app.MapControllers();

app.Run();
