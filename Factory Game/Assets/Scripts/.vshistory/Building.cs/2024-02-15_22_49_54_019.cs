using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _player;

    public GameObject bluePrint;

    private void Start()
    {
        _player.GetComponent<Interact>();
    }


}
