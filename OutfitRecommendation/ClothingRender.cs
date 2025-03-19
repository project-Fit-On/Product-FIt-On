using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothingRecommender : MonoBehaviour
{
    public ClothingDatabase clothingDatabase;
    public string preferredStyle = "Casual"; // Can be set dynamically
    public string currentSeason = "Summer"; // Can be set dynamically

    void Start()
    {
        List<ClothingItem> recommendedOutfit = GetRecommendedOutfit();
        DisplayRecommendation(recommendedOutfit);
    }

    List<ClothingItem> GetRecommendedOutfit()
    {
        return clothingDatabase.ClothingItems
            .Where(item => (item.Season == currentSeason || item.Season == "All") && item.Style == preferredStyle)
            .ToList();
    }

    void DisplayRecommendation(List<ClothingItem> outfit)
    {
        foreach (var item in outfit)
        {
            Debug.Log($"Recommended: {item.Name} ({item.Type}) - {item.Color}");
        }
    }
}
