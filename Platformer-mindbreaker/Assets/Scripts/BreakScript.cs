using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreakScript : MonoBehaviour
{
    public AudioSource DoorCrashSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boat")
        {
            DoorCrashSound.Play();
            gameObject.SetActive(false);
            Destroy(collision.gameObject);

            Destroy(gameObject,1);

        }
    }
}
