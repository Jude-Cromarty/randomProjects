using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public Transform FirePoint;
public GameObject bulletPrefab;

private float shootTimer;

public float shootDelay = 0.2f;

public int range;


public float bulletForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FindClosestEnemy() == null){return;}
        Vector3 targ = FindClosestEnemy().transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            shootTimer  += Time.deltaTime;
if (shootTimer > shootDelay){ 
    if ( Vector3.Distance( FindClosestEnemy().transform.position, transform.position ) < range)
     {shootTimer = 0;
            Shoot();}
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
    void Shoot()
{
    GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    rb.AddForce(FirePoint.right * bulletForce, ForceMode2D.Impulse);
}

    
}
