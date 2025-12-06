using System.Net.Http.Json;
using WebApp.Dtos;

namespace WebApp.Clients
{
	public class EmployeeClient
	{
		private readonly HttpClient _httpClient;

		public EmployeeClient(IHttpClientFactory httpClient)
		{
			_httpClient = httpClient.CreateClient("ApiClient");
		}

		public async Task<List<EmployeeDto>?> GetAllAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<EmployeeDto>>("/api/employees");
		}

		public async Task<EmployeeDto?> GetByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<EmployeeDto>($"api/employees/{id}");
		}

		public async Task<bool> CreateAsync(EmployeeDto dto)
		{
			var response = await _httpClient.PostAsJsonAsync("api/employees", dto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> UpdateAsync(int id, EmployeeDto dto)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/employees/{id}", dto);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/employees/{id}");
			return response.IsSuccessStatusCode;
		}

	}
}
