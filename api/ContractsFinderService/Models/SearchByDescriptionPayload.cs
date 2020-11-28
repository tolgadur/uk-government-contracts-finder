//-----------------------------------------------------------------------------------------------------------
// <copyright file="ContractsFinderController.cs">
//  Copyright (c) Tolga Hasan Dur. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------------------------------


namespace api.ContractsFinderService.Models.ApiModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="SearchByDescriptionPayload" />.
    /// </summary>
    public class SearchByDescriptionPayload
    {
        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [JsonProperty("Description", Required = Required.Always)]
        public string Description { get; set; }
    }
}
