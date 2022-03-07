using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boat")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
