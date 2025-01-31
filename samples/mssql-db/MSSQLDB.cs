﻿using ContainerSolutions.OperatorSDK;

namespace mssql_db
{
	/// <summary>
	/// Class extends the BaseCRD from Operator SDK to add the MSSQLDB CRD
	/// </summary>
	public class MSSQLDB : BaseCRD
	{
		/// <summary>
		/// Constructor initiates instance of MSSQLDB class
		/// This implements the BaseCRD class with:
		/// Group: mssql-db
		/// Version: v1
		/// Plural: mssqldbs
		/// Singular: mssqldb
		/// ReconciliationCheckInterval: 5 (default) - but we can override from here
		/// </summary>
		public MSSQLDB() :
			base("samples.k8s-dotnet-controller-sdk", "v1", "mssqldbs", "mssqldb")
		{ }

		// CRD contains the spec Class
		public MSSQLDBSpec Spec { get; set; }
		
		// Overrides spec equality comparison
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			return ToString().Equals(obj.ToString());
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return Spec.ToString();
		}

	}
	/// <summary>
	/// 1:1 with CRD Spec
	/// </summary>
	public class MSSQLDBSpec
	{
		public string DBName { get; set; }

		public string ConfigMap { get; set; }

		public string Credentials { get; set; }
		
		// Overrides string conversion
		public override string ToString()
		{
			return $"{DBName}:{ConfigMap}:{Credentials}"; 
		}
	}
}
