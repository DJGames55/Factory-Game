using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : MonoBehaviour
{
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

    [Header("Blueprint")]
    public Material blueprintMaterial, clearRedMaterial;
    public GameObject[] gameObjects;
    
    public GameObject holdPosition;
    public int holdLayer;
    private GameObject placingObject;
    [HideInInspector] public bool isBuilding;

    private Collider blueprintCollider;
    private bool canPlace;
    public Collider lookCollider;

    private void Update()
    {

        if (isBuilding && objectBlueprint != null)
        {
            objectBlueprint.transform.position = gridPointer.transform.position;
        }

        Vector3 mousePosition = GetSelectedPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        gridPointer.transform.position = gridPosition;
    }

    private void Start()
    {
        ///_input.PlaceObjectEvent += PlaceObject;
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
        // Change Layer
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
                // Change Layer
                child.gameObject.layer = 2;
            }
        }
    }

    public void StopPlacingObject()
    {
        Destroy(objectBlueprint);
    }

    /***
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
            lookCollider.enabled = true;
        }
        else if (!canPlace)
        {
            StartCoroutine(UnableToPlace());
        }
    }


    // Check Can Place
    #region Check Can Place
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collider " + collider);
        if (isBuilding && collider == blueprintCollider)
        {
            Debug.Log("blue collider" + collider);
            canPlace = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (isBuilding && collider == blueprintCollider)
        {
            canPlace = true;
            AbleToPlace();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (isBuilding && collider == blueprintCollider)
        {
            canPlace = false;
        }
    }
    #endregion Check Can Place

    private IEnumerator UnableToPlace()
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

        yield return new WaitForSeconds(1);
        _interact.HoldingCameraSet();
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
    ***/
    public Vector3 GetSelectedPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 25, placementLayerMask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
