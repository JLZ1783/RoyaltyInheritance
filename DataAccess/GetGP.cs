using Dapper;
using DynamicsGPAddin1.Models;
using Microsoft.Dexterity.Applications;
using Microsoft.Dexterity.Bridge;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Dexterity;

namespace DynamicsGPAddin1.DataAccess
{

	public class GetGP
	{

		public List<T> LoadData<T, U>(string sqlStatement, U parameters, SqlConnection sql)
		{

			using (IDbConnection connection = sql)
			{


				List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();

				return rows;

			}

		}
		public void SaveData<T>(string sqlStatement, T parameters, SqlConnection sql)
		{
			using (IDbConnection connection = sql)
			{
				connection.Execute(sqlStatement, parameters);

			}
		}

	}
}


