using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text recommendationText;
    public ClothingRecommender recommender;

    void Start()
    {
        WeatherAPI.OnWeatherFetched += UpdateSeason;
    }

    void UpdateSeason(string season)
    {
        recommender.currentSeason = season;
        List<ClothingItem> recommendedOutfit = recommender.GetRecommendedOutfit();
        ShowOutfit(recommendedOutfit);
    }

    void ShowOutfit(List<ClothingItem> outfit)
    {
        recommendationText.text = "Recommended Outfit:\n";
        foreach (var item in outfit)
        {
            // 🔸 Modified this line to include color info
            recommendationText.text += $"{item.Name} ({item.Type}) - {item.Color}\n";
        }
    }
}
