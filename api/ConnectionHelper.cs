//-----------------------------------------------------------------------------------------------------------
// <copyright file="ConnectionHelper.cs">
//  Copyright (c) Tolga Hasan Dur. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------------------------------


namespace app.PaymentGatewayService
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Data.SQLite;
    using System.Data;
    using Dapper;
    using api.ContractsFinderService.Models.ApiModels;

    public static class ConnectionHelper
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private static IConfiguration _configuration;

        /// <summary>
        /// Initializes the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Init(IConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>
        /// This will save a payment in the database.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns> The Connectionstring </returns>
        public static void SaveContract(Contract contract)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT OR REPLACE INTO Contracts (Id, Title, PublishedDate, OrganisationName, NoticeType, DeadlineDate, AwardedDate, Description) VALUES " +
                    "(@Id, @Title, @PublishedDate, @OrganisationName, @NoticeType, @DeadlineDate, @AwardedDate, @Description)", contract);
            }
        }

        /// <summary>
        /// This will return the matching contracts.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns> The Connectionstring </returns>
        public static List<Contract> SearchByDescription(string keyword)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                return cnn.Query<Contract>(String.Format("SELECT * FROM Payments WHERE {0} IN Description", keyword), new DynamicParameters()).ToList();
            }
        }

        /// <summary>
        /// This will delete the contract with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns> The Connectionstring </returns>
        public static void DeleteContract(string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(String.Format("DELETE FROM Contracts WHERE Id='{0}'", id));
            }
        }

        /// <summary>
        /// This will return the Connectionstring from our config file.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns> The Connectionstring </returns>
        private static string LoadConnectionString(string id = "Default")
        {
            return _configuration.GetConnectionString(id);
        }
    }
}
