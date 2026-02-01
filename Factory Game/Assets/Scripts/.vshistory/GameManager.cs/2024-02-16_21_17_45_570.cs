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
    public TMP_Text interactText;
    public float sens;

    private void Start()
    {
        
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;

        pauseMenu.SetActive(false);
        interactText.enabled = false;
        SensChange();
    }

    private void HandlePause()
    {
        interactText.enabled = false;
        pauseMenu.SetActive(true);
    }

    private void HandleResume()
    {
        interactText.enabled = true;
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
            SensChange();
        }
        else 
        {
            controllerText.text = @"Use Controller
Yes";
            usingController = true;
            SensChange();
        }
    }

    public void resetPos()
    {
        player.transform.position = Vector3.zero;
    }
}
