using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeatherData : MonoBehaviour {
    private float timer;
    public float minutesBetweenUpdate = 1f; // default to 1 minute
    public WeatherInfo Info;
    public string API_key;
    private float latitude;
    private float longitude;
    private bool locationInitialized;

    [Header("UI Elements")]
    public Text currentWeatherText;

    [Header("Dependencies")]
    public GetLocation getLocation;
    public WeatherScriptableObject weatherSO;

    public void Begin() {
        latitude = getLocation.latitude;
        longitude = getLocation.longitude;
        locationInitialized = true;

        // Initialize the scriptable object singleton if not already
        weatherSO.Init();
    }

    void Update() {
        if (locationInitialized) {
            if (timer <= 0f) {
                StartCoroutine(GetWeatherInfo());
                timer = minutesBetweenUpdate * 60f;
            } else {
                timer -= Time.deltaTime;
            }
        }
    }

    private IEnumerator GetWeatherInfo() {
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={API_key}&units=metric";

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError) {
            Debug.LogError($"Weather API Error: {www.error}");
            yield break;
        }

try {
    Info = JsonUtility.FromJson<WeatherInfo>(www.downloadHandler.text);
    weatherSO.UpdateData(Info);

    // Optional: safety check for weather list
    if (Info.weather != null && Info.weather.Count > 0) {
        currentWeatherText.text = $"Current weather: {Info.weather[0].main}, {Info.weather[0].description}";
    } else {
        currentWeatherText.text = "Weather data unavailable.";
    }
} catch (Exception ex) {
    Debug.LogError($"Failed to parse weather data: {ex.Message}");
}

    }
}

