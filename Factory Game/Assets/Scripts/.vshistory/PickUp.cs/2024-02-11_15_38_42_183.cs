using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;

    public Transform holdPos;

    private GameObject heldObj;
    private Rigidbody heldObjRb;

    private void Start()
    {
        _input.InteractEvent += HandleInteract;
    }

    private void HandleInteract()
    {

    }
}
