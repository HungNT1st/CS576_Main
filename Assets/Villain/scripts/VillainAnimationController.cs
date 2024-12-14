using UnityEngine;

public class VillainAnimationController : MonoBehaviour
{
    private Animator anim;
    
    // animation states
    public static readonly string Idle = "Idle";
    public static readonly string Attack = "AttackHor";
    public static readonly string Running = "Running";
    public static readonly string Death = "Death";
    public static string curState;

    void Start() 
    {
        anim = GetComponent<Animator>();
    }

    public void CrossFade(string state, float transitionDuration)
    {
        if (curState != state)
        {
            anim.CrossFade(state, transitionDuration, 0);
            curState = state;
        }
    }
}