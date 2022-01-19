using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Bullet(Clone)" || collision.tag == "Wall" || collision.tag == "Wolf")
        {
            Destroy(gameObject);
        }
    }
}
