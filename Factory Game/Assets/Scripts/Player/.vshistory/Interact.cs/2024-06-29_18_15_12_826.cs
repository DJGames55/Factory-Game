using UnityEngine.UIElements;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIReader _UIReader;

    public GameObject holdPosition;
    public GameObject pickUpCam;
    public Camera mainCamera;
    private Label interactText;

    [HideInInspector] public GameObject canHoldObject = null;
    [HideInInspector] public GameObject heldObject = null;
    private int objectLayer;
    public int holdLayer;
    private GameObject tempHoldObject;
    private Transform objectParent;

    [HideInInspector] public bool isBuilding;

    private void Start()
    {
        _input.InteractEvent += HandleInteract;
        pickUpCam.SetActive(false);
        interactText = _UIReader.actionPrompt;
        interactText.style.display = DisplayStyle.None;

        int holdLayer = LayerMask.NameToLayer("holdLayer");
        mainCamera.cullingMask |= (1 << holdLayer);
    }

    public void HandleInteract()
    {
        if (heldObject != null)
        {
            DropItem();
        }
        else if (canHoldObject != null)
        {
            PickUpItem();
        }
    }

    // Picks Up Item
    public void PickUpItem()
    {
        interactText.style.display = DisplayStyle.Flex;
        interactText.text = "E To Drop";
        heldObject = canHoldObject;
        canHoldObject = null;

        heldObject.GetComponent<Collider>().enabled = false;
        heldObject.GetComponent<Rigidbody>().useGravity = false;

        // Changes the Objects View Layer
        objectLayer = heldObject.layer;
        heldObject.layer = holdLayer;
        // Changes the Layer of the children
        foreach (Transform child in heldObject.transform)
        {
            child.gameObject.layer = holdLayer;
        }

        // Makes Object child of Hold Position
        objectParent = heldObject.transform.parent;
        heldObject.transform.parent = holdPosition.transform;

        HoldingCameraSet();
    }

    // Drops Item
    public void DropItem()
    {
        interactText.style.display = DisplayStyle.None;
        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.GetComponent<Rigidbody>().useGravity = true;

        // Changes the objects View Layer
        heldObject.layer = objectLayer;
        // Changes the Layer of the children
        foreach (Transform child in heldObject.transform)
        {
            child.gameObject.layer = objectLayer;
        }
        // Set X Rotation to 0
        Vector3 angles = heldObject.transform.rotation.eulerAngles;
        angles.x = 0;
        heldObject.transform.rotation = Quaternion.Euler(angles);

        // Makes Object child of origional Parent
        heldObject.transform.parent = objectParent;
        objectParent = null;

        heldObject = null;
        tempHoldObject = canHoldObject;
        canHoldObject = null;

        HoldingCameraStop();
    }

    // Enables Holding View Camera
    public void HoldingCameraSet()
    {
        pickUpCam.SetActive(true);

        int holdLayer = LayerMask.NameToLayer("holdLayer");
        mainCamera.cullingMask &= ~(1 << holdLayer);
    }

    // Disables Holding View Camera
    public void HoldingCameraStop()
    {
        pickUpCam.SetActive(false);

        int holdLayer = LayerMask.NameToLayer("holdLayer");
        mainCamera.cullingMask |= (1 << holdLayer);
    }

    private void Update()
    {
        if (!isBuilding) 
        {
            if (heldObject != null)
            {
                interactText.style.display = DisplayStyle.Flex;
                interactText.text = "E To Drop";
                // Sets the held Objects position & Rotation
                heldObject.transform.SetPositionAndRotation(holdPosition.transform.position, holdPosition.transform.rotation);
            }
            else if (tempHoldObject != null)
            {
                interactText.style.display = DisplayStyle.Flex;
                interactText.text = "Press E To Pick Up";
                canHoldObject = tempHoldObject;
                tempHoldObject = null;
            }
        }
        else
        {
            interactText.style.display = DisplayStyle.None;
        }
    }


    // Checks if able to pick up object
    private void OnTriggerEnter(Collider other)
    {
        if (!isBuilding)
        {
            if (other.gameObject.GetComponent<ObjectType>() != null)
            {
                if (other.gameObject.GetComponent<ObjectType>().canPickUp)
                {
                    interactText.style.display = DisplayStyle.Flex;
                    interactText.text = "Press E To Pick Up";
                    canHoldObject = other.gameObject;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactText.style.display = DisplayStyle.None;
        canHoldObject = null;
    }
}
