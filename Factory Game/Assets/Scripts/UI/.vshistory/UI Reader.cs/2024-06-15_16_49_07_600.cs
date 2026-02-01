using UnityEngine.UIElements;
using UnityEngine;

public class UIReader : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    //Main Menu
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject hudUI;

    public VisualElement uiPause;
    public VisualElement uiHud;
    public VisualElement uiBuild;

    #region Menus
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
    #endregion

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

        //Menus
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
        sensSlider.RegisterValueChangedCallback(OnSensChanged);
        resetPosButton = uiPause.Q<Button>("ResetPosButton");
        backButton = uiPause.Q<Button>("BackButton");

        //HUD
        //Action Prompt
        actionPrompt = uiHud.Q<Label>("ActionPrompt");

        //Build Menu
        buildMenuBackground = uiBuild.Q<VisualElement>("buildMenuBackground");
    }

    private void OnSensChanged(ChangeEvent<int> evt)
    {
        gameManager.sens = evt.newValue;
        sensLable.text = "Sensitivity: " + evt.newValue;
    }
}
