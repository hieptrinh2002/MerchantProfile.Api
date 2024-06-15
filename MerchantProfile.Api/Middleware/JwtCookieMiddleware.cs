namespace MerchantProfile.Api.Middleware
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cookie = context.Request.Cookies["merchant_jwt"];
            if (!string.IsNullOrEmpty(cookie))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + cookie);
            }

            else
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\": \"Unauthorized: Invalid Jwt .\"}");
                return;
            }

            await _next(context);
        }
    }
}
