using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;

    private void Start()
    {
        _input.InteractEvent += HandleInteract;
    }

    private void HandleInteract()
    {
        Debug.Log("Interact");
    }


    // Collision
    private void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("canPickUp"))
        {
            Debug.Log("Can Pick Up");
        }
        else
        {
            Debug.Log("Can't Pick Up");
        }
    }
    private void OnTriggerExit(Collision collision)
    {

    }
}
