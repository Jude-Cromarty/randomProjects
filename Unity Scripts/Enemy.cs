using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    
    Vector2 destination;

    public float speed;
    int health = 5;

    public GameObject Parts,Gold;

    // Start is called before the first frame update
    void Start()
    {
          player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        destination = player.transform.position;

       if(health <= 0)
       {

        int Item = Random.Range(0, 10);
        switch (Item)
        {
        case 1: case 2: case 3: case 4: case 5:
         Instantiate(Parts, transform.position, Quaternion.identity);
        Destroy(gameObject);
        break;
        case 6: case 7: case 8:
         Instantiate(Gold, transform.position, Quaternion.identity);
         break;
        default:
         Destroy(gameObject);
        break;   
        }
        }

        transform.position = Vector2.MoveTowards(transform.position, destination, speed);


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Bullet")
         {
            health --;
         }

    }
}
