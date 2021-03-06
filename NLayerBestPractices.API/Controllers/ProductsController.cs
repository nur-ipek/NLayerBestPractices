using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerBestPractices.API.DTOs;
using NLayerBestPractices.API.Filters;
using NLayerBestPractices.Core.Models;
using NLayerBestPractices.Core.Services;

namespace NLayerBestPractices.API.Controllers 
{

    //Controller seviyesinde de ValidationFilter yazabiliriliz. 


    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allProducts = await _service.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(allProducts));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategoryByIdAsync(int id)
        {
            var category = await _service.GetWithCategoryByIdAsync(id);

            return Ok(_mapper.Map<ProductWithCategoryDto>(category));
        }

        //Yazdığım filter'ı nerede kullanmak istiyorsam o controller üzerine eklemem gerekli !!!
        //[ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var newProduct = await _service.AddAsync(_mapper.Map<Product>(productDto));

            return Created(string.Empty, _mapper.Map<ProductDto>(newProduct));
            //Entity döndürmüyoruz.
        }

        [ServiceFilter(typeof(NotFoundFilter))]  
        [HttpPut]
        public IActionResult Update(ProductDto productDto)
        {
            _service.Update(_mapper.Map<Product>(productDto));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var product= _service.GetByIdAsync(id).Result;
            _service.Remove(product);

            return NoContent();
        }

    }
}
