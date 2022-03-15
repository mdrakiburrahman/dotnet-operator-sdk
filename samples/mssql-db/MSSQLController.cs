using System;
using System.Threading.Tasks;
using ContainerSolutions.OperatorSDK;
using NLog;

namespace mssql_db
{
	// This is our Controller Class
	class MSSQLController
	{
		// Initiates logger
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		// Controller entrypoint
		static void Main(string[] args)
		{
			try
			{
				string k8sNamespace = "default";
				if (args.Length > 1) // If an arg was passed in it is namespace, so override it
					k8sNamespace = args[0];

				Controller<MSSQLDB>.ConfigLogger();

				Log.Info($"=== {nameof(MSSQLController)} STARTING for namespace {k8sNamespace} ===");

				// Our main CRD Interface
				MSSQLDBOperationHandler handler = new MSSQLDBOperationHandler();

				// Initiates the Contorller for the CRD
				Controller<MSSQLDB> controller = new Controller<MSSQLDB>(new MSSQLDB(), handler, k8sNamespace);

				// Initiates controller in Async, there's a typo in the SDK - should be StartAsync
				Task reconciliation = controller.SatrtAsync();

				Log.Info($"=== {nameof(MSSQLController)} STARTED ===");

				// Continues to run tasks
				// https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.configureawait
				reconciliation.ConfigureAwait(false).GetAwaiter().GetResult();

			}
			catch (Exception ex)
			{
				Log.Fatal(ex);
					throw;
			}
			finally
			{
				// If controller is killed
				Log.Warn($"=== {nameof(MSSQLController)} TERMINATING ===");
			}
		}
	}
}
