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
	public class SubCategoryReposatory : Reposatory<Category, int>, ISubCategoryReposatory
	{
		private readonly ApplicationContext _context;
		public SubCategoryReposatory(ApplicationContext context) : base(context)
		{
			_context = context;
		}
	}
}
