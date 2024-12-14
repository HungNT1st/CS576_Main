using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private FirstPersonController firstPersonController;
    public Canvas canvas;
    private Transform button;
    private Transform text1;
    private Transform text2;

    private void Start()
    {
        button = canvas.transform.Find("Image");
        text1 = canvas.transform.Find("PressE");
        text2 = canvas.transform.Find("TalkWith");

        if (button != null) button.gameObject.SetActive(false);
        if (text1 != null) text1.gameObject.SetActive(false);
        if (text2 != null) text2.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstPersonController = other.GetComponent<FirstPersonController>();
            if (firstPersonController == null)
            {
                Debug.LogWarning("FirstPersonController component not found on Player.");
            }

            if (button != null) button.gameObject.SetActive(true);
            if (text1 != null) text1.gameObject.SetActive(true);
            if (text2 != null) text2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (button != null) button.gameObject.SetActive(false);
            if (text1 != null) text1.gameObject.SetActive(false);
            if (text2 != null) text2.gameObject.SetActive(false);

            if (firstPersonController != null)
            {
                firstPersonController.playerCanMove = false;
                firstPersonController.cameraCanMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("Player movement and camera disabled, cursor unlocked.");
            }
            else
            {
                Debug.LogWarning("FirstPersonController is null, cannot disable movement and camera.");
            }

            if (ConversationManager.Instance != null)
            {
                ConversationManager.Instance.StartConversation(myConversation);
                ConversationManager.OnConversationEnded -= EnablePlayerMovement; 
                ConversationManager.OnConversationEnded += EnablePlayerMovement;
            }
            else
            {
                Debug.LogError("ConversationManager.Instance is null.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (button != null) button.gameObject.SetActive(false);
            if (text1 != null) text1.gameObject.SetActive(false);
            if (text2 != null) text2.gameObject.SetActive(false);
        }
    }

    private void EnablePlayerMovement()
    {
        if (firstPersonController != null)
        {
            firstPersonController.playerCanMove = true;
            firstPersonController.cameraCanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Player movement and camera enabled, cursor locked.");
        }
        else
        {
            Debug.LogWarning("FirstPersonController is null, cannot enable movement and camera.");
        }

        if (button != null) button.gameObject.SetActive(false);
        if (text1 != null) text1.gameObject.SetActive(false);
        if (text2 != null) text2.gameObject.SetActive(false);

        if (ConversationManager.Instance != null)
        {
            ConversationManager.OnConversationEnded -= EnablePlayerMovement;
        }
    }

    private void OnDisable()
    {
        if (ConversationManager.Instance != null)
        {
            ConversationManager.OnConversationEnded -= EnablePlayerMovement;
        }
    }
}
