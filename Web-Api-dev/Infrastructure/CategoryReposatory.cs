using Application.Contracts;
using Context;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public class CategoryReposatory : Reposatory<Category, int>, ICategoryReposatory
	{
        private readonly ApplicationContext _Context;

        public CategoryReposatory(ApplicationContext context) : base(context)
		{
            _Context = context;

        }
       
    }
}
