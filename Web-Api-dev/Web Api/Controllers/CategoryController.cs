﻿using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryServices _icategoryServices;
        private readonly ISubcategoryServices _subcategoryServices;

        public CategoryController(IcategoryServices categoryServices,
            ISubcategoryServices subcategoryServices)
        {
            _icategoryServices = categoryServices;
            _subcategoryServices = subcategoryServices;
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> getAll()
        {
            
            var res = await _icategoryServices.GetAllCategory();
            return Ok(res);

        }

        [HttpGet]
        [Route("SubCategories")]
        public async Task<IActionResult> getSubCategory(int id)
        {
            var res = await _subcategoryServices.getSubCategoryByCatId(id);
            return Ok(res);

        }

        [HttpGet]
        [Route("AllSubCategories")]
        public async Task<IActionResult> getSubCategory()
        {
            var res = await _subcategoryServices.GetAllSubcategories();
            return Ok(res);
        }

    }
}
