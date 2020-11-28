//-----------------------------------------------------------------------------------------------------------
// <copyright file="ContractFinderApiResponse.cs">
//  Copyright (c) Tolga Hasan Dur. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------------------------------



namespace api.ContractsFinderService.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Defines the <see cref="ContractFinderApiResponse" />.
    /// </summary>
    public class ContractFinderApiResponse
    {
        /// <summary>
        /// Gets or sets the api results.
        /// </summary>
        [JsonProperty("results", Required = Required.Always)]
        public List<Result> Results { get; set; }

    }

    /// <summary>
    /// Defines the <see cref="Results" />.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets the releases.
        /// </summary>
        [JsonProperty("releases", Required = Required.Always)]
        public List<Releases> Releases { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        [JsonProperty("publisher", Required = Required.Always)]
        public Publisher Publisher { get; set; }

        /// <summary>
        /// Gets or sets the published date.
        /// </summary>
        [JsonProperty("publishedDate", Required = Required.Always)]
        public DateTime publishedDate { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="Publisher" />.
    /// </summary>
    public class Publisher
    {
        /// <summary>
        /// Gets or sets the publisher name.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="Releases" />.
    /// </summary>
    public class Releases
    {
        /// <summary>
        /// Gets or sets tender.
        /// </summary>
        [JsonProperty("tender", Required = Required.Always)]
        public Tender Tender { get; set; }

        /// <summary>
        /// Gets or sets the awards.
        /// </summary>
        [JsonProperty("awards", Required = Required.Always)]
        public List<Awards> awards { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="Awards" />.
    /// </summary>
    public class Awards
    {
        /// <summary>
        /// Gets or sets the awards date
        /// </summary>
        [JsonProperty("date", Required = Required.Always)]
        public DateTime Date { get; set; }
    }


    /// <summary>
    /// Defines the <see cref="Tender" />.
    /// </summary>
    public class Tender
    {
        /// <summary>
        /// Gets or sets the tender title.
        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }


        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }


        /// <summary>
        /// Gets or sets the tender period.
        /// </summary>
        [JsonProperty("tenderPeriod", Required = Required.Always)]
        public TenderPeriod TenderPeriod { get; set; }
    }


    /// <summary>
    /// Defines the <see cref="TenderPeriod" />.
    /// </summary>
    public class TenderPeriod
    {
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [JsonProperty("endDate", Required = Required.Always)]
        public DateTime EndDate { get; set; }
    }
}
