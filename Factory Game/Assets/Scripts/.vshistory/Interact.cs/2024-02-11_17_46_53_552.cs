using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;

    public Transform holdPos;
    public GameObject pickUpCam;
    public Camera mainCamera;

    private GameObject heldObject;
    private GameObject canHoldObject;

    private void Start()
    {
        _input.InteractEvent += HandleInteract;
        pickUpCam.SetActive(false);

        int holdLayer = LayerMask.NameToLayer("holdLayer");
        mainCamera.cullingMask |= (1 << holdLayer);
    }

    private void HandleInteract()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.GetComponent<Rigidbody>().useGravity = true;
            heldObject = null;
            pickUpCam.SetActive(false);

            int holdLayer = LayerMask.NameToLayer("holdLayer");
            mainCamera.cullingMask |= (1 << holdLayer);
        }
        else if (canHoldObject != null) 
        { 
            heldObject = canHoldObject;
            canHoldObject = null;
            heldObject.GetComponent<Collider>().enabled = false;
            heldObject.GetComponent<Rigidbody>().useGravity = false;
            pickUpCam.SetActive(true);

            int holdLayer = LayerMask.NameToLayer("holdLayer");
            mainCamera.cullingMask &= ~(1 << holdLayer);
        }
        Debug.Log("Interact");
    }

    private void Update()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = new Vector3(holdPos.transform.position.x, holdPos.transform.position.y, holdPos.transform.position.z);
        }
    }


    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canPickUp"))
        {
            canHoldObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canHoldObject = null;
    }
}
