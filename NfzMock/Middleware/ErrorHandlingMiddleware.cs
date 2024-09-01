using NfzMock.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NfzMock.Middleware
{
    internal class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ProdConfig prodConfig;
        private readonly ILog logger;


        public ErrorHandlingMiddleware(ProdConfig prodConfig, ILog logger)
        {
            this.prodConfig = prodConfig;
            this.logger = logger;
        }

        public async Task Invoke(Func<Task> task)
        {
            try
            {
                logger.Info("Uruchomienie warstwy łapiącej błędy");
                await task();
            }
            catch (Exception ex)
            {
                string errorCommunicate = $"Wystąpił niespodziewany błąd! {ex.Message}";
                if (!prodConfig.IsProductionMode)
                {
                    errorCommunicate += $" {ex.StackTrace}";
                }
                logger.Error(errorCommunicate);
            }
        }
    }
}
