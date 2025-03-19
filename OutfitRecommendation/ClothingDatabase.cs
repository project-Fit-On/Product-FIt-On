using System.Collections.Generic;
using UnityEngine;

public class ClothingDatabase : MonoBehaviour
{
    public List<ClothingItem> ClothingItems = new List<ClothingItem>();

    void Start()
    {
        LoadSampleData();
    }

    void LoadSampleData()
    {
        ClothingItems.Add(new ClothingItem("Blue T-Shirt", "Shirt", "Blue", "Summer", "Casual"));
        ClothingItems.Add(new ClothingItem("Black Hoodie", "Shirt", "Black", "Winter", "Casual"));
        ClothingItems.Add(new ClothingItem("Jeans", "Pants", "Blue", "All", "Casual"));
        ClothingItems.Add(new ClothingItem("Formal Shoes", "Shoes", "Black", "All", "Formal"));
        ClothingItems.Add(new ClothingItem("Sneakers", "Shoes", "White", "All", "Casual"));
    }
}
