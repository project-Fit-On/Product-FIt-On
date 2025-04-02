using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text recommendationText;
    public Transform imageContainer; // 🔸 New: Container to hold clothing item images
    public GameObject imagePrefab;   // 🔸 New: Prefab with an Image component
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

        // 🔸 Clear old images
        foreach (Transform child in imageContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in outfit)
        {
            recommendationText.text += $"{item.Name} ({item.Type}) - {item.Color}\n";

            // 🔸 Instantiate image prefab and assign sprite
            if (item.Sprite != null)
            {
                GameObject imageObj = Instantiate(imagePrefab, imageContainer);
                Image img = imageObj.GetComponent<Image>();
                img.sprite = item.Sprite;
            }
        }
    }
}
