using UnityEngine.UIElements;
using UnityEngine;

public class UIReader : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    //Main Menu
    public VisualElement ui;

    public VisualElement uiElement;
    //Menus
    public VisualElement menu;
    public VisualElement optionsMenu;
    //Buttons
    //Menu
    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;
    //Options
    private Label sensLable;
    public Slider sensSlider;
    public Button resetPosButton;
    public Button backButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        uiElement = ui.Q<VisualElement>("Background");

        //Menus
        menu = ui.Q<VisualElement>("Menu");
        optionsMenu = ui.Q<VisualElement>("OptionsMenu");

        //Buttons
        //Menu
        resumeButton = ui.Q<Button>("ResumeButton");
        optionsButton = ui.Q<Button>("OptionsButton");
        mainMenuButton = ui.Q<Button>("MainMenuButton");
        //Options
        sensLable = ui.Q<Label>("SliderLable");
        sensSlider = ui.Q<Slider>("SensSlider");
        sensSlider.RegisterValueChangedCallback(OnSensChanged);
        resetPosButton = ui.Q<Button>("ResetPosButton");
        backButton = ui.Q<Button>("BackButton");

    }

    private void OnSensChanged(ChangeEvent<int> evt)
    {
        gameManager.sens = evt.newValue;
        sensLable.text = ("Sensitivity" + evt.newValue);
    }
}
