using System.Net;
using System.Text.Json;

namespace TaskSistem.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Ocorreu um erro interno no servidor.";

            switch (exception)
            {
                case ArgumentException:
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    message = exception.Message;
                    break;

                // Erros de falta de permissão (aquele do nosso TaskService!)
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized; // 401
                    message = exception.Message;
                    break;

                // Erros de registro não encontrado
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404
                    message = exception.Message;
                    break;

                // Se você disparou um throw new Exception("texto") genérico
                case Exception:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    message = exception.Message;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            // Cria o corpo do JSON que o front-end vai ler
            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = message,
                detailed = exception.InnerException?.Message // Opcional: ajuda a debugar relacionamentos do EF
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}