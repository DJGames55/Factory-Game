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

        resumeButton = root.Q<Button>("resumeButton");
        optionsButton = root.Q<Button>("optionsButton");
        exitButton = root.Q<Button>("exitButton");
    }
}
