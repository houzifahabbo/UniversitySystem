﻿using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using University.API.Filters;
using University.API.Helpers;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public AuthController(IAuthService authService, IJwtTokenHelper jwtTokenHelper)
        {
            _authService = authService;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> Login([FromBody] LoginForm form)
        {
            var user = await _authService.Login(form);
            var token = _jwtTokenHelper.GenerateToken(user);
            return new ApiResponse(token, HttpStatusCode.OK);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ApiResponse> Regiter([FromBody] RegisterForm form)
        {
            var dto = await _authService.Register(form);
            return new ApiResponse(dto);
        }
    }
}
