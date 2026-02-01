using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameObject _player;

    public GameObject[] gameObjects;
    
    public GameObject _camera;

    public GameObject bluePrints;
    public GameObject bluePrintsImage;
    public TMP_Text bluePrintsText;

    private void Start()
    {
        _input.OpenBuildEvent += HandleOpenBuild;
        bluePrints.SetActive(false);
        ///bluePrintsImage.SetActive(false);
    }

    private void HandleOpenBuild()
    {
        if (_camera.GetComponent<Interact>().heldObject != null)
        {
            _camera.GetComponent<Interact>().HandleInteract();
        }
        _camera.GetComponent<Interact>().HoldingCameraSet();

        bluePrints.SetActive(true);
        ///bluePrintsImage.SetActive(true);
        ///bluePrintsText.text = "";
    }
}
