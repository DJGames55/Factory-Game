using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIReader _UIReader;
    [SerializeField] private UIDocument _UIDoc;
    [SerializeField] private GameObject player;
    
    public float sens;

    private void Start()
    {
        HandleResume();

        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
        //Buttons
        //Menu
        _UIReader.resumeButton.clicked += HandleResume;
        _UIReader.optionsButton.clicked += Options;
        _UIReader.mainMenuButton.clicked += HandleMainMenu;
        //Options
        _UIReader.resetPosButton.clicked += resetPos;
        _UIReader.backButton.clicked += HandlePause;
    }

    public void resetPos()
    {
        player.transform.position = Vector3.zero;
    }

    //UI
    private void HandlePause()
    {
        _UIReader.menu.style.display = DisplayStyle.Flex;
        _UIReader.optionsMenu.style.display = DisplayStyle.None;
        _input.SetUI();
        _UIReader.uiElement.style.display = DisplayStyle.None;
    }

    private void HandleResume()
    {
        _UIReader.menu.style.display = DisplayStyle.None;
        _UIReader.optionsMenu.style.display = DisplayStyle.None;
        _input.SetGameplay();
        _UIReader.uiElement.style.display = DisplayStyle.None;
    }

    private void Options()
    {
        _UIReader.optionsMenu.style.display = DisplayStyle.Flex;
    }

    private void HandleMainMenu()
    {
        Debug.Log("Main Menu");
    }
}
