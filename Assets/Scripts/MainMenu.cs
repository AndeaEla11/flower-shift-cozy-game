using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;

    public  void OnStartClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OrderScene");
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnVolumeChanged()
    {
        AudioListener.volume = volumeSlider.value;
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void ResetVolume()
    {
        volumeSlider.value = defaultVolume;
        OnVolumeChanged();
        VolumeApply();
    }
}
