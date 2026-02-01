using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIDocument _UIDoc;
    [SerializeField] private GameObject player;
    

    public float sens;

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
    }

    private void HandlePause()
    {
        interactText.enabled = false;
        pauseMenu.SetActive(true);
    }

    private void HandleResume()
    {
        interactText.enabled = true;
        pauseMenu.SetActive(false);
    }

    public void resetPos()
    {
        player.transform.position = Vector3.zero;
    }
}
