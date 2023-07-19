﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Knife4jUIDemo.Controllers
{
    /// <summary>
    /// 中文这是一个Get请求这是一个Get请求
    /// </summary>
    [ApiController]
    [Route("api/WeatherForecast/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 得到一个ErrorCode
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ErrorCode GetErrorCode()
        {
            return ErrorCode.Success;
        }

        [HttpGet]
        public ErrorCode GetErrorCode2(ErrorCode errorCode)
        {
            return errorCode;
        }

        [HttpGet]
        public IActionResult GetErrorCode4(ErrorCode errorCode)
        {
            return new JsonResult(new PostErrorCodeDto() { Message="a",ErrorCode=errorCode});
        }

        /// <summary>
        /// 发送一个Post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PostErrorCodeDto PostErrorCode([FromBody] PostErrorCodeDto PostErrorCodeDto)
        {
            return PostErrorCodeDto;
        }

        /// <summary>
        /// 这是一个Get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 发送一个Post
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateUserRequest")]
        public UserRequest<TheyReqDto> CreateUserRequest([FromBody] UserRequest<TheyReqDto> req)
        {
            return req;
        }

    }

    /// <summary>
    /// TheyReqDto
    /// </summary>
    public sealed class TheyReqDto
    {
        /// <summary>
        /// Hobbies
        /// </summary>
        public string Hobbies { get; set; } = string.Empty;

        /// <summary>
        /// Behavior
        /// </summary>
        public string Behavior { get; set; } = string.Empty;
    }

    public class UserRequest<TRequest> where TRequest : class
    {
        /// <summary>
        /// RequestId
        /// </summary>
        public string RequestId { get; set; } = string.Empty;

        /// <summary>
        /// Args
        /// </summary>
        public TRequest Args { get; set; }
    }

    /// <summary>
    /// 请求实体
    /// </summary>
    public class PostErrorCodeDto
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
    }
}
