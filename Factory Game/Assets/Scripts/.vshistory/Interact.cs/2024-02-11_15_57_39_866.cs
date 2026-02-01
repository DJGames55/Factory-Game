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
}
