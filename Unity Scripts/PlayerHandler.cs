using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    
    public float playerHealth = 10f;

    public int playerEXP;

    private int levelEXP;

    private Slider EXPslider;
    private Slider HealthSlider;
    private GameObject expBar;
    private GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerEXP = 0;
        levelEXP = 5;
        playerHealth = 10;
//expbar logic
        expBar = GameObject.Find("EXPbar");
        EXPslider = expBar.GetComponent<Slider>();
        EXPslider.maxValue = levelEXP;
//healthbar logic
        healthBar = GameObject.Find("HealthBar");
        HealthSlider = healthBar.GetComponent<Slider>();
        HealthSlider.maxValue = playerHealth;
        HealthSlider.value = playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if(playerEXP >= levelEXP)
        {
            Debug.Log("Levelup!");
            playerEXP = 0;
            levelEXP = levelEXP + 1;
            EXPslider.value = 0;
            EXPslider.maxValue = levelEXP;
        }
                //Game Over
        if(playerHealth <= 0){Destroy(gameObject); Debug.Log("You Lose!");}
    }

     void OnTriggerEnter2D(Collider2D other) 
    { //requires rigidbody
        if(other.gameObject.tag == "EXP")
        {
            Destroy(other.gameObject);
            playerEXP++;
            EXPslider.value = playerEXP;
        }
    }

    public void Damage(int damageAmount)
    {
        playerHealth = playerHealth - damageAmount;
        HealthSlider.value = playerHealth;
    }



}


