using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private GameObject winScreenUI;
    [SerializeField] private AudioClip winSound;

    private void LoadNextLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (currentSceneName == "Level2")
        {
            SoundManager.instance.PlaySound(winSound);
            winScreenUI.SetActive(true);
            Time.timeScale = 0f;


            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }
}
