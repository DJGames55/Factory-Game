using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _player;
    [SerializeField] private UIDocument buildUI;

    public Material blueprintMaterial;
    public GameObject[] gameObjects;
    
    public GameObject holdPosition;
    private GameObject placingObject;

    private void Start()
    {
        _input.PlaceObjectEvent += PlaceObject;
    }

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
        CreateBlueprintObject(gameObject);
        _gameManager.SetBuilding();
        placingObject = gameObject;
    }

    [HideInInspector] public GameObject objectBlueprint;
    private void CreateBlueprintObject(GameObject gameObject)
    {
        // Creates a copy of the object
        objectBlueprint = Instantiate(gameObject);
        objectBlueprint.name = gameObject.name + "Blueprint";
        // Sets Object as a Child of holdPosition
        objectBlueprint.transform.parent = holdPosition.transform;
        objectBlueprint.transform.SetPositionAndRotation(holdPosition.transform.position, holdPosition.transform.rotation);

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

    public void StopPlacingObject(GameObject placingObject)
    {
        Destroy(placingObject);
    }

    private void PlaceObject()
    {
        GameObject placedObject = Instantiate(placingObject);
        placedObject.name = placingObject.name + "Copy";
        StopPlacingObject(objectBlueprint);
        placedObject.GetComponent<ObjectType>().Destoyable = true;

        Vector3 angles = holdPosition.transform.eulerAngles;
        angles.x = 0f;
        placedObject.transform.SetPositionAndRotation(holdPosition.transform.position, Quaternion.Euler(angles));

    }
}
