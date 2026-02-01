using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputReader _input;
    [SerializeField] private UIReader _UIReader;
    [SerializeField] private GameObject player;
    [SerializeField] private BuildMenu _buildMenu;
    
    public float sens;

    private void Start()
    {
        Resume();
        
        //Input
        _input.PauseEvent += HandlePause;
        _input.ResumeEvent += Resume;

        _input.OpenBuildEvent += BuildMenu;

        //UI
        #region UI
        //Buttons
        //Menu
        _UIReader.resumeButton.clicked += Resume;
        _UIReader.optionsButton.clicked += Options;
        _UIReader.mainMenuButton.clicked += HandleMainMenu;
        //Options
        _UIReader.sensSlider.RegisterValueChangedCallback(OnSensChanged);
        _UIReader.resetPosButton.clicked += resetPos;
        _UIReader.backButton.clicked += HandlePause;
        #endregion UI
    }
    private void Resume()
    {
        _input.SetGameplay();
        _UIReader.pauseBackground.style.display = DisplayStyle.None;
        _UIReader.buildMenuBackground.style.display = DisplayStyle.None;
    }

    //Pause Menu
    #region PauseMenu

    private void HandlePause()
    {
        _UIReader.menu.style.display = DisplayStyle.Flex;
        _UIReader.optionsMenu.style.display = DisplayStyle.None;
        _input.SetUI();
        _UIReader.pauseBackground.style.display = DisplayStyle.Flex;
    }


    private void Options()
    {
        _UIReader.menu.style.display = DisplayStyle.None;
        _UIReader.optionsMenu.style.display = DisplayStyle.Flex;
    }

    private void OnSensChanged(ChangeEvent<int> evt)
    {
        sens = evt.newValue;
        _UIReader.sensLable.text = "Sensitivity: " + evt.newValue;
    }

    public void resetPos()
    {
        player.transform.position = Vector3.zero;
    }

    private void HandleMainMenu()
    {
        Debug.Log("Main Menu");
    }
    #endregion PauseMenu

    //Build Menu
    #region BuildMenu

    private void BuildMenu()
    {
        _UIReader.buildMenuBackground.style.display = DisplayStyle.Flex;
        _input.SetUI();
        _buildMenu.BuildMenuOpen();
    }

    #endregion BuildMenu
}
