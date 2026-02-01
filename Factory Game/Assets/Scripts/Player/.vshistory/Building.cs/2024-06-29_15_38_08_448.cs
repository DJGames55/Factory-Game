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

        // Create a new Button for each objected assigned in the inspector
        foreach (var gameObject in gameObjects)
        {
            // New row every 3 Buttons
            if (buttonCount % 3 == 0)
            {
                currentRow = new VisualElement();
                currentRow.AddToClassList("row");
                buildUI.rootVisualElement.Q("ScrollMenu").Add(currentRow);
            }

            // Creates the Button
            var button = new Button() { text = gameObject.name };
            currentRow.Add(button);
            // Gives it an "on click" function
            button.clicked += () => OnButtonClicked(gameObject);
            buttonCount++;
        }
    }

    // Button Clicked Function
    private void OnButtonClicked(GameObject gameObject)
    {
        // Creates a copy of the object
        GameObject objectBlueprint = Instantiate(gameObject);
        objectBlueprint.name = gameObject.name + "Blueprint";
        objectBlueprint.transform.parent = holdPosition.transform;
        objectBlueprint.transform.position = Vector3.zero
        
        // Disable Blueprint Object Gravity
        Rigidbody oBRb = objectBlueprint.GetComponent<Rigidbody>();
        if (oBRb != null) { oBRb.useGravity = false; }

        // Disable Blueprint Object Collider
        Collider oBC = objectBlueprint.GetComponent<Collider>();
        if (oBC != null) { oBC.enabled = false; }

        SetBluePrint(objectBlueprint);
    }

    private void SetBluePrint(GameObject blueprintObject)
    {
        // Change Material
        Renderer renderer = blueprintObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = blueprintMaterial;
        }

        // Change Children Materal
        if (blueprintObject.transform.childCount != 0)
        {
            foreach (Transform child in blueprintObject.transform)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.material = blueprintMaterial;
                }
            }
        } 
    }
}
