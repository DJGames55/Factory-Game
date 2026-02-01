using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player;

    public GameObject sensSlider;

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

    public void SensChange()
    {
        player.GetComponent<PlayerControls>().sensitivity = sensSlider.GetComponent<Slider>().value;
    }
}
