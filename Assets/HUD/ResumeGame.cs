using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeGame : MonoBehaviour
{
    
    [SerializeField] GameObject pauseScreen;
    
    [SerializeField] FirstPersonController firstPersonController;
    private void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            
            firstPersonController.playerCanMove = true;
            firstPersonController.cameraCanMove = true;
        });
    }
}
