using UnityEngine.UIElements;
using UnityEngine;

public class UIReader : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    //Main Menu
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hudUI;
    [SerializeField] private GameObject buildUI;

    public VisualElement uiPause;
    public VisualElement uiHud;
    public VisualElement uiBuild;

    //Pause Menu
    #region PauseMenu

    public VisualElement pauseBackground;

    public VisualElement menu;
    public VisualElement optionsMenu;
    //Buttons
    //Menu
    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;
    //Options
    public Label sensLable;
    public SliderInt sensSlider;
    public Button resetPosButton;
    public Button backButton;

    #endregion PauseMenu

    //HUD
    //Action Prompt
    public Label actionPrompt;

    //Build Menu
    public VisualElement buildMenuBackground;

    private void Awake()
    {
        uiPause = pauseMenu.GetComponent<UIDocument>().rootVisualElement;
        uiHud = hudUI.GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        pauseBackground = uiPause.Q<VisualElement>("PauseBackground");

        //Pause Menu
        #region PauseMenu

        menu = uiPause.Q<VisualElement>("Menu");
        optionsMenu = uiPause.Q<VisualElement>("OptionsMenu");

        //Buttons
        //Menu
        resumeButton = uiPause.Q<Button>("ResumeButton");
        optionsButton = uiPause.Q<Button>("OptionsButton");
        mainMenuButton = uiPause.Q<Button>("MainMenuButton");
        //Options
        sensLable = uiPause.Q<Label>("SliderLabel");
        sensSlider = uiPause.Q<SliderInt>("SensSlider");
        resetPosButton = uiPause.Q<Button>("ResetPosButton");
        backButton = uiPause.Q<Button>("BackButton");

        #endregion PauseMenu

        //HUD
        //Action Prompt
        actionPrompt = uiHud.Q<Label>("ActionPrompt");

        //Build Menu
        buildMenuBackground = uiBuild.Q<VisualElement>("BuildMenuBackground");
    }
}
