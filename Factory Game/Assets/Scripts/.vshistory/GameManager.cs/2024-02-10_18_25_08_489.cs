using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player;

    public GameObject sensSlider;
    public TMP_Text sensText;
    public float sens;

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;

        pauseMenu.SetActive(false);
        sensText.text = $"Sensitivity: {sens}";
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
        sens = sensSlider.GetComponent<Slider>().value;
        sensText.text = $"Sensitivity: {sens}";
    }
}
