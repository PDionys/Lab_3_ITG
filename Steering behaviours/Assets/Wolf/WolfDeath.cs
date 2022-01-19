using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDeath : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Bullet(Clone)" || collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
