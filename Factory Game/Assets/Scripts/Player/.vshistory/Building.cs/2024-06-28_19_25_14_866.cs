using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _player;
    [SerializeField] private UIDocument buildUI;

    public Material blueprintMaterial;
    public GameObject[] gameObjects;
    
    public GameObject _camera;
    public GameObject holdPosition;

    private void OnEnable()
    {
        VisualElement currentRow = null;
        int buttonCount = 0;

        //Create a new Button for each objected assigned in the inspector
        foreach (var gameObject in gameObjects)
        {
            //New row every 3 Buttons
            if (buttonCount % 3 == 0)
            {
                currentRow = new VisualElement();
                currentRow.AddToClassList("row");
                buildUI.rootVisualElement.Q("ScrollMenu").Add(currentRow);
            }

            var button = new Button();
            button.text = gameObject.name;
            currentRow.Add(button);
            button.clicked += () => onButtonClicked(gameObject);
            buttonCount++;
        }
    }

    private void onButtonClicked(GameObject gameObject)
    {
        Debug.Log(gameObject.name);
        GameObject objectBlueprint = Instantiate(gameObject);
        objectBlueprint.name = gameObject.name + "Blueprint";
        objectBlueprint.transform.parent = holdPosition.transform;
    }
}
