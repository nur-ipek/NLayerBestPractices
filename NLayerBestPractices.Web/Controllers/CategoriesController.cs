using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerBestPractices.Web.ApiService;
using NLayerBestPractices.Web.DTOs;
using NLayerBestPractices.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerBestPractices.Web.Controllers
{
    public class CategoriesController : Controller
    {
        
        private readonly IMapper _mapper;
        private readonly CategoryApiService _categoryApiService;

        //Burada Auoto Mapper'ın dependency injection kütüphanesini indirdiğimiz için IMapper class'ını ctor içerisinde verebiliyoruz !!
        //AS.NET Core Mimarisi DI üzerine kurulu olduğu için kütüphane yüklerken DI olanı yüklemeliyiz.
        public CategoriesController(IMapper mapper, CategoryApiService categoryApiService)
        {
            _mapper = mapper;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        
        {
            var list = await _categoryApiService.GetAllAsync();

            return View(_mapper.Map<IEnumerable<CategoryDto>>(list));
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var category = await _categoryApiService.AddAsync(categoryDto);

            return RedirectToAction("Index");
        }

        //Güncellenecek olan alanı dolduralım..
        public async Task<IActionResult> Update(int id)
        {
            var updateCategory = await _categoryApiService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(updateCategory));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
           await  _categoryApiService.Update(categoryDto);
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryApiService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
