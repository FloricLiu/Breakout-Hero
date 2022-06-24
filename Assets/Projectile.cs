using UnityEngine;

class Projectile : MonoBehaviour
{
    [SerializeField] private float memberSpeed = 1.0f;
    private Rigidbody2D memberRigidBody = null;

    [SerializeField] private Vector3 memberDirection = Vector3.zero;
    protected void Start()
    {
        memberRigidBody = GetComponent<Rigidbody2D>();
        Vector3 localVelocity = memberSpeed * memberDirection;
        memberRigidBody.velocity = localVelocity;
    }

    protected void OnTriggerEnter2D(Collider2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;

        if (localOtherObject.name == "Ball" || localOtherObject.name == "Top Wall" || localOtherObject.name == "Left Wall" || localOtherObject.name == "Down Wall" || localOtherObject.name == "Right Wall")
        {
            Destroy(gameObject);
        }
    }
}