//-----------------------------------------------------------------------------------------------------------
// <copyright file="ContractsFinderController.cs">
//  Copyright (c) Tolga Hasan Dur. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------------------------------


namespace api.ContractsFinderService.Models.ApiModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Contract" />.
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty("Title", Required = Required.Always)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        [JsonProperty("PublishedDate", Required = Required.Always)]
        public DateTime PublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the organisation name.
        /// </summary>
        [JsonProperty("OrganisationName", Required = Required.Always)]
        public string OrganisationName { get; set; }

        /// <summary>
        /// Gets or sets the deadline date.
        /// </summary>
        [JsonProperty("DeadlineDate", Required = Required.Always)]
        public DateTime DeadlineDate { get; set; }

        /// <summary>
        /// Gets or sets the awarded date.
        /// </summary>
        [JsonProperty("AwardedDate", Required = Required.Always)]
        public DateTime AwardedDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("Description", Required = Required.Always)]
        public string Description { get; set; }

        /// <summary>
        /// Maps the contracts.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// The <see cref="Contract" />.
        /// </returns>
        public static List<Contract> Map(Result contract)
        {
            List<Contract> contracts = new List<Contract>();
            foreach(var release in contract.Releases)
            {
                var newContract = new Contract();
                newContract.OrganisationName = contract.Publisher.Name;
                newContract.PublishedDate = contract.publishedDate;
                newContract.Title = release.Tender.Title;
                newContract.Description = release.Tender.Description;
                newContract.AwardedDate = release.awards[0].Date;
                newContract.DeadlineDate = release.Tender.TenderPeriod.EndDate;
                contracts.Add(newContract);
            }

            return contracts;
        }
    }
}
