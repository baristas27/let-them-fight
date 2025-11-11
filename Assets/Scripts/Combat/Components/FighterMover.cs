using UnityEngine;

public class FighterMover : MonoBehaviour
{
    public Transform target;         // Inspector'dan atanacak hedef
    public float moveSpeed = 3f;     // Yürüme hýzý

    private Rigidbody rb;            // Rigidbody referansý

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 moveVelocity = direction * moveSpeed;
            rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
        }
    }
}