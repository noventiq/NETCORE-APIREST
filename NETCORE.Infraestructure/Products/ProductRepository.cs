using NETCORE.Domain.Products.Domain;
using NETCORE.Domain.Products.Interfaces;
using NETCORE.Shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NETCORE.Domain.Common.Interfaces;
using Dapper;

namespace NETCORE.Infraestructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICustomConnection _connection;
        public ProductRepository(ICustomConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Product>> List()
        {
            IEnumerable<Product> lista;

            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    lista = await scope.QueryAsync<Product>("SELECT * FROM Products", commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Error al listar productos", ex);
                }
            }

            return lista;
        }
    }
}
