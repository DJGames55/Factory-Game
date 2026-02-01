using UnityEngine.UIElements;
using UnityEngine;

public class UIReader : MonoBehaviour
{
    //Main Menu
    public VisualElement ui;
    //Menus
    public VisualElement menu;
    public VisualElement optionsMenu;
    //Buttons
    //Menu
    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;
    //Options
    public Button resetPosButton;
    public Button backButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        //Menus
        menu = ui.Q<VisualElement>("Menu");
        optionsMenu = ui.Q<VisualElement>("OptionsMenu");

        //Buttons
        //Menu
        resumeButton = ui.Q<Button>("ResumeButton");
        optionsButton = ui.Q<Button>("OptionsButton");
        mainMenuButton = ui.Q<Button>("MainMenuButton");
        //Options

    }

}
