using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : Singleton<HUD>
{
    [SerializeField] RectTransform textPopUpRect;
    [SerializeField] TextMeshProUGUI textPopUpTMP;
    const int POPUP_DELTA_Y = 250;
    [Header("Health")]
    [SerializeField] Image environmentHealth;
    [SerializeField] Image playerHealth;
    [Header("Small task")]
    [SerializeField] TextMeshProUGUI smallTaskTMP;
    [SerializeField] Image smallTaskLoading;
    [Header("Screens")]
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] FirstPersonController firstPersonController;

    private void Start() {
        StopSmallTaskLoading();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        textPopUpRect.DOMoveY(-POPUP_DELTA_Y, 0f);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            Cursor.lockState = Time.timeScale == 0f ? CursorLockMode.None : CursorLockMode.Locked;

            
            firstPersonController.playerCanMove = (!firstPersonController.playerCanMove);
            firstPersonController.cameraCanMove = (!firstPersonController.cameraCanMove);   
        }
        
    }
    bool isPoping = false;
    public void PopUpText(string txt, float duration) {
        if (isPoping) return;
        isPoping = true;
        textPopUpTMP.text = txt;
        textPopUpRect.DOMoveY(POPUP_DELTA_Y, 0.5f).onComplete += () => {
            StartCoroutine(wait());
            IEnumerator wait()
            {
                yield return new WaitForSeconds(duration);
                textPopUpRect.DOMoveY(-POPUP_DELTA_Y, 0.5f);
                isPoping = false;
            }
        };
    }
    
    public void GameOver(bool isWin) {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        if (isWin) {
            winScreen.SetActive(true);
        }
        else {
            loseScreen.SetActive(true);
        }
    }

    public void SetEnvironmentHealth(float val) {
        SetImageFill(environmentHealth, val);
    }
    public void SetPlayerHealth(float val) {
        SetImageFill(playerHealth, val);
    }
    private void SetImageFill(Image img, float val) {
        if (val < 0 || val > 1) throw new Exception("health value should be between 0 and 1");
        img.DOFillAmount(val, 0.5f);
    }

    public void IncreaseEnvironmentHealth(float delta) {
        SetEnvironmentHealth(environmentHealth.fillAmount + delta);
    }

    [SerializeField] TextMeshProUGUI missionDisplay;
    private List<string> missions = new();
    public void AddMission(string mission) {
        missions.Add(mission);
        updateMissionDisplay();
    }
    public void RemoveMission(string mission) {
        int i = mission.IndexOf(mission);
        if (i != -1) {
            mission.Remove(i);
        }
        updateMissionDisplay();
    }

    private void updateMissionDisplay() {
        missionDisplay.text = "";
        string str = "";
        missions.ForEach(e => str += "- e \n");
    }

    
    public DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> SetSmallTaskLoading(string taskName, float duration) {
        smallTaskTMP.text = taskName;
        smallTaskLoading.fillAmount = 1f;
        var r = smallTaskLoading.DOFillAmount(0f, duration);
        r.onComplete += StopSmallTaskLoading;
        return r;
    }

    public void StopSmallTaskLoading() {
        DOTween.Kill(smallTaskLoading);
        smallTaskLoading.fillAmount = 0f;
        smallTaskTMP.text = "";
    }

}
