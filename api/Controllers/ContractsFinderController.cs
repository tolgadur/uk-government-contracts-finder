//-----------------------------------------------------------------------------------------------------------
// <copyright file="ContractsFinderController.cs">
//  Copyright (c) Tolga Hasan Dur. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------------------------------



namespace api.Controllers
{
    using api.ContractsFinderService;
    using api.ContractsFinderService.Models.ApiModels;
    using app.PaymentGatewayService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("contracts")]
    public class ContractsFinderController : ControllerBase
    {
        public ContractsFinderController(IConfiguration configuration)
        {
            ConnectionHelper.Init(configuration);
        }

        /// <summary>
        /// Searches contracts that match by description
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost("search")]
        public IActionResult SearchByDescription(SearchByDescriptionPayload payload)
        {
            return ContractsFinder.SearchByDescription(payload);
        }

        /// <summary>
        /// Saves the previous years contracts in the database.
        /// </summary>
        [HttpGet("save/year")]
        public IActionResult SaveLastYearsContracts()
        {
            ContractsFinder.FetchNewContractsYearly();
            return Ok("curl --location --request GET 'https://localhost:44382/contracts/save/year'");
        }

        /// <summary>
        /// Short message displayed for debugging purposes.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Api is running.");
        }
    }
}
