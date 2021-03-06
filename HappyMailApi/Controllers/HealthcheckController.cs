﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Text;
using HappyMailApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace HappyMailApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthcheckController : ControllerBase
    {
        private readonly IMongoDatabase _mongoDatabase;

        public HealthcheckController(IDatabaseSettings databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("HappyMail 💛📧 API is up and running");
            stringBuilder.AppendLine($"UTC time: ({DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)})");

            if (!string.IsNullOrWhiteSpace(User.Identity.Name)) 
            {
                stringBuilder.AppendLine($"Your username: {User.Identity.Name}");
            }

            var isMongoAlive = false;
            try
            {
                isMongoAlive =  _mongoDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(5000);
            }
            catch (Exception) { }

            stringBuilder.AppendLine(isMongoAlive ? $"MongoDb is alive! ✔️" : $"MongoDb is dead! 💀");

            return Ok(stringBuilder.ToString());
        }
    }
}
