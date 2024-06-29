using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("GameOver")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Point")]
    public Text pointText;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(pauseScreen.activeInHierarchy) 
                PauseGame(false);
            else
                PauseGame(true);
        }
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    public void Resume()
    {
        PauseGame(false);
    }
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1; 
        //Time.timeScale = System.Convert.ToInt32(!status);
    }
    public void SoundVolume()
    {
        SoundManager.instance.changeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.changeMusicVolume(0.2f);
    }

    public void ShowPoint(string value)
    {
        pointText.text = value;
    }
}
