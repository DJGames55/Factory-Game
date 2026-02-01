using UnityEngine.UIElements;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private UIReader _UIReader;

    public Transform holdPos;
    public GameObject pickUpCam;
    public Camera mainCamera;
    private Label interactText;

    public GameObject canHoldObject;
    public GameObject heldObject;
    public int objectLayer;
    public int holdLayer;
    private GameObject tempHoldObject;

    public bool isBuilding;

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

    public void PickUpItem()
    {
        interactText.style.display = DisplayStyle.Flex;
        interactText.text = "E To Drop";
        heldObject = canHoldObject;
        canHoldObject = null;

        heldObject.GetComponent<Collider>().enabled = false;
        heldObject.GetComponent<Rigidbody>().useGravity = false;

        objectLayer = heldObject.layer;
        heldObject.layer = holdLayer;

        HoldingCameraSet();
    }

    public void DropItem()
    {
        interactText.style.display = DisplayStyle.None;
        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.GetComponent<Rigidbody>().useGravity = true;

        heldObject = null;
        tempHoldObject = canHoldObject;
        canHoldObject = null;

        HoldingCameraStop();
    }

    public void HoldingCameraSet()
    {
        pickUpCam.SetActive(true);

        int holdLayer = LayerMask.NameToLayer("holdLayer");
        mainCamera.cullingMask &= ~(1 << holdLayer);
    }

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
                heldObject.transform.position = new Vector3(holdPos.transform.position.x, holdPos.transform.position.y, holdPos.transform.position.z);
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


    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (!isBuilding)
        { 
            if (other.gameObject.CompareTag("canPickUp"))
            {
                interactText.style.display = DisplayStyle.Flex;
                interactText.text = "Press E To Pick Up";
                canHoldObject = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactText.style.display = DisplayStyle.None;
        canHoldObject = null;
    }
}
