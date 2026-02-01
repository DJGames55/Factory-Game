using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _player;

    public GameObject[] gameObjects;
    
    public GameObject _camera;

    private void Start()
    {
        _input.OpenBuildEvent += HandleOpenBuild;
    }

    private void HandleOpenBuild()
    {
        if (_camera.GetComponent<Interact>().heldObject != null)
        {
            _camera.GetComponent<Interact>().HandleInteract();
        }
        _camera.GetComponent<Interact>().HoldingCameraSet();
}
