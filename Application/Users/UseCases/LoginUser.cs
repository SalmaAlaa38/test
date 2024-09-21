
using Application.Contracts;
using Domain.Utilties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using static Application.Users.UserDtos;

namespace Application.Users.UseCases
{
    public class LoginUser
        (
        IUserManager userManager,
        ITokenProvider tokenProvider
        )
    {
        public record Request(LoginUserDto user , HttpContext context);

        public async Task<Result> Handle(Request request)
        {
            var user = await userManager.GetUserByEmailAsync(request.user.Email);
            if (user is null ||
                !userManager.CheckPasswordAsync( request.user.Password , user.PasswordHash))
            {
                return new Result
                {
                    IsSuccess = false,
                    Message =  "User not found"
                };
            }
            if (user.RefreshTokens.Any(rt=>rt.IsActive))
            {
                user.RefreshTokens.Single(rt => rt.IsActive).RevokedOn = DateTime.Now;
            }

            var accessToken = tokenProvider.GenerateAccessToken(user);
            var RefreshToken = tokenProvider.GenerateRefreshToken();
            user.RefreshTokens.Add(RefreshToken);
            await userManager.SaveChanges();
            request.context.Response.Cookies.Append("refreshToken", RefreshToken.token
                , new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite= SameSiteMode.Strict,
                    Expires = RefreshToken.Expiration
                });
            return new Result
            {
                IsSuccess = true,
                Data =new
                {
                    accessToken ,
                    refreshTokenExpiration = RefreshToken.Expiration
                }
            };
        }
    }
}
