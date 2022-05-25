namespace SyncLib.WebApp.Models.Extensions
{
    public class AppJwtAuthenticationMiddleware
    {
        private readonly RequestDelegate Next;

        public AppJwtAuthenticationMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var loggedInUser = context.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(loggedInUser))
                context.Response.Redirect("/System/Login");
            else
                await Next.Invoke(context);
        }
    }

    public static class AppJwtAuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder AuthorizeApp(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppJwtAuthenticationMiddleware>();

        }
    }

    public class AppJwtAuthenticationMiddlewarePipeline
    {
        public void Configure(IApplicationBuilder app)
        {
            app.AuthorizeApp();
        }
    }
}