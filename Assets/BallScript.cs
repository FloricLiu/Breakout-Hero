using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript: MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 500f;
    public int maxHealth = 100;
    public int currentHealth;
    public int maxMana = 100;
    public int currentMana;
    public MP MP;
    public GameObject Skill;
    public GameObject Full;
    //time
    private float memberCooldownDuration = 5.0f;
    private float memberCooldownTimer = 0.0f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        currentMana = 20;
        MP.SetHealth(currentHealth, maxHealth);
        MP.SetMana(currentMana, maxMana);
    }

    private void Start()
    {
        ResetBall();
        Full.gameObject.SetActive(false);
        Skill.gameObject.SetActive(false);
        memberCooldownTimer = memberCooldownDuration;
    }
    private void Update()
    {
        memberCooldownTimer -= Time.deltaTime;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (currentMana >= 100)
        {
            currentMana = 100;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                memberCooldownTimer = memberCooldownDuration;
                Skill.gameObject.SetActive(true);
                currentMana = 0;
                MP.SetMana(currentMana, maxMana);
            }
            Full.gameObject.SetActive(true);
        }
        if (currentMana < 100)
        {
            Full.gameObject.SetActive(false);
        }
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        if (memberCooldownTimer <= 0.0f)
        {
            Skill.gameObject.SetActive(false);
        }
        this.rigidbody.velocity = 6.0f * (rigidbody.velocity.normalized);
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBall();
        }
    }
    public void ResetBall()
    {
        this.rigidbody.velocity = Vector2.zero;
        this.transform.position = Vector2.zero;

        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = new Vector2();
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }
    protected void OnCollisionEnter2D(Collision2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;

        if (localOtherObject.name == "Down Wall")
        {
            currentHealth -= 10;
            MP.SetHealth(currentHealth, maxHealth);
        }

        if (localOtherObject.name == "Boss" || localOtherObject.name.StartsWith("Mob"))
        {
            currentMana += 10;
            MP.SetMana(currentMana, maxMana);
        }
    }
    protected void OnTriggerEnter2D(Collider2D localCollider)
    {
        GameObject localOtherObject = localCollider.gameObject;
        if (localOtherObject.name.StartsWith("Fire"))
        {
            currentHealth -= 5;
            MP.SetHealth(currentHealth, maxHealth);
        }
        if (localOtherObject.name.StartsWith("Red"))
        {
            currentHealth += 20;
            MP.SetHealth(currentHealth, maxHealth);
            Destroy(localOtherObject);
        }
        if (localOtherObject.name.StartsWith("Blue"))
        {
            currentMana += 20;
            MP.SetMana(currentMana, maxMana);
            Destroy(localOtherObject);
        }
    }
}
