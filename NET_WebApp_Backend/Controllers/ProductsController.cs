using Dapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_WebApp_Backend.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;

namespace NET_WebApp_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IConfiguration _configuration;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IConfiguration configuration,
            IWebHostEnvironment environment,
            ILogger<ProductsController> logger
            )
        {
            this._configuration = configuration;
            this._hostingEnvironment = environment;
            this._logger = logger;
            this._logger.LogInformation("Ingresó a controller {0}", this.GetType().Name);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult> List()
        {
            Product producto = new();

            IEnumerable<Product> listado;

            using (SqlConnection connection = new SqlConnection(this._configuration.GetConnectionString("SqlConnection")))
            {
                listado = await connection.QueryAsync<Product>("SELECT * FROM Products");
            }


            return Ok(listado);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create([FromBody] Product product)
        {

            ProductValidator validator = new ProductValidator();

            ValidationResult result = validator.Validate(product);
            if (!result.IsValid)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, null);
            }

            return Ok();
        }

        [HttpPut]
        [Route("actualiza/{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] Product product)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            return Ok();
        }


        [HttpPost]
        [Route("{id}/foto")]
        public async Task<ActionResult> UploadPhoto([FromRoute] int id, IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "Uploadsx");

            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    catch (Exception ex)
                    {
                        this._logger.LogError(ex, "archivo: {0}", file.FileName);
                        return StatusCode(StatusCodes.Status500InternalServerError, ex);
                    }
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("{id}/fotos")]
        public async Task<ActionResult> UploadPhoto([FromRoute] int id, IList<IFormFile> files)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("product-multimedia")]
        public async Task<ActionResult> UploadPhotoAndData([FromRoute] int id, [FromForm] ProductMultimedia productMultimedia)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            Product product = new Product();
            product.Id = productMultimedia.Id;
            product.Title = productMultimedia.Title;

            foreach (IFormFile file in productMultimedia.Images)
            {
                if (file.Length > 0)
                {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return Ok();
        }
    }
}
