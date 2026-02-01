using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _player;
    [SerializeField] private Interact _interact;
    [SerializeField] private UIDocument buildUI;

    public Material blueprintMaterial;
    public Material clearRedMaterial;
    public GameObject[] gameObjects;
    
    public GameObject holdPosition;
    public int holdLayer;
    private GameObject placingObject;
    private bool isBuilding;

    private Collider blueprintCollider;
    private bool canPlace;

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
            if (buttonCount % 4 == 0)
            {
                currentRow = new VisualElement();
                currentRow.AddToClassList("row");
                buildUI.rootVisualElement.Q("BuildScrollMenu").Add(currentRow);
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

        // Disable Blueprint Object Gravity
        Rigidbody oBRb = objectBlueprint.GetComponent<Rigidbody>();
        if (oBRb != null) { oBRb.useGravity = false; }

        // Disable Blueprint Object Collider
        blueprintCollider = objectBlueprint.GetComponent<Collider>();
        if (blueprintCollider != null) { blueprintCollider.enabled = false; }

        SetBluePrint();
        isBuilding = true;
    }

    private void SetBluePrint()
    {
        // Change Material
        Renderer renderer = objectBlueprint.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = blueprintMaterial;
        }

        // Change Children Materal
        if (objectBlueprint.transform.childCount != 0)
        {
            foreach (Transform child in objectBlueprint.transform)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.material = blueprintMaterial;
                }
            }
        }

        // Changes the Objects View Layer
        objectBlueprint.layer = holdLayer;
        // Changes the Layer of the children
        foreach (Transform child in objectBlueprint.transform)
        {
            child.gameObject.layer = holdLayer;
        }
        _interact.HoldingCameraSet();
    }

    public void StopPlacingObject()
    {
        Destroy(objectBlueprint);
        _interact.HoldingCameraStop();
    }

    private void PlaceObject()
    {
        if (isBuilding && canPlace)
        {
            // Creates Object
            GameObject placedObject = Instantiate(placingObject);
            placedObject.name = placingObject.name + "Copy";
            StopPlacingObject();
            placedObject.GetComponent<ObjectType>().Destoyable = true;

            // Sets Placed Objects Location
            Vector3 angles = holdPosition.transform.eulerAngles;
            angles.x = 0f;
            placedObject.transform.SetPositionAndRotation(holdPosition.transform.position, Quaternion.Euler(angles));

            _gameManager.Resume();
            isBuilding = false;
            _interact.isBuilding = false;
            _interact.HoldingCameraStop();
        }
        else if (!canPlace)
        {
            UnableToPlace();
        }
    }

    private void Update()
    {
        if (isBuilding && objectBlueprint != null)
        {
            objectBlueprint.transform.SetPositionAndRotation(holdPosition.transform.position, holdPosition.transform.rotation);
        }
    }

    // Check Can Place
    #region Check Can Place
    private void OnTriggerEnter(Collider other)
    {
        if (isBuilding && other != blueprintCollider)
        {
            canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isBuilding && other != blueprintCollider)
        {
            canPlace = true;
            AbleToPlace();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isBuilding && other != blueprintCollider)
        {
            canPlace = false;
        }
    }
    #endregion Check Can Place

    private void UnableToPlace()
    {
        _interact.HoldingCameraStop();

        // Change Material
        Renderer renderer = objectBlueprint.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = clearRedMaterial;
        }

        // Change Children Materal
        if (objectBlueprint.transform.childCount != 0)
        {
            foreach (Transform child in objectBlueprint.transform)
            {
                Renderer childRenderer = child.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    childRenderer.material = clearRedMaterial;
                }
            }
        }
    }

    private void AbleToPlace()
    {
        _interact.HoldingCameraSet();

        // Change Material
        Renderer renderer = objectBlueprint.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = blueprintMaterial;
        }

        // Change Children Materal
        if (objectBlueprint.transform.childCount != 0)
        {
            foreach (Transform child in objectBlueprint.transform)
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
