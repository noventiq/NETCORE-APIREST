using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NETCORE.Application.Common;
using NETCORE.Domain.Products.Domain;
using NETCORE.Domain.Products.DTO;
using NETCORE.Domain.Products.Interfaces;
using NETCORE.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NETCORE.Application.Products
{
    public class ProductApp : BaseApp<ProductApp>
    {
        private readonly IProductRepository _productRepository;

        public ProductApp(IProductRepository productRepository, ILogger<BaseApp<ProductApp>> logger) : base(logger)
        {
            _productRepository = productRepository;
        }

        public async Task<StatusResponse<IEnumerable<Product>>> List()
        {
            return await this.complexProcess(() => _productRepository.List(), "");
            //StatusResponse<IEnumerable<Product>> status = await this.complexProcess(() => _productRepository.List(), "");
            //return status;
            //IEnumerable<Product> lista = null;
            //try
            //{
            //    lista = await _productRepository.List();
            //}
            //catch (Exception ex)
            //{
            //    this._logger.LogError(ex, "");
            //    throw;
            //}

            //return new StatusResponse<IEnumerable<Product>>(true, "") { Data = lista };
        }

        public async Task<StatusResponse<Product>> Create(RequestProduct product)
        {
            StatusResponse<Product> status = new StatusResponse<Product>(true, "");

            RequestProductValidator validator = new RequestProductValidator();
            ValidationResult result = validator.Validate(product);

            if (!result.IsValid)
            {
                this._logger.LogError("{0}, {1}", status.TraceId, JsonSerializer.Serialize(product));

                status.Success = false;
                status.Title = "Revise los datos ingresado e intente de nuevo";
                status.Errors = this.GetErrors(result.Errors);
            }

            status.Data = new Product();

            return status;
        }
    }
}
