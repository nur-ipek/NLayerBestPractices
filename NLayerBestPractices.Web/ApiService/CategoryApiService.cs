using Newtonsoft.Json;
using NLayerBestPractices.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLayerBestPractices.Web.ApiService
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> categoryDtos;
            var response = await _httpClient.GetAsync("Category");  //---> Gizli Base Url var..

             if(response.IsSuccessStatusCode)
            {
                categoryDtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
            }
             else
            {
                categoryDtos = null;
            }

            //seralize --> nesneyi json'a dönüştürme işlemi
            //deserialize --> json'dan class'a dönüştürme işlemi

            return categoryDtos;
        }

        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("Category", stringContent);

            if(response.IsSuccessStatusCode)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
                return categoryDto;

            }
            else
            {
                //loglama!!
                return null;
            }
            
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            CategoryDto categoryDto;
            var response = await _httpClient.GetAsync($"Category/{id}");

            if (response.IsSuccessStatusCode)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
                return categoryDto;
            }
            else
            {
                categoryDto = null;
                return null;
            }
        }

        public async Task<bool> Update(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("Category", stringContent);

            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }


           
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"Category/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
