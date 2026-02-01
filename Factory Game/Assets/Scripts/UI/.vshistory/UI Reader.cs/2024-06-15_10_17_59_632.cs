using UnityEngine.UIElements;
using UnityEngine;

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


}
