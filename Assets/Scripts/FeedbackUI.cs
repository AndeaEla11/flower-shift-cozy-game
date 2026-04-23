using UnityEngine;
using TMPro;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TMP_Text resultsText;
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        resultsPanel.SetActive(false);
    }

    public void ShowResults(int required, int placed)
    {
        resultsText.text = GameState.feedbackMessage;
        scoreText.text = "Correct: " + GameState.correctCount + "\nIncorrect: " + GameState.incorrectCount;
        resultsPanel.SetActive(true);
    }

    public void Hide()
    {
        resultsPanel.SetActive(false);
    }

    public void Goodbye()
    {
        CustomerFlow flow = FindFirstObjectByType<CustomerFlow>();
        if (flow != null)
        { 
            flow.OnGoodbyeClicked(); 
        }

        Hide();
    }
}
