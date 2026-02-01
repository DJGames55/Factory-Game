using UnityEngine.UIElements;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] private GameManager gameManager
    [SerializeField] private UIReader _UIReader;

    public VisualElement ui;

    private void Start()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        _UIReader
    }

    public void BuildMenuOpen()
    {
        Debug.Log("Build Menu Open");
    }
}
