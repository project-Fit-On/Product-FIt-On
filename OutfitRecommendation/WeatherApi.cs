using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeatherAPI : MonoBehaviour
{
    private string apiKey = "YOUR_API_KEY"; // Replace with a real weather API key
    private string city = "New York";
    private string url = "http://api.weatherapi.com/v1/current.json?key={0}&q={1}";

    public delegate void WeatherUpdate(string season);
    public static event WeatherUpdate OnWeatherFetched;

    void Start()
    {
        StartCoroutine(GetWeatherData());
    }

    IEnumerator GetWeatherData()
    {
        string finalUrl = string.Format(url, apiKey, city);
        UnityWebRequest request = UnityWebRequest.Get(finalUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResult = request.downloadHandler.text;
            string season = ParseWeatherToSeason(jsonResult);
            OnWeatherFetched?.Invoke(season);
        }
        else
        {
            Debug.LogError("Failed to get weather data.");
        }
    }

    string ParseWeatherToSeason(string json)
    {
        // Simplified example: Extract temp and determine season
        float temperature = 25f; // Assume extracted from JSON
        return temperature > 20 ? "Summer" : "Winter";
    }
}
