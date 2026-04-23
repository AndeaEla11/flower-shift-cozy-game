using System.Collections.Generic;
using UnityEngine;

public class BouquetBuilder : MonoBehaviour
{
    public List<GameObject> placedFlowers = new List<GameObject>();
     
    public void RegisterFlower(GameObject flower)
    {
        placedFlowers.Add(flower);
        flower.transform.SetParent(transform);
    }

    public int GetCount()
    {
        return placedFlowers.Count;
    }
}
