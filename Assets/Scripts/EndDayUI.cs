using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EndDayUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameController gameController;

    public void ShowResults(int correct, int incorrect)
    {
        panel.SetActive(true);

        resultText.text = "Correct: " + correct + "\n" + "Incorrect: " + incorrect;
    }
    public void OnNewDayClicked()
    {
        GameState.ResetDay();
        panel.SetActive(false);

        if (gameController != null)
        {
            gameController.SpawnCustomer();
        }
    }
}
