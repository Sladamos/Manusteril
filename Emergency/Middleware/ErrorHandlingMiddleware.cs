using Emergency.Config;
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

        public ErrorHandlingMiddleware(ProdConfig prodConfig)
        {
            this.prodConfig = prodConfig;
        }

        public void Invoke(Action task)
        {
            try
            {
                if (!prodConfig.IsProductionMode)
                {
                    Console.WriteLine("Uruchomienie warstwy łapiącej błędy");
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
                Console.WriteLine(errorCommunicate);
            }
        }
    }
}
