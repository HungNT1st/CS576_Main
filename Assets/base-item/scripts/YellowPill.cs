// using UnityEngine;

// public class YellowPill : BasePill
// {
//     [SerializeField] private float speedMultiplier = 0.5f;

//     private void Awake()
//     {
//         duration = 5f;
//     }

//     protected override void ActivateEffect()
//     {
//         base.ActivateEffect();
        
//         VillainBehavior villain = GameObject.FindGameObjectWithTag("Player").GetComponent<VillainBehavior>();
//         if (villain != null)
//         {
//             villain.SetSpeedMultiplier(speedMultiplier);
//         }
//     }

//     protected override void DeactivateEffect()
//     {
//         VillainBehavior villain = GameObject.FindGameObjectWithTag("Player").GetComponent<VillainBehavior>();
//         if (villain != null)
//         {
//             villain.ResetSpeed();
//         }
//         base.DeactivateEffect();
//     }
// } 