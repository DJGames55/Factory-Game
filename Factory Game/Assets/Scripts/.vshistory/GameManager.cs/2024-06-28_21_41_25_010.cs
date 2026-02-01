using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIReader _UIReader;
    [SerializeField] private GameObject player;
    [SerializeField] private Interact _interact;
    
    public float sens;

    private void Start()
    {
        Resume();

        #region Input
        // Input
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += Resume;

        _input.OpenBuildEvent += BuildMenu;
        _input.ExitBuildEvent += ExitBuild;
        #endregion Input

        // UI
        #region UI
        // Buttons
        // Menu
        _UIReader.resumeButton.clicked += Resume;
        _UIReader.optionsButton.clicked += Options;
        _UIReader.mainMenuButton.clicked += HandleMainMenu;
        // Options
        _UIReader.sensSlider.RegisterValueChangedCallback(OnSensChanged);
        _UIReader.resetPosButton.clicked += ResetPos;
        _UIReader.backButton.clicked += HandlePause;
        #endregion UI
    }

    public void Resume()
    {
        _input.SetGameplay();
        _UIReader.pauseBackground.style.display = DisplayStyle.None;
        _UIReader.buildMenuBackground.style.display = DisplayStyle.None;
    }

    // Pause Menu
    #region PauseMenu

    private void HandlePause()
    {
        // Opens the Pause Menu
        _UIReader.menu.style.display = DisplayStyle.Flex;
        _UIReader.optionsMenu.style.display = DisplayStyle.None;
        _input.SetUI();
        _UIReader.pauseBackground.style.display = DisplayStyle.Flex;
    }


    private void Options()
    {
        // Changes to options Menu
        _UIReader.menu.style.display = DisplayStyle.None;
        _UIReader.optionsMenu.style.display = DisplayStyle.Flex;
    }

    private void OnSensChanged(ChangeEvent<int> evt)
    {
        // Changes Sensitivity with Slider
        sens = evt.newValue;
        _UIReader.sensLable.text = "Sensitivity: " + evt.newValue;
    }

    // Resets Player position
    public void ResetPos()
    {
        player.transform.position = Vector3.zero;
    }

    private void HandleMainMenu()
    {
        /// To Do
        Debug.Log("Main Menu");
    }
    #endregion PauseMenu

    // Build Menu
    #region Building

    private void BuildMenu()
    {
        if (_interact.heldObject != null)
        {
            _interact.DropItem();
        }

        _input.SetUI();
        _UIReader.buildMenuBackground.style.display = DisplayStyle.Flex;
    }

    public void SetBuilding()
    {
        _input.SetBuild();
        _UIReader.buildMenuBackground.style.display = DisplayStyle.None;
        _interact.isBuilding = true;
    }

    public void ExitBuild()
    {
        _input.SetUI();
        _UIReader.buildMenuBackground.style.display = DisplayStyle.Flex;
        _interact.isBuilding = false;
    }

    #endregion Building
}
