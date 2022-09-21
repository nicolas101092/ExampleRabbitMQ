namespace ApiPublisher.Infrastructure.Startup
{
    /// <summary>
    /// Class for configure the pipeline of the api execution
    /// </summary>
    public static class WebApplicationExtension
    {
        /// <summary>
        /// method for configure the services of the api
        /// </summary>
        /// <param name="app">a object WebApplication to configure the pipeline</param>
        public static void Configure(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
