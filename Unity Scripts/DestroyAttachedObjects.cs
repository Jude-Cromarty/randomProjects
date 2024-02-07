using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAttachedObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return; // do not destroy player
        }
          if (collision.CompareTag("DoNotDestroy"))
        {
            return; // do not destroy player
        }
        Destroy(collision.gameObject);
    }
}
