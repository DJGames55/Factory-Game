using UnityEngine;
using UnityEngine.UIElements;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager
    [SerializeField] private UIReader _UIReader;

    public VisualElement ui;

    private void Start()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        _UIReader.squareButton.clicked += squareButtonPressed;
    }

    public void BuildMenuOpen()
    {
        Debug.Log("Build Menu Open");
    }

    private void squareButtonPressed()
    {

    }
}
