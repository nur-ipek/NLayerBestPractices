﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerBestPractices.API.DTOs;
using NLayerBestPractices.Core.Models;
using NLayerBestPractices.Core.Services;
using NLayerBestPractices.Service.Services;

namespace NLayerBestPractices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper; //_private 
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories)); //200 durum kodu -->Ok

            //Entity'lerimizi direk olarak dönmemeliyiz. DTO nesneleri bunun için vardır ve dönüşümler içinse AutoMapper kullanmalıyız.
            //Entity'lerimizin içinde client'lara göstermememiz gereken property'ler olabilir.
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            return Ok(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto) // Entity almıyorum ve Client'a entity vermiyorumm.
        {
            var newCategory = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

            return Created(string.Empty, _mapper.Map<CategoryDto>(newCategory));
        }


    }
}
