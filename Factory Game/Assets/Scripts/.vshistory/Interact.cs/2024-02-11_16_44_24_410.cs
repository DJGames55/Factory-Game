using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;

    public Transform holdPos;

    private GameObject heldObject;
    private GameObject canHoldObject;

    private void Start()
    {
        _input.InteractEvent += HandleInteract;
    }

    private void HandleInteract()
    {
        if (canHoldObject != null) 
        { 
            heldObject = canHoldObject;
            canHoldObject = null;
        }
        Debug.Log("Interact");
    }

    private void Update()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = holdPos;
        }
    }


    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canPickUp"))
        {
            canHoldObject = other.gameObject;
            Debug.Log("Can Pick Up");
        }
        else
        {
            Debug.Log("Can't Pick Up");
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
