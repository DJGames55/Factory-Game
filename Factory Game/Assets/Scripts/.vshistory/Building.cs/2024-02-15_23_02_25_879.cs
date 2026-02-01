using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _camera;

    public GameObject bluePrints;

    private void Start()
    {
        _input.OpenBuildEvent += OpenBuild();
    }

    private void OpenBuild()
    {
        _camera.GetComponent<Interact>().HoldingCameraSet();
        if (interactScript.canHoldObject != null)
        {
            interactScript.HandleInteract();
        }
    }
}
