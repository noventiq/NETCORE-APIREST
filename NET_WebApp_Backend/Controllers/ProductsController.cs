using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET_WebApp_Backend.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace NET_WebApp_Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            this._configuration = configuration;
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
        [Route("crear")]
        public async Task<ActionResult> Create([FromBody] Product product) {
            return Ok();
        }

        [HttpPut]
        [Route("actualiza/{id}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody] Product product) {
            return Ok();
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<ActionResult> Delete([FromRoute]int id) {
            return Ok();
        }
    }
}
