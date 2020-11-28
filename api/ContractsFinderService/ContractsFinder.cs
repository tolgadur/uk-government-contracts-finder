//-----------------------------------------------------------------------------------------------------------
// <copyright file="ContractsFinder.cs">
//  Copyright (c) Tolga Hasan Dur. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------------------------------



namespace api.ContractsFinderService
{
    using api.ContractsFinderService.Models;
    using api.ContractsFinderService.Models.ApiModels;
    using app.PaymentGatewayService;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Defines the <see cref="ContractsFinder" />.
    /// </summary>
    public class ContractsFinder
    {
        /// <summary>
        /// Contract Finder api endpoint
        /// </summary>
        private static string ApiEndpoint = "https://www.contractsfinder.service.gov.uk/Published/Notices/OCDS/Search";

        /// <summary>
        /// This endpoint will search for a contract with matching description.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns>
        /// The <see cref="Task{IActionResult}" />.
        /// </returns>
        public static IActionResult SearchByDescription(SearchByDescriptionPayload payload)
        {
            try
            {
                // fetch yesterdays contracts and then search
                FetchNewContractsDaily();
                var contracts = ConnectionHelper.SearchByDescription(payload.Description);

                return new OkObjectResult(new SearchByDescriptionResponse().Map(contracts));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }

        /// <summary>
        /// This method will fetch new contracts from the last month from the contracts api and save them in the database.
        /// </summary>
        public static void FetchNewContractsYearly()
        {
            var publishedFrom = DateTime.Now.AddMonths(-1);
            var publishedTo = DateTime.Now;
            var page = 1;
            List<Contract> contracts = GetContractsFromGovApi(publishedFrom, publishedTo, page);
            while (contracts.Count > 0)
            {
                SaveContractsInDB(contracts);
                page += 1;
                contracts = GetContractsFromGovApi(publishedFrom, publishedTo, page);
            }
        }

        /// <summary>
        /// This method will fetch new contracts from today from the contracts api and save them in the database.
        /// </summary>
        private static void FetchNewContractsDaily()
        {
            var publishedFrom = DateTime.Now.AddDays(-1);
            var publishedTo = DateTime.Now;
            var page = 1;
            List<Contract> contracts = GetContractsFromGovApi(publishedFrom, publishedTo, page);
            while (contracts.Count > 0)
            {
                SaveContractsInDB(contracts);
                page += 1;
                contracts = GetContractsFromGovApi(publishedFrom, publishedTo, page);
            }
        }

        /// <summary>
        /// This method will fetch new contracts the government api.
        /// </summary>
        private static List<Contract> GetContractsFromGovApi(DateTime publishedFrom, DateTime publishedTo, int page)
        {
            // construct request uri
            var uriBuilder = new UriBuilder();
            var paramValues = HttpUtility.ParseQueryString(uriBuilder.Query);
            paramValues.Add("publishedFrom", publishedFrom.ToString("yyyy-MM-dd"));
            paramValues.Add("publishedTo", publishedTo.ToString("yyyy-MM-dd"));
            paramValues.Add("page", page.ToString());
            paramValues.Add("size", "100");
            paramValues.Add("orderBy", "publishedDate");
            paramValues.Add("order", "DESC");
            paramValues.Add("stages", "award");
            uriBuilder.Query = Uri.EscapeUriString(HttpUtility.UrlDecode(paramValues.ToString()));

            // make http request
            List<Contract> contractsResponses = new List<Contract>();
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(ApiEndpoint + uriBuilder.Query);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            // deserilize response
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            ContractFinderApiResponse responseBody = JsonConvert.DeserializeObject<ContractFinderApiResponse>(reader.ReadToEnd());

            foreach (var res in responseBody.Results)
            {
                contractsResponses.AddRange(Contract.Map(res));
            }

            return contractsResponses;
        }

        /// <summary>
        /// Saves a list of contract in the database
        /// </summary>
        private static void SaveContractsInDB(List<Contract> contracts)
        {
            foreach(var contract in contracts)
            {
                ConnectionHelper.SaveContract(contract);
            }
        }

    }
}
