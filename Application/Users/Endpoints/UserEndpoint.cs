
using Application.Users.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static Application.Users.UserDtos;


namespace Application.Users.Endpoints
{
    public static class UserEndpoint
    {
        public static IEndpointRouteBuilder MapUserEndpoints
            (this  IEndpointRouteBuilder Routes)
        {
            var Route = Routes.MapGroup("api/users");
            Route.MapPost("login", async ([FromBody]LoginUserDto user,
                [FromServices] LoginUser Handler,HttpContext context) =>
            {
                var request = new LoginUser.Request(user, context);
                var result = await Handler.Handle(request);
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Message);
            
                }
                return Results.Ok(new
                {
                    result.Data,
                    result.Message
                });
            });

            Route.MapPost("register", async([FromBody] RegisterUserDto user,
                [FromServices] RegisterUser Handler ,HttpContext context) =>
            {
                var request = new RegisterUser.Request(user, context);
                var result = await Handler.Handle(request);
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Message);

                }
                return Results.Ok(new
                {
                    result.Data,
                    result.Message
                });
            });

            Route.MapGet("refresh-token/{UserId}" ,async (
                [FromRoute] string UserId ,HttpContext context,
                [FromServices] GetRefreshToken Handler) =>
            {
                var request  = new GetRefreshToken.Request(UserId ,context);
                var result = await Handler.Handle(request);
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(result.Message);
                }
                return Results.Ok(result.Data);
            });


            Route.MapGet("logout", async (HttpContext context) =>
            {
                context.Response.Cookies.Delete("refreshToken");
                return Results.Ok("Logout Successful");
            });
            Route.MapGet("forget-password", async () =>
            {

            });
            return Routes;
        }
    }
}
