using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;

    public Transform holdPos;
    public GameObject pickUpCam;
    public Camera mainCamera;
    public TMP_Text interactText;

    public GameObject heldObject;
    public GameObject canHoldObject;

    private void Start()
    {
        _input.InteractEvent += HandleInteract;
        pickUpCam.SetActive(false);

        int holdLayer = LayerMask.NameToLayer("holdLayer");
        mainCamera.cullingMask |= (1 << holdLayer);
    }

    public void HandleInteract()
    {
        if (heldObject != null)
        {
            interactText.enabled = false;
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject = null;
            HoldingCameraStop();
        }
        else if (canHoldObject != null)
        {
            interactText.enabled = true;
            interactText.text = "E To Drop";
            heldObject = canHoldObject;
            canHoldObject = null;
            heldObject.GetComponent<Collider>().enabled = false;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            HoldingCameraSet();
        }
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
        if (heldObject != null)
        {
            interactText.enabled = true;
            interactText.text = "E To Drop";
            heldObject.transform.position = new Vector3(holdPos.transform.position.x, holdPos.transform.position.y, holdPos.transform.position.z);
        }
    }


    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canPickUp"))
        {
            ///interactText.enabled = true;
            ///interactText.text = "E To Pick Up";
            canHoldObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ///interactText.enabled = false;
        canHoldObject = null;
    }
}
