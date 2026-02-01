using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _player;
    [SerializeField] private UIDocument buildUI;

    public GameObject[] gameObjects;
    
    public GameObject _camera;

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
        GameObject newGameObject = Instantiate(gameObject);
        newGameObject.name = gameObject.name + "Clone";
        newGameObject.transform.position = Vector3.zero;
    }

    private void Start()
    {
        _input.OpenBuildEvent += HandleOpenBuild;
    }

    private void HandleOpenBuild()
    {

    }
}
