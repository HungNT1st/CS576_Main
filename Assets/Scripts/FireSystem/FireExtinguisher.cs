using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    [Header("Water Settings")]
    [SerializeField] private float waterAmount = 1f;
    [SerializeField] private float waterRange = 5f;
    [SerializeField] private LayerMask fireLayer;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem waterSprayEffect;
    
    private bool isSpraying = false;

    void Start()
    {
        StopSpraying();
        waterSprayEffect.Stop();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isSpraying)
                StartSpraying();
            else
                StopSpraying();
        }
        
        if (isSpraying)
        {
            SprayWater();
        }
    }

    private void StartSpraying()
    {
        isSpraying = true;
        //AudioManager.Instance.PlayAudioGroup("WATER");
        if (waterSprayEffect != null)
        {
            waterSprayEffect.Play();
        }
    }

    private void StopSpraying()
    {
        isSpraying = false;
        //AudioManager.Instance.StopAudioGroup("WATER");
        if (waterSprayEffect != null)
        {
            waterSprayEffect.Stop();
        }
    }

    private void SprayWater()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * waterRange, Color.blue); // Debug visualization

        if (Physics.Raycast(ray, out hit, waterRange, fireLayer))
        {
            Debug.Log($"Hit something on layer {hit.collider.gameObject.layer}"); // Debug log
            FireManager fire = hit.collider.GetComponent<FireManager>();
            if (fire != null)
            {
                fire.ApplyWater(waterAmount * Time.deltaTime);
                Debug.Log($"Applying water to fire: {waterAmount * Time.deltaTime}"); // Debug log
            }
        }
    }
} 