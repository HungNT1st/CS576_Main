using UnityEngine;
using UnityEngine.UI; 

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject mainMenuCanvas;     
    public GameObject instructionCanvas;  

    public Button instructionButton;      
    public Button backButton;             
    void Start()
    {
        instructionButton.onClick.AddListener(ShowInstructionCanvas);
        backButton.onClick.AddListener(ShowMainMenuCanvas);

        mainMenuCanvas.SetActive(true);
        instructionCanvas.SetActive(false);
    }

    public void ShowInstructionCanvas()
    {
        mainMenuCanvas.SetActive(false);
        instructionCanvas.SetActive(true);
    }

    public void ShowMainMenuCanvas()
    {
        instructionCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
}
