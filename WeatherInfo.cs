using System;
using System.Collections.Generic;

[Serializable]
public class WeatherDetails {
    public string main;
    public string description;
    public string icon;
}

[Serializable]
public class MainInfo {
    public float temp;
}

[Serializable]
public class WeatherInfo {
    public List<WeatherDetails> weather;
    public MainInfo main;
    public string name;

    public string GetIcon() {
        return (weather != null && weather.Count > 0) ? weather[0].icon : "unknown";
    }
}

