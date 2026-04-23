using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class OrderUI : MonoBehaviour
{
    [SerializeField] private GameObject orderPanel;
    [SerializeField] private TMP_Text orderText;

    private void Awake()
    {
        orderPanel.SetActive(false);
    }

    public void ShowOrder(OrderData order)
    {
        string message = "I'd like " + order.requiredTotalFlowers + " flowers, please!";
        
        if (!string.IsNullOrEmpty(order.requiredFlowerType))
        {
            message += "\nInclude " + order.requiredFlowerTypeCount + " " + order.requiredFlowerType + " flowers.";
        }

        if (!string.IsNullOrEmpty(order.requiredFlowerColour))
        {
            message += "\nInclude " + order.requiredFlowerColourCount + " " + order.requiredFlowerColour + " flowers.";
        }

        orderText.text = message;
        orderPanel.SetActive(true);
    }

    public void HideOrder()
    {
        orderPanel.SetActive(false);
    }

    public void StartBuilding()
    {
        HideOrder();
        SceneManager.LoadScene("BouquetArrangementScene");
    }
}
