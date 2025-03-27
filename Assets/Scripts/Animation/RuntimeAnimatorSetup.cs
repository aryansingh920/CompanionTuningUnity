using UnityEngine;
using UnityEditor.Animations;

public class RuntimeAnimatorSetup : MonoBehaviour
{
    public string idleClipName = "Idle";     // Name of animation inside the FBX
    public string walkClipName = "Walking";  // Name of animation inside the FBX

    void Awake()
    {
        Animator animator = GetComponent<Animator>();

        // Load FBX model that contains animations from Resources
        Object[] idleAssets = Resources.LoadAll("Animations/Idle");
        Object[] walkAssets = Resources.LoadAll("Animations/Walking");

        AnimationClip idleClip = null;
        AnimationClip walkClip = null;

        foreach (var obj in idleAssets)
        {
            if (obj is AnimationClip clip && clip.name.Contains(idleClipName))
            {
                idleClip = clip;
                break;
            }
        }

        foreach (var obj in walkAssets)
        {
            if (obj is AnimationClip clip && clip.name.Contains(walkClipName))
            {
                walkClip = clip;
                break;
            }
        }

        if (!idleClip || !walkClip)
        {
            Debug.LogError("Animation clips not found in FBX files. Ensure FBXs are inside Resources/Animations and clip names are correct.");
            return;
        }

        // Create AnimatorController
        AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/TempCompanionController.controller");
        animator.runtimeAnimatorController = animatorController;

        // Add parameters
        animatorController.AddParameter("isWalking", AnimatorControllerParameterType.Bool);

        // Add states
        var rootStateMachine = animatorController.layers[0].stateMachine;
        AnimatorState idleState = rootStateMachine.AddState("Idle");
        idleState.motion = idleClip;

        AnimatorState walkState = rootStateMachine.AddState("Walk");
        walkState.motion = walkClip;

        // Transitions
        var toWalk = idleState.AddTransition(walkState);
        toWalk.AddCondition(AnimatorConditionMode.If, 0, "isWalking");
        toWalk.hasExitTime = false;

        var toIdle = walkState.AddTransition(idleState);
        toIdle.AddCondition(AnimatorConditionMode.IfNot, 0, "isWalking");
        toIdle.hasExitTime = false;
    }
}
