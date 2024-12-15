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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSpraying();
        }
        else if (Input.GetMouseButtonUp(0))
        {
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
        if (waterSprayEffect != null)
        {
            waterSprayEffect.Play();
        }
    }

    private void StopSpraying()
    {
        isSpraying = false;
        if (waterSprayEffect != null)
        {
            waterSprayEffect.Stop();
        }
    }

    private void SprayWater()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, waterRange, fireLayer))
        {
            FireManager fire = hit.collider.GetComponent<FireManager>();
            if (fire != null)
            {
                fire.ApplyWater(waterAmount * Time.deltaTime);
            }
        }
    }
} 