using Application.Contracts;
using Domain.Utilties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

public class GetRefreshToken
    (
    IUserManager userManager ,
    ITokenProvider tokenProvider
    )
{
    public record Request(string UserId, HttpContext context);
    public async Task<Result> Handle(Request request)
    {
        var user = await userManager.GetUserByIdAsync(request.UserId);
        var refreshTokenFromCookie = request.context
            .Request.Cookies["refreshToken"];
        if (user is null || refreshTokenFromCookie is null)
        {
            return new Result
            {
                IsSuccess = false,
                Message = "User not found"
            };
        }
        var activeToken = user.RefreshTokens
            .SingleOrDefault(rf => rf.IsActive &&
            rf.token ==  refreshTokenFromCookie);
        if (activeToken == null )
        {
            return new Result
            {
                IsSuccess = false,
                Message = "Invalid or expired refresh token"
            };
        }
        activeToken.RevokedOn = DateTime.Now;
        var newToken = tokenProvider.GenerateRefreshToken();
        request.context.Response.Cookies.Append("refreshToken", newToken.token, new CookieOptions
        {
            SameSite = SameSiteMode.Strict,
            Expires = newToken.Expiration,
            HttpOnly = true,
            Secure = true
        });
        user.RefreshTokens.Add(newToken);
        await userManager.SaveChanges();
        return new Result
        {
            IsSuccess = true,
            Data = new
            {
                AccessToken = tokenProvider.GenerateAccessToken(user),
                RefreshTokenExpiration = newToken.Expiration
            }
        };
    }
}