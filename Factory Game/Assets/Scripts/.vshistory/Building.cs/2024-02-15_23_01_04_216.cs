using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _player;

    public GameObject bluePrints;
    private Component interactScript;

    private void Start()
    {
        interactScript = _player.GetComponent<Interact>().
        _input.OpenBuildEvent += OpenBuild();
    }

    private void OpenBuild()
    {
        _player.GetComponent<Interact>().HoldingCameraSet();
        if (_player.GetComponent<Interact>().canHoldObject != null)
        {
            _player.GetComponent<Interact>().HandleInteract();
        }
    }
}
