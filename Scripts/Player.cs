using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxSpeed = 4;
    public float jumpForce = 400;
    public float minHeight, maxHeight;
    public int maxHealth = 10;
    public string playerName;
    public Sprite playerImage;
    public AudioClip colisionSound, jumpSound, healthItem;
    public Weapon weapon;

    private int currentHealth;
    private float currentSpeed;
    private Rigidbody rb;
    private Animator anim;
    private Transform GroundCheck;
    private bool onGround;
    private bool isDead = false;
    private bool facingRight = true;
    private bool jump = false;
    private AudioSource audioS;
    private bool holdingWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        GroundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
        currentHealth = maxHealth;
        audioS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Linecast(transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        anim.SetBool("OnGround", onGround);
        anim.SetBool("Dead", isDead);
        anim.SetBool("Weapon", holdingWeapon);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            float h = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (!onGround)
                z = 0;

            rb.velocity = new Vector3(h * currentSpeed, rb.velocity.y, z * currentSpeed);

            if (onGround)
                anim.SetFloat("Speed", Mathf.Abs(rb.velocity.magnitude));

            if (h > 0 && !facingRight)
            {
                Flip();
            }
            else if (h < 0 && facingRight)
            {
                Flip();
            }

            if (jump)
            {
                jump = false;
                rb.AddForce(Vector3.up * jumpForce);
                PlaySong(jumpSound);
            }
            float minWideth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            float maxWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 10)).x;
            rb.position = new Vector3
                (Mathf.Clamp(rb.position.x, minWideth + 1, maxWidth - 1),
                rb.position.y,
                Mathf.Clamp(rb.position.z, minHeight, maxHeight));
        }
    }


    void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = maxSpeed;
    }

    public void TookDamage(int damage)
    {
        if (!isDead)
        {
            //Debug.Log("»÷ÖÐÁË");
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");
           // anim.SetTrigger("Hit");
            FindObjectOfType<UIManager>().updateHealth(currentHealth);
            PlaySong(colisionSound);
            if (currentHealth <= 0)
            {
                isDead = true;
                FindObjectOfType<GameManager>().lives--;
                if (facingRight)
                {
                    rb.AddForce(new Vector3(-3, 5, 0), ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                }
            }
        }
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Health Item"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Destroy(other.gameObject);
                anim.SetTrigger("Catching");
                PlaySong(healthItem);
                currentHealth = maxHealth;
                FindObjectOfType<UIManager>().updateHealth(currentHealth);
            }
        }

        if (other.CompareTag("Weapon"))
        {
            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetTrigger("Catching");
                holdingWeapon = true;
                WeaponItem weaponItem = other.GetComponent<PickableWeapon>().weapon;
                weapon.ActivateWeapon(weaponItem.sprite, weaponItem.color, weaponItem.durability, weaponItem.damage);
                Destroy(other.gameObject);
            }
        }
    }

    void PlayerRespawn()
    {
        if(FindObjectOfType<GameManager>().lives > 0)
        {
            FindObjectOfType<UIManager>().UpdateLives();
            isDead = false;
            currentHealth = maxHealth;
            FindObjectOfType<UIManager>().updateHealth(currentHealth);
            anim.Rebind();
            float midWidth = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10)).x;
            transform.position = new Vector3(midWidth, 10, -4);
        }
        else
        {
            FindObjectOfType<UIManager>().UpdateDisplayMessage("Game Over!");
            // Destroy(FindObjectOfType<UIManager>().gameObject);
            Invoke("DestroyUI", 2f);
            Invoke("LoadScene", 5f);
        }

    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }


    void DestroyUI()
    {
        Destroy(FindObjectOfType<UIManager>().gameObject);
    }

    public void SetHoldingWeaponToFalse()
    {
        holdingWeapon = false;
    }
}
