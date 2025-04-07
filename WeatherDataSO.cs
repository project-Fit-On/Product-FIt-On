using UnityEngine;
using System;

[CreateAssetMenu(fileName = "WeatherData", menuName = "Weather/WeatherData")]
public class WeatherScriptableObject : ScriptableObject
{
    public static WeatherScriptableObject Instance;
    public Action<WeatherInfo> OnWeatherUpdated;
    public WeatherInfo currentInfo;

    public void Init()
    {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void UpdateData(WeatherInfo info)
    {
        currentInfo = info;
        OnWeatherUpdated?.Invoke(info);
    }
}
