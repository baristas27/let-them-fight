using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    // --INPUTS--
    [Header("Target Matching")]
    [Tooltip("The target 'Ghost' bone (Animation Target) that this physical bone will mimic.")]
    public Transform targetLimb;

    // --CACHE--
    private ConfigurableJoint cj;
    private Quaternion startRotation;

    private void Awake()
    {
        // component caching
        cj = GetComponent<ConfigurableJoint>();

        // capturing the initial rotation
        startRotation = transform.localRotation;
    }

    private void FixedUpdate()
    {
        if (targetLimb == null) return;

        // mathematical mimicry
        cj.targetRotation = Quaternion.Inverse(targetLimb.localRotation) * startRotation;

    }
}
