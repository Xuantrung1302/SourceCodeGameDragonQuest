using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrow;
    [SerializeField] private RectTransform[] buttons;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    private int currentPosition;

    private void Awake()
    {
        ChangePosition(0);
        Time.timeScale = 1; // Ensure time scale is reset to 1 when the game starts
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit"))
            Interact();
    }

    public void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(changeSound);
        }

        if (currentPosition < 0)
            currentPosition = buttons.Length - 1;
        else if (currentPosition > buttons.Length - 1)
            currentPosition = 0;

        AssignPosition();
    }

    private void AssignPosition()
    {
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        if (currentPosition == 0)
        {
            Play();
        }
        else if (currentPosition == 1)
        {
            Quit();
        }
    }

    public void Play()
    {
        Time.timeScale = 1; // Ensure time scale is reset to 1 when starting the game
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void MainMenu()
    {
        Time.timeScale = 1; // Reset time scale to 1 when returning to the main menu
        SceneManager.LoadScene(0);
    }
}
