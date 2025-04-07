using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using SimpleJSON; // if you're using SimpleJSON

public class WeatherManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown districtDropdown;  // The dropdown for districts
    public TMP_Text weatherText;           // Display weather info here

    // Replace with your actual OpenWeatherMap (or another service) API key
    private string apiKey = "af3256594a289252394b0ee57a53e693";

    // We'll keep the weather result in a string
    private string currentWeatherString = "";

    // Called when the object is enabled (or the scene is loaded)
    private void Start()
    {
        // Optional: set a default text
        weatherText.text = "Select a district, then click Get Weather";
    }

    // Called when the user clicks the "Get Weather" button (assign this in the Inspector)
    public void OnGetWeatherButtonClicked()
    {
        // Find out which district was selected in the dropdown
        string selectedDistrict = districtDropdown.options[districtDropdown.value].text;

        // Kick off the weather fetch
        StartCoroutine(FetchWeather(selectedDistrict));
    }

    private IEnumerator FetchWeather(string district)
    {
        // Construct the URL for current weather from OpenWeatherMap
        // "LK" as the country code for Sri Lanka
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={district},LK&units=metric&appid={apiKey}";

        // Send the GET request
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error fetching weather: " + request.error);
                currentWeatherString = "Weather Error!";
            }
            else
            {
                // Parse the JSON
                string jsonResult = request.downloadHandler.text;
                Debug.Log("Weather JSON: " + jsonResult);

                // If using SimpleJSON
                var data = JSON.Parse(jsonResult);

                // Example: main weather condition is data["weather"][0]["main"] ("Clear", "Clouds", "Rain", etc.)
                string weatherMain = data["weather"][0]["main"];
                // A more descriptive text might be data["weather"][0]["description"]

                // Simple interpretation of a few possible values
                switch (weatherMain)
                {
                    case "Clear":
                        currentWeatherString = "It's sunny!";
                        break;
                    case "Clouds":
                        currentWeatherString = "It's cloudy!";
                        break;
                    case "Rain":
                        currentWeatherString = "It's raining!";
                        break;
                    default:
                        currentWeatherString = $"Weather: {weatherMain}";
                        break;
                }
            }
        }

        // Update your UI text with the result
        weatherText.text = currentWeatherString;
    }
}
