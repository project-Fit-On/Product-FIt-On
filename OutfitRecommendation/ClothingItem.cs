using System;

[Serializable] // Allows this class to be serialized (e.g., saved to a file or sent over a network)
public class ClothingItem
{
    public string Name;
    public string Type; // e.g., "Shirt", "Pants", "Shoes"
    public string Color;
    public string Season; // e.g., "Summer", "Winter", "All"
    public string Style; // e.g., "Casual", "Formal", "Sporty"

//Constructor to initialise all properties when creating a new item
    public ClothingItem(string name, string type, string color, string season, string style)
    {
        Name = name;
        Type = type;
        Color = color;
        Season = season;
        Style = style;
    }
}
