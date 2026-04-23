using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject dialogueUI;

    private bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        controlsPanel.SetActive(false);

        dialogueUI.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(false);

        dialogueUI.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ShowControls()
    {
        pausePanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void HideControls() 
    { 
        controlsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
