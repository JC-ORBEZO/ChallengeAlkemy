﻿using ChallengeAlkemy.Models;
using ChallengeAlkemy.ViewModels.Auth;
using ChallengeAlkemy.ViewModels.Auth.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeAlkemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Post(RegisterRequestModel model)
        {
            var userExist=await _userManager.FindByNameAsync(model.Username);
            if (userExist != null) return BadRequest("El nombre ingresado yae xiste.");
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                IsActive = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new
                    {
                    Status="Error",
                    Message=$"User Creation Failed Errors: {string.Join(',', result.Errors.Select(x => x.Description))}"
                });
            }
            return Ok(new
            {
                Status="Success",
                Message=$"User Created Successfully"
            });
                
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Post(LoginRequestViewModel model)
        {
            var result =await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(model.Username);
                if (currentUser.IsActive)
                {
                    //generar token
                    //devolver token
                    return Ok(await GetToken(currentUser));
                }
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new
            {
                Status = "Error",
                Message = $"El Usuario {model.Username} no está autorizado."
            });
        }

        private async Task<LoginResponseViewModel> GetToken(User currentUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLargaDeAUTORIZACION"));
            var token= new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return new LoginResponseViewModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }


}
