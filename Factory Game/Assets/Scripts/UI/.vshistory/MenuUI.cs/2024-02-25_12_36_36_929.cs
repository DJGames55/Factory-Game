using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : MonoBehaviour
{
    public VisualElement menu;
    public VisualElement options;

    public Button resumeButton;
    public Button optionsButton;
    public Button exitButton;


    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        menu = root.Q<VisualElement>("Menu");
        options = root.Q<VisualElement>("OptionsMenu");

        resumeButton = root.Q<Button>("resumeButton");
        resumeButton.clicked += resumeButtonPressed;

        optionsButton = root.Q<Button>("optionsButton");
        optionsButton.clicked += optionsButtonPressed;

        exitButton = root.Q<Button>("exitButton");
        exitButton.clicked += exitButtonPressed;
    }

    void resumeButtonPressed()
    {
        menu.
    }

    void optionsButtonPressed()
    {

    }

    void exitButtonPressed()
    {

    }
}
