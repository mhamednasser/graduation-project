using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class PythonApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public PythonApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration["PythonApi:BaseUrl"]; // Read the base URL from configuration
    }

    public async Task<string> PredictNutritionalInfoAsync(IFormFile image)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(image.OpenReadStream()), "image", image.FileName);

        var response = await _httpClient.PostAsync($"{_baseUrl}/predict", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error from Python API: {error}");
        }

        return await response.Content.ReadAsStringAsync(); // Return raw JSON as string
    }
}
