namespace ApiClima
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myAllowSpecificOrigins = "_loremIpsum";

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy
                                        .WithOrigins("http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                                  });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.UseCors(myAllowSpecificOrigins);

            app.Run();
        }
    }
}