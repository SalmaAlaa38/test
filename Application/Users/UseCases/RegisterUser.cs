using Application.Contracts;
using Domain.Utilties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Users.UserDtos;

namespace Application.Users.UseCases
{
    internal class RegisterUser
        (
        ITokenProvider tokenProvider,
        IUserManager userManager
        )
    {
        public record Request(RegisterUserDto user , HttpContext context); 

        public async Task<Result> Handle(Request request)
        {
            var user  = await userManager.GetUserByEmailAsync(request.user.Email);
            if(user is not null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "User already exists"
                };
            }
            user = new Domain.Entities.User
            {
                Email = request.user.Email,
                PasswordHash = userManager.HashPasswordAsync(request.user.Password),
                Username = request.user.Username,
                Phone = request.user.Phone,
                Role =  "USER"
            };
            var createdUser=  await userManager.InsertUserAsync(user);
            var refreshToken =  tokenProvider.GenerateRefreshToken();
            request.context.Response.Cookies.Append("refresh",
                refreshToken.token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite =SameSiteMode.Strict,
                    Expires = refreshToken.Expiration
                });

            return new Result
            {
                IsSuccess = true,
                Message =  "User created successfully",
                Data = new
                {
                    accessToken = tokenProvider.GenerateAccessToken(createdUser),
                    refreshTokenExpiration = refreshToken.Expiration
                }
            };
        }
    }
}
