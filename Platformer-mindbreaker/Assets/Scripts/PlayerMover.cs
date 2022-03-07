using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{

    public float Speed, JumpForce;
    public LayerMask groundLayer;
    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject boat;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;


    private SpriteRenderer sprite;
    private bool grounded;
    private Rigidbody2D rb;
    private Transform groundChecker;
    private Animator anim;
    private bool _playerGotKey;
    private bool cageDestoyed = false;





    void Start()
    {
        groundChecker = transform.GetChild(1);
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        Vector2 newVelo = transform.right * moveX * Speed;
        newVelo.y = rb.velocity.y;
        rb.velocity = newVelo;
        grounded = Physics2D.OverlapCircle(groundChecker.position, 0.05f, groundLayer);
        anim.SetBool("Grounded", grounded);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
        }

        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VSpeed", rb.velocity.y);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Boat")
        {
            collision.rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Lever") && Input.GetKeyDown(KeyCode.E) && cageDestoyed == true)
        {
            trap.SetActive(false);
            Debug.Log("Working");
        }
        if (collision.CompareTag("LeverBoat") && Input.GetKeyDown(KeyCode.E))
        {
            boat.SetActive(true);
            Debug.Log("Working");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            _playerGotKey = true;
        }
        if (other.CompareTag("Cage") && _playerGotKey == true)
        {
            Destroy(other.gameObject);
            cageDestoyed = true;
            _playerGotKey = false;
        }

        if (other.gameObject.tag == "Death")
        {
            pause.SetActive(false);
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (other.CompareTag("Finish"))
        {
            pause.SetActive(false);
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        
        
    }
}