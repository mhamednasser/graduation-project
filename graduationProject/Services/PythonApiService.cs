using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

public class PythonApiService
{
    private readonly HttpClient _httpClient;

    public PythonApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // This method calls the Python API and returns the raw JSON response as a string
    public async Task<string> PredictNutritionalInfoAsync(IFormFile image)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StreamContent(image.OpenReadStream()), "image", image.FileName);  

        var response = await _httpClient.PostAsync("http://127.0.0.1:8000/predict", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error from Python API: {error}");
        }

        return await response.Content.ReadAsStringAsync(); // Return raw JSON as string  erg3 tany leh !!!
    }

}
