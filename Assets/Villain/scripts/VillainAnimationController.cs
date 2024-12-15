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

    public void CrossFade(string state)
    {
        if (curState != state)
        {
            anim.CrossFade(state, 0.01f, 0);
            curState = state;
        }
    }

    public float GetClipLength(string clipName)

    {

        Animator animator = GetComponent<Animator>();

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        foreach (AnimatorClipInfo clip in clipInfo)

        {

            if (clip.clip.name == clipName)

                return clip.clip.length;

        }

        return 1f; 

    }
}