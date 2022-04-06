using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDapperCrudDemo.Domain;

namespace AspNetCoreDapperCrudDemo.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
