using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIReader _UIReader;
    [SerializeField] private GameObject player;
    [SerializeField] private Interact _interact;
    [SerializeField] private Building _building;
    [SerializeField] private PlayerControls _playerControls;
    
    public float sens;
    [ReadOnly] public bool gamePaused;

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
        _playerControls.sensitivity = sens * 10;
        _UIReader.resetPosButton.clicked += ResetPos;
        _UIReader.backButton.clicked += HandlePause;
        #endregion UI
    }

    // Resumes from a Menu
    public void Resume()
    {
        _input.SetGameplay();
        _UIReader.pauseBackground.style.display = DisplayStyle.None;
        _UIReader.buildMenuBackground.style.display = DisplayStyle.None;
    }

    // Pause Menu
    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }

    #region PauseMenu

    // Opens the Pause Menu
    private void HandlePause()
    {
        _UIReader.menu.style.display = DisplayStyle.Flex;
        _UIReader.optionsMenu.style.display = DisplayStyle.None;
        _input.SetUI();
        _UIReader.pauseBackground.style.display = DisplayStyle.Flex;
    }


    // Changes to options Menu
    private void Options()
    {
        _UIReader.menu.style.display = DisplayStyle.None;
        _UIReader.optionsMenu.style.display = DisplayStyle.Flex;
    }

    // Changes Sensitivity with Slider
    private void OnSensChanged(ChangeEvent<int> evt)
    {
        sens = evt.newValue;
        _UIReader.sensLable.text = "Sensitivity: " + evt.newValue;
        _playerControls.sensitivity = sens * 10;
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

    // Opens Build Menu
    private void BuildMenu()
    {
        if (_interact.heldObject != null)
        {
            _interact.DropItem();
        }

        _input.SetUI();
        _UIReader.buildMenuBackground.style.display = DisplayStyle.Flex;
    }

    // Sets Building "Placing" mode
    public void SetBuilding()
    {
        _input.SetBuild();
        _UIReader.buildMenuBackground.style.display = DisplayStyle.None;
        _interact.isBuilding = true;
    }

    // Exits Building "Placing" mode
    public void ExitBuild()
    {
        _input.SetUI();
        _UIReader.buildMenuBackground.style.display = DisplayStyle.Flex;
        _interact.isBuilding = false;
        _building.isBuilding = false;
        _building.StopPlacingObject();
    }

    #endregion Building
}
