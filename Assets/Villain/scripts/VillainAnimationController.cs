using UnityEngine;

public class VillainAnimationController : MonoBehaviour
{
    private Animator anim;
    
    // State parameter name
    private static readonly string STATE_PARAM = "state";
    
    // State values
    public static class States
    {
        public const int Idle = 0;
        public const int Running = 1;
        public const int Attack = 2;
        public const int Death = 3;
    }
    
    private int currentState;

    void Start() 
    {
        anim = GetComponent<Animator>();
        SetState(States.Idle); // Start in idle state
    }

    public void SetState(int newState)
    {
        if (currentState != newState)
        {
            anim.SetInteger(STATE_PARAM, newState);
            currentState = newState;
        }
    }

    public float GetClipLength(string clipName)
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        foreach (AnimatorClipInfo clip in clipInfo)
        {
            if (clip.clip.name == clipName)
                return clip.clip.length;
        }
        return 1f;
    }
}