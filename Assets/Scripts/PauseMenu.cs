using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
        
        Time.timeScale = 1.0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Restart()
    {
        PlayerHealth.Instance.currentHealth = 4;
        Stamina.Instance.ResetStamina();
        EconomyManager.Instance.ResetGold();
        if (GameObject.Find("Player") != null)
        {
            Destroy(GameObject.Find("Player"));
        }
       
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Scene1");
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;

    }
}
