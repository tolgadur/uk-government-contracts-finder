//-----------------------------------------------------------------------------------------------------------
// <copyright file="SearchByDescriptionResponse.cs">
//  Copyright (c) Tolga Hasan Dur. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------------------------------


namespace api.ContractsFinderService.Models.ApiModels
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SearchByDescriptionPayload" />.
    /// </summary>
    public class SearchByDescriptionResponse
    {
        /// <summary>
        /// Gets or sets the matching contracts
        /// </summary>
        [JsonProperty("MatchingContracts", Required = Required.Always)]
        public List<Contract> MatchingContracts { get; set; }

        /// <summary>
        /// Maps the list of contracts to the response json.
        /// </summary>
        /// <param name="contracts">The contracts.</param>
        /// <returns>
        /// The <see cref="SearchByDescriptionResponse" />.
        /// </returns>
        public SearchByDescriptionResponse Map(List<Contract> contracts)
        {
            this.MatchingContracts = contracts;
            return this;
        }

    }
}
