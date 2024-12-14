using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIDocument mainMenuDocument;

    private Button settingsButton;
    private Button achievementsButton;
    private Button playButton;

    private void Awake()
    {
        VisualElement root = mainMenuDocument.rootVisualElement;

        settingsButton = root.Q<Button>("SettingButton");
        achievementsButton = root.Q<Button>("AchivementButton");
        playButton = root.Q<Button>("PlayButton");

        settingsButton.clickable.clicked += ShowSettingsMenu;
        achievementsButton.clickable.clicked += ShowAchivementsMenu;
        playButton.clickable.clicked += StartGame;
    }

    private void ShowSettingsMenu()
    {
        print("Show settings menu");
    }

    private void ShowAchivementsMenu()
    {
        print("Show settings menu");
    }

    private void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
