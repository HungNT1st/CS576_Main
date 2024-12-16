using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchSceneBtn : MonoBehaviour
{
    [SerializeField] string sceneName;
    private void Start() {
        GetComponent<Button>().onClick.AddListener(() => {
            DOTween.KillAll();
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1f;
        });
    }
}
