using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    [SerializeField] private List<OrderData> orders = new List<OrderData>();

    public OrderData CurrentOrder { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (orders.Count == 0)
        {
            orders.Add(new OrderData{requiredTotalFlowers = 2});

            orders.Add(new OrderData{requiredTotalFlowers = 3, requiredFlowerType = "Tulip", requiredFlowerTypeCount = 1});

            orders.Add(new OrderData{requiredTotalFlowers = 3, requiredFlowerColour = "Yellow", requiredFlowerColourCount = 2});

            orders.Add(new OrderData{requiredTotalFlowers = 4, requiredFlowerColour = "Pink", requiredFlowerColourCount = 2});
        }
    }

    public void GenerateNewOrder()
    {
        int randomIndex = Random.Range(0, orders.Count);
        CurrentOrder = orders[randomIndex];
    }
}
