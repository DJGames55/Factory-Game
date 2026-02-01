using UnityEngine.UIElements;
using UnityEngine;
using TMPro.EditorUtilities;

public class UIReader : MonoBehaviour
{
    //Main Menu
    public VisualElement ui;
    //Buttons
    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        resumeButton = ui.Q<Button>("ResumeButton");
        optionsButton = ui.Q<Button>("OptionsButton");
        mainMenuButton = ui.Q<Button>("MainMenuButton");
    }

}
