using UnityEngine.UIElements;
using UnityEngine;

public class UIReader : MonoBehaviour
{
    //Main Menu
    public VisualElement menu;
    //Buttons
    public Button resumeButton;
    public Button optionsButton;
    public Button mainMenuButton;

    private void Awake()
    {
        menu = GetComponent<UIDocument>().rootVisualElement;
    }


}
