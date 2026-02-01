using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;

        pauseMenu.SetActive(false);
    }

    private void HandlePause()
    {
        pauseMenu.SetActive(true);
    }

    private void HandleResume()
    {
        pauseMenu.SetActive(false);
    }
}
