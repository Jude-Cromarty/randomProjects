using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    [SerializeField] public Transform Prefab;
    [SerializeField] public Transform player;
    int coins;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
        
        {Instantiate(Prefab, player.transform.forward, Quaternion.identity);}
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            coins = coins + 1;
            Debug.Log("You have " + coins + " Coin(s)");
            Destroy(this.gameObject);
        }
    }
}
