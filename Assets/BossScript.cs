using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> memberRuneList = new List<GameObject>();
    [SerializeField] private GameObject memberBloodPrefab = null;
    public float memberTeleportDuration = 5.0f;
    public int maxHealth = 100;
    public int currentHealth;
    public HP HP;
    private Rigidbody2D memberRigidBody = null;
    //time
    private float memberCooldownDuration = 3.0f;
    private float memberTimer = 0.0f;
    [SerializeField] private GameObject memberPrefab1 = null;
    [SerializeField] private GameObject memberPrefab2 = null;
    [SerializeField] private GameObject memberPrefab3 = null;
    [SerializeField] private GameObject memberPrefab4 = null;
    [SerializeField] private GameObject memberPrefab5 = null;
    [SerializeField] private GameObject memberPrefab6 = null;
    [SerializeField] private GameObject memberPrefab7 = null;
    [SerializeField] private GameObject memberPrefab8 = null;
    [SerializeField] private GameObject memberMobPrefab = null;
    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] float maxDistance;
    Vector2 wayPoint;
    enum State
    {
        eTeleport,
        eAttack1,
        eAttack2,
        eAttack3,
        eSummon
    }
    State memberState;
    protected void Start()
    {
        memberState = State.eTeleport;
        memberTimer = memberTeleportDuration;
        memberRigidBody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        HP.SetHealth(currentHealth, maxHealth);
        memberTimer = memberCooldownDuration;
        SetNewDestination();
        //memberRigidBody.velocity = memberSpeed * memberDirection;
    }


    protected void Update()
    {
        switch (memberState)
        {
            case (State.eTeleport):
                {
                    //timer
                    memberTimer -= Time.deltaTime;
                    //teleport
                    if (memberTimer <= 0.0f)
                    {
                        int localNumRune = memberRuneList.Count;
                        int localRuneIndex = Random.Range(0, localNumRune);
                        GameObject localRuneObject = memberRuneList[localRuneIndex];
                        Vector3 localPosition = localRuneObject.transform.position;
                        this.transform.position = localPosition;
                        memberTimer = memberCooldownDuration;
                        memberState = State.eAttack1;
                    }
                }
                break;

            case (State.eAttack1):
                {
                    //timer
                    memberTimer -= Time.deltaTime;
                    if (memberTimer <= 0.0f)
                    {
                        Vector3 localPosition = this.transform.position;
                        Instantiate(memberPrefab1, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab2, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab3, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab4, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab5, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab6, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab7, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab8, localPosition, Quaternion.identity);
                        memberTimer = memberTeleportDuration;
                        memberState = State.eAttack2;
                    }
                }
                break;
            case (State.eAttack2):
                {
                    //timer
                    memberTimer -= Time.deltaTime;
                    if (memberTimer <= 0.0f)
                    {
                        Vector3 localPosition = this.transform.position;
                        Instantiate(memberPrefab1, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab2, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab3, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab4, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab5, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab6, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab7, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab8, localPosition, Quaternion.identity);
                        memberTimer = memberTeleportDuration;
                        memberState = State.eAttack3;
                    }
                }
                break;
            case (State.eAttack3):
                {
                    //timer
                    memberTimer -= Time.deltaTime;
                    if (memberTimer <= 0.0f)
                    {
                        Vector3 localPosition = this.transform.position;
                        Instantiate(memberPrefab1, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab2, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab3, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab4, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab5, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab6, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab7, localPosition, Quaternion.identity);
                        Instantiate(memberPrefab8, localPosition, Quaternion.identity);
                        memberTimer = memberTeleportDuration;
                        memberState = State.eSummon;
                    }
                }
                break;
            case (State.eSummon):
                {
                    //timer
                    memberTimer -= Time.deltaTime;
                    int localNumRune = memberRuneList.Count;
                    int localRuneIndex = Random.Range(0, localNumRune);
                    GameObject localRuneObject = memberRuneList[localRuneIndex];
                    Vector3 localPosition = localRuneObject.transform.position;
                    if (memberTimer <= 0.0f)
                    {
                        Instantiate(memberMobPrefab, localPosition, Quaternion.identity);
                        memberTimer = memberTeleportDuration;
                        memberState = State.eTeleport;
                    }
                }
                break;
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(memberBloodPrefab, this.transform.position, Quaternion.identity);
        }

        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
    }
    protected void OnCollisionEnter2D(Collision2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;

        if (localOtherObject.name == "Ball")
        {
            currentHealth -= 3;
            HP.SetHealth(currentHealth, maxHealth);
        }
    }
    protected void OnTriggerEnter2D(Collider2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;
        if (localOtherObject.name == "Skill")
        {
            currentHealth -= 6;
            HP.SetHealth(currentHealth, maxHealth);
        }
    }
    void SetNewDestination()
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(0, 4));
    }
}
