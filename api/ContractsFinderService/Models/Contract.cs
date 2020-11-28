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
        /// Maps the jobject result.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// The <see cref="Contract" />.
        /// </returns>
        public Contract Map(JToken contract)
        {
            this.Title = contract["releases"]["tender"]["title"].ToString();
            this.PublishedDate = DateTime.Parse(contract["publishedDate"].ToString());
            this.OrganisationName = contract["publisher"]["name"].ToString();
            this.DeadlineDate = DateTime.Parse(contract["releases"]["tender"]["tenderPeriod"]["endDate"].ToString());
            this.AwardedDate = DateTime.Parse(contract["releases"]["awards"]["date"].ToString());
            this.Description = contract["releases"]["tender"]["description"].ToString();
            return this;
        }
    }
}
