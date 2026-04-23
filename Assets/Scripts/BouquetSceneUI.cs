using UnityEngine;
using UnityEngine.SceneManagement; 

public class BouquetSceneUI : MonoBehaviour
{

    public void DeliverBouquet()
    {
        BouquetBuilder builder = FindFirstObjectByType<BouquetBuilder>();
        if (builder == null || builder.placedFlowers.Count == 0)
        {
            return;
        }

        if (OrderManager.Instance == null || OrderManager.Instance.CurrentOrder == null)
        {
            return;
        }

        OrderValidator.ValidateOrder(OrderManager.Instance.CurrentOrder, builder);

        GameState.bouquetDelivered = true;
        SceneManager.LoadScene("OrderScene");
    }
}
