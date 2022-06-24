/*Mob Script*/
/*liu chen han*/
using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject memberBloodPrefab = null;

    [SerializeField] private float memberSpeed = 2.0f;
    [SerializeField] private Vector3 memberDirection = Vector3.zero;
    public int maxHealth = 100;
    public int currentHealth;
    public HP HP;
    [SerializeField] private GameObject memberPrefab1 = null;
    [SerializeField] private GameObject memberPrefab2 = null;
    [SerializeField] private GameObject memberPrefab3 = null;
    [SerializeField] private Transform Ball = null;
    [SerializeField] private GameObject Blue = null;
    [SerializeField] private GameObject Red = null;
    //time
    public float memberCooldownDuration = 5.0f;
    private float memberCooldownTimer = 0.0f;
    private Rigidbody2D memberRigidBody = null;

    protected void Start()
    {
        memberRigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        HP.SetHealth(currentHealth, maxHealth);
        memberCooldownTimer = memberCooldownDuration;
        memberRigidBody.velocity = memberSpeed * memberDirection;
        Ball = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected void Update()
    {
        if (Vector2.Distance(transform.position, Ball.position) > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, Ball.position, memberSpeed * Time.deltaTime);
        }
        transform.position += transform.forward * Time.deltaTime * memberSpeed;
        memberRigidBody.velocity = memberSpeed * memberDirection;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            int num = Random.Range(1, 3);
            if (num == 1)
            {
                Instantiate(Red, this.transform.position, Quaternion.identity);
            }
            if (num == 2)
            {
                Instantiate(Blue, this.transform.position, Quaternion.identity);
            }
        }
        //throw weapon
        memberCooldownTimer -= Time.deltaTime;
        if (memberCooldownTimer <= 0.0f)
        {
            Vector3 localPosition = this.transform.position;
            Instantiate(memberPrefab1, localPosition, Quaternion.identity);
            Instantiate(memberPrefab2, localPosition, Quaternion.identity);
            Instantiate(memberPrefab3, localPosition, Quaternion.identity);
            memberCooldownTimer = memberCooldownDuration;
        }
    }
    protected void OnTriggerEnter2D(Collider2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;

        if (localOtherObject.name.StartsWith("Arrow") || localOtherObject.name == "Hero")
        {
            this.gameObject.SetActive(false);
            Vector3 localPosition;
            localPosition = this.transform.position;
            Instantiate(memberBloodPrefab, localPosition, Quaternion.identity);

        }
        if (localOtherObject.name.StartsWith("Bumper") || localOtherObject.name.StartsWith("Wall"))
        {
            memberRigidBody.velocity = Vector2.zero;
        }
        if (localOtherObject.name == "Skill")
        {
            currentHealth -= 500;
            HP.SetHealth(currentHealth, maxHealth);
        }
    }
    protected void OnTriggerStay2D(Collider2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;
        if (localOtherObject.name.StartsWith("Spinner"))
        {

            if (Vector3.Distance(this.transform.position, localOtherObject.transform.position) <= 0.01f)
            {
                // The knight is at the center of the spinner, turn now!
                // Change the knight's direction to the spinner's up vector.

                memberDirection = localOtherObject.transform.up;
            }
        }
    }
    protected void OnCollisionEnter2D(Collision2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;

        if (localOtherObject.name == "Ball")
        {
            currentHealth -= 35;
            HP.SetHealth(currentHealth, maxHealth);
        }
    }
}