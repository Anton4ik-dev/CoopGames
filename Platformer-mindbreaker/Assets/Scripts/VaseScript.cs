using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseScript : MonoBehaviour
{
    public AudioSource CrashVaseSound;

    private bool isFalling = false;
    private bool alreadyTriggered = false;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private GameObject keys;
    [SerializeField] private Transform spawnPoint;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(rb.velocity.y != 0)
        {
            isFalling = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && isFalling == true && alreadyTriggered == false)
        {
            alreadyTriggered = true;
            Instantiate(keys, spawnPoint.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            anim.SetBool("Dead", true);
            Destroy(gameObject, 0.65f);
            CrashVaseSound.Play();
        }
    }
}
