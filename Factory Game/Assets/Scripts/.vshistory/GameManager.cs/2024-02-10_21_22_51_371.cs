using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject player;
    public bool usingController;
    public TMP_Text controllerText;

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
        if (usingController)
        {
            sens = sens * 10;
        }
    }
    public void useController()
    {
        if (usingController)
        {
            controllerText.text = @"Use Controller
No";
            usingController = false;
        }
        else 
        {
            controllerText.text = @"Use Controller
Yes";
            usingController = true; 
        }
    }

    public void resetPos()
    {
        player.transform.position = Vector3.zero;
    }
}
