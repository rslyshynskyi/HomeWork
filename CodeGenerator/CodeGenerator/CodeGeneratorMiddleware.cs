//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;

//namespace CodeGenerator
//{
//    public class CodeGeneratorMiddleware
//    {
//        private readonly RequestDelegate _next;
//        CodeGeneratorService _codeService;

//        public CodeGeneratorMiddleware(RequestDelegate next, CodeGeneratorService codeService)
//        {
//            _next = next;
//            _codeService = codeService;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            if (context.Request.Path.Value.ToLower() == "/api/code")
//            {
//                context.Response.ContentType = "text/html; charset=utf-8";
//            }
//            else
//            {
//                await _next.Invoke(context);
//            }
//        }
//    }

//    public static class CodeGeneratorExtensions
//    {
//        public static IApplicationBuilder UseCodeGenerator(this IApplicationBuilder builder)
//        {
//            var routeBuilder = new Microsoft.AspNetCore.Routing.RouteBuilder(builder);

//            return builder.UseMiddleware<CodeGeneratorMiddleware>();
//        }
//    }
//}
