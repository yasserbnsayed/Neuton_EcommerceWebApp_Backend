using Application.Contracts;
using Context;
using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ProductReposatory : Reposatory<Product, int>, IProductReposatory
    {
        public ProductReposatory(ApplicationContext context) : base(context)
        {
        }
    }
}
