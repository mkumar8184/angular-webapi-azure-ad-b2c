using AuthenticatioService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.
    AddAuthService(   
    builder.Configuration
    ).
    InjectOtherSerives();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials
                                  // app.UseCors("CorsPolicy");  
app.UseHttpsRedirection();
app.UseAuthentication();
HttpContextHandler.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
app.UseAuthorization();

app.MapControllers();

app.Run();
