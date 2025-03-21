using UnityEngine;
using System.Collections.Generic;

public class ClothManager : MonoBehaviour
{
    private Dictionary<string, string> clothMap = new Dictionary<string, string>
    {
        { "8822443311", "ClothModels/8822443311/8822443311" },
        {"886754321","ClothModels/886754321/886754321"}
        // etc.
    };

    public void SpawnCloth(string serial)
    {
        if (clothMap.ContainsKey(serial))
        {
            string path = clothMap[serial];
            GameObject clothPrefab = Resources.Load<GameObject>(path);
            if (clothPrefab != null)
            {
                Camera mainCam = Camera.main;
                if (mainCam != null)
                {
                    Vector3 pos = mainCam.transform.position + mainCam.transform.forward * 2f;
                    var obj = Instantiate(clothPrefab, pos, Quaternion.identity);
                    obj.transform.LookAt(mainCam.transform);
                }
            }
        }
    }
}
