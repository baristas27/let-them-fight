using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private List<Rigidbody> ragdollRigidbodies;

    private Animator animator;

    private Collider mainCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mainCollider = GetComponent<Collider>();

        ragdollRigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        ragdollRigidbodies.Remove(GetComponent<Rigidbody>());

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdollActive)
    {
        //if(animator != null)
        //{
        //    animator.enabled = !isRagdollActive;
        //}


        if(mainCollider != null)
        {
            mainCollider.enabled = !isRagdollActive;
        }

        Rigidbody mainRb = GetComponent<Rigidbody>();
        if(mainRb != null)
        {
            mainRb.isKinematic = isRagdollActive;
        }





        foreach(Rigidbody rb in ragdollRigidbodies)
        {
            if(rb == null) continue;


            rb.isKinematic = !isRagdollActive;

            rb.detectCollisions = isRagdollActive;

        }
    }






}
