using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIReader _UIReader;
    [SerializeField] private UIDocument _UIDoc;
    [SerializeField] private GameObject player;
    
    public float sens;

    private void Start()
    {
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += HandleResume;
        _UIReader.resumeButton.clicked += HandleResume;
        _UIReader.optionsButton.clicked += Options;
        _UIReader.mainMenuButton.clicked += HandleMainMenu;

    }

    public void resetPos()
    {
        player.transform.position = Vector3.zero;
    }

    //UI
    private void HandlePause()
    {
        //_UIDoc.GetComponent<MenuUI>().menu.style.display = DisplayStyle.Flex;
    }

    private void HandleResume()
    {
        //_UIDoc.GetComponent<MenuUI>().menu.style.display = DisplayStyle.None;
    }

    private void Options()
    {
        Debug.Log("Options Menu");
    }

    private void HandleMainMenu()
    {
        Debug.Log("Main Menu");
    }
}
