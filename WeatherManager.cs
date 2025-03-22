using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class WeatherManager : MonoBehaviour
{
    private string apiKey = "515dd7c6cc066a1f8863efc613afd5b0"; // Replace with your OpenWeatherMap API Key
    private string city = "Colombo";  // Change to user's location
    private string apiUrl = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric";

    public Text cityText;
    public Text temperatureText;
    public Text weatherConditionText;
    public Image weatherIcon;

    private void Start()
    {
        StartCoroutine(GetWeatherData());
    }

    IEnumerator GetWeatherData()
    {
        string url = string.Format(apiUrl, city, apiKey);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                JObject json = JObject.Parse(request.downloadHandler.text);

                string weatherCondition = json["weather"][0]["main"].ToString();
                float temperature = float.Parse(json["main"]["temp"].ToString());

                cityText.text = "City: " + city;
                temperatureText.text = "Temperature: " + temperature + "°C";
                weatherConditionText.text = "Condition: " + weatherCondition;

                // Change icon based on condition
                string icon = json["weather"][0]["icon"].ToString();
                StartCoroutine(LoadWeatherIcon(icon));
            }
            else
            {
                Debug.LogError("Failed to fetch weather data: " + request.error);
            }
        }
    }

    IEnumerator LoadWeatherIcon(string iconCode)
    {
        string iconUrl = $"https://openweathermap.org/img/wn/{iconCode}@2x.png";
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(iconUrl))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                weatherIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}

