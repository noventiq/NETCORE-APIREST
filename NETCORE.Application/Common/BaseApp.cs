using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NETCORE.Shared;

namespace NETCORE.Application.Common
{
    public class BaseApp<T>
    {
        public readonly ILogger<BaseApp<T>> _logger;
        private readonly string _connectionString;
        private readonly IConfiguration _config;
        public BaseApp(ILogger<BaseApp<T>> logger)
        {
            _logger = logger;
        }

        public BaseApp(ILogger<BaseApp<T>> logger, IConfiguration configuracion)
        {
            _logger = logger;
            _config = configuracion;
            _connectionString = _config.GetConnectionString("SqlConnection");
        }

        protected async Task<StatusResponseSimple> simpleProcess(Func<Task> callback, string titulo)
        {
            var response = new StatusResponseSimple(true, "");

            try
            {
                await callback();

                response.Success = true;
                response.Title = titulo;
            }
            catch (CustomException customEx)
            {
                this._logger.LogError(customEx, "TraceId: {0}", response.TraceId);
                response.Success = false;
                response.Title = customEx.Titulo;
                response.Detail = customEx.ToString();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "TraceId: {0}", response.TraceId);
                response.Success = false;
                response.Title = "Sucedió un error inesperado.";
                response.Detail = ex.ToString();
            }

            return response;
        }

        protected async Task<StatusResponse<T>> complexProcess<T>(Func<Task<T>> callbackData, string titulo = "")
        {
            var response = new StatusResponse<T>(true, "");

            try
            {
                response.Data = await callbackData();


                response.Success = true;
                response.Title = titulo;
            }
            catch (CustomException customEx)
            {
                this._logger.LogError(customEx, "TraceId: {0}", response.TraceId);
                response.Success = false;
                response.Title = customEx.Titulo;
                response.Detail = customEx.ToString();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "TraceId: {0}", response.TraceId);
                response.Success = false;
                response.Title = "Sucedió un error inesperado.";
                response.Detail = ex.ToString();
            }

            return response;
        }

        public Dictionary<string, List<string>> GetErrors(List<ValidationFailure> errors)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach (ValidationFailure failure in errors)
            {
                List<string> errorsList = null;
                if (!result.TryGetValue(failure.PropertyName, out errorsList))
                {
                    errorsList = new List<string>();
                    result[failure.PropertyName] = errorsList;
                }

                errorsList.Add(failure.ErrorMessage);
            }
            return result;
        }
    }
}
