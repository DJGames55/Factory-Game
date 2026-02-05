using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

public class Building : MonoBehaviour
{
    [Header("Blueprint")]
    public GameObject[] gameObjects;
    public Material blueprintMaterial, clearRedMaterial;

    [Header("Other")]
    [SerializeField] private InputReader _input;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _player;
    [SerializeField] private Interact _interact;
    [SerializeField] private UIDocument buildUI;

    [Header("Placement")]
    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private Camera _camera;
    private Vector3 lastPosition;
    [SerializeField] private GameObject mouseIndicator, gridPointer;
    [SerializeField] private Grid grid;
    
    public GameObject holdPosition;
    public int holdLayer;
    private GameObject placingObject;
    [HideInInspector] public bool isBuilding;

    private Collider blueprintCollider;
    private bool canPlace;
    public Collider lookCollider;

    private void Update()
    {
        Vector3 mousePosition = GetSelectedPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        gridPointer.transform.position = gridPosition; 

        if (isBuilding && objectBlueprint != null)
        {
            float blueprintHeight = objectBlueprint.gameObject.GetComponent<ObjectType>().objectHeight;
            objectBlueprint.transform.position = new Vector3((float)(gridPosition.x + 0.5), (float)(gridPosition.y + blueprintHeight/2), (float)(gridPosition.z + blueprintHeight/2));
        }
    }

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

        // Disable Blueprint Object Gravity
        Rigidbody oBRb = objectBlueprint.GetComponent<Rigidbody>();
        if (oBRb != null) { oBRb.useGravity = false; }

        // Disable Blueprint Object Collider
        blueprintCollider = objectBlueprint.GetComponent<Collider>();
        if (blueprintCollider != null) { blueprintCollider.isTrigger = true; }

        if (objectBlueprint.transform.childCount != 0)
        {
            foreach (Transform child in objectBlueprint.transform)
            {
                if (child.GetComponent<Collider>() != null)
                {
                    child.GetComponent<Collider>().isTrigger = true;
                }
            }
        }

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
        // Change Layer to IgnoreRaycast
        objectBlueprint.layer = 2;

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
                // Change Layer to IgnoreRaycast
                child.gameObject.layer = 2;
            }
        }

        objectBlueprint.AddComponent<Blueprint>();
        objectBlueprint.GetComponent<Blueprint>().buildingScript = transform.GetComponent<Building>();
    }

    public void StopPlacingObject()
    {
        Destroy(objectBlueprint);
    }

    private void PlaceObject()
    {
        Blueprint blueprintScript = objectBlueprint.GetComponent<Blueprint>();
        if (isBuilding && blueprintScript.CheckPlacement())
        {
            // Creates Object
            GameObject placedObject = Instantiate(placingObject);
            placedObject.name = placingObject.name + "Copy";
            StopPlacingObject();
            placedObject.GetComponent<ObjectType>().Destoyable = true;

            // Sets Placed Objects Location
            Vector3 angles = objectBlueprint.transform.eulerAngles;
            angles.x = 0f;
            placedObject.transform.SetPositionAndRotation(objectBlueprint.transform.position, Quaternion.Euler(angles));

            _gameManager.Resume();
            isBuilding = false;
            _interact.isBuilding = false;
            _interact.HoldingCameraStop();
            lookCollider.enabled = true;
        }
        else if (!canPlace)
        {
            UnableToPlace();
        }
    }


    // Check Can Place
    #region Check Can Place
    public void BlueprintStateChange(bool placeable)
    {
        if (!isBuilding) return;
        canPlace = placeable;
        if (!canPlace) UnableToPlace();
        else AbleToPlace();
    }
    #endregion Check Can Place

    private void UnableToPlace()
    {
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

    public Vector3 GetSelectedPosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 25f, placementLayerMask))
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }
}
