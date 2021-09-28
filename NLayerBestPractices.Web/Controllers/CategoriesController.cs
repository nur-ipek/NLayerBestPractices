using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerBestPractices.Core.Models;
using NLayerBestPractices.Core.Services;
using NLayerBestPractices.Core.UnitOfWorks;
using NLayerBestPractices.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        //Burada Auoto Mapper'ın dependency injection kütüphanesini indirdiğimiz için IMapper class'ını ctor içerisinde verebiliyoruz !!
        //AS.NET Core Mimarisi DI üzerine kurulu olduğu için kütüphane yüklerken DI olanı yüklemeliyiz.
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _categoryService.GetAllAsync();

            return View(_mapper.Map<IEnumerable<CategoryDto>>(list));
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));

            return RedirectToAction("Index");
        }

        //Güncellenecek olan alanı dolduralım..
        public async Task<IActionResult> Update(int id)
        {
            var updateCategory = await _categoryService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(updateCategory));
        }

        [HttpPost]
        public IActionResult Update(CategoryDto categoryDto)
        {
            _categoryService.Update(_mapper.Map<Category>(categoryDto));
            return RedirectToAction("Index");
        }

        
        public IActionResult Delete(int id)
        {
            var deleteCategory =  _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(deleteCategory);
            return RedirectToAction("Index");
        }

       
    }
}
