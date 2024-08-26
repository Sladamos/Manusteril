using Emergency.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency.Middleware
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

        public void Invoke(Action task)
        {
            try
            {
                if (!prodConfig.IsProductionMode)
                {
                    logger.Info("Uruchomienie warstwy łapiącej błędy");
                    logger.Error("Ale bomba to działa");
                }
                task();
            }
            catch (Exception ex)
            {
                string errorCommunicate = "Wystąpił niespodziewany błąd!";
                if (!prodConfig.IsProductionMode)
                {
                    errorCommunicate += $" {ex.Message}";
                }
                logger.Error(errorCommunicate, ex);
            }
        }
    }
}
