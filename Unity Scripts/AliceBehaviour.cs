using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBehaviour : MonoBehaviour
{
    public float offset;
    public float threshold = 0.0001f;
    public float speed;

    private GameObject player;
    private Vector3 direction;
    private Vector3 targetPosition;

    public int range = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public void SetTarget(GameObject newTarget)
    {

        // adding the offset in that direction
        targetPosition = player.transform.position - direction * offset; //<- undershoot
        //targetPosition = player.transform.position + direction * offset; // <- overshoot

        // direction from the player to the player
        direction = (player.transform.position - transform.position).normalized;

    }

    private void Update()
    {

        // make Player overshoot the player by offset
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, (speed * Time.deltaTime));

        if (FindClosestEnemy() == null) { SetTarget(player = GameObject.FindGameObjectWithTag("Player")); }
        Vector3 targ = FindClosestEnemy().transform.position;

        if(player != null){gameObject.transform.GetChild(0).gameObject.SetActive(false);}

        if (Vector3.Distance(FindClosestEnemy().transform.position, transform.position) < range)
        {
            player = null;
           gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}