using UnityEngine;
using UnityEngine.UI;
public class DroppedObjectHandler : MonoBehaviour
{
    private  Slider healthslider;
    private  GameObject healthbar;

   public bool isFlipped;

    private  int damage;
    public  int Defense;

    public  int attack;

    //damage = att * att / (att + def) <--Defense Calculation-->

    private void Start()
    {
        attack = 10;
        // Find the "HealthBar" child within the current object's hierarchy
        Transform healthBarTransform = transform.Find("HealthBar");

        // Check if the child "HealthBar" is found
        if (healthBarTransform != null)
        {
            // Access the healthbar GameObject
            healthbar = healthBarTransform.gameObject;

            // Get the Slider component from the healthbar GameObject
            healthslider = healthbar.GetComponent<Slider>();

            if (healthslider != null)
            {
                // Set the initial value for the health slider
                healthslider.value = 200;
            }
            else
            {
                Debug.LogError("Slider component not found on the HealthBar GameObject.");
            }
        }
        else
        {
            Debug.LogError("HealthBar not found as a child of the current GameObject.");
        }
        
    }

        
    public void HandleDroppedObject(string cardName) 
    {
         string actualCardName = cardName.Replace("Card", "").Replace("(Clone)", "");
        switch (actualCardName)
        {
            case "Fool":
                break;
            case "Lovers":
                Debug.Log("Activated Lovers");
                break;
            case "Emperor":
            attack = attack + 5;
                healthslider.value = healthslider.value - (attack * attack / (attack + Defense));
                break;
            case "HighPriestess":
                healthslider.value = healthslider.value + Mathf.Clamp((healthslider.value*100)/25,healthslider.minValue,healthslider.maxValue); 
                break;
            case "Hierophant":
                healthslider.value = healthslider.value - (attack * attack / (attack + Defense));
                break;
            case "Magician":
               healthslider.value = healthslider.value + Mathf.Clamp(healthslider.value, healthslider.minValue,healthslider.maxValue);
                break;
            case "Empress":
                Defense = Defense + 5;
                break;
            case "Chariot":
                Defense = Defense - 5;
                break;
            case "Strength":
                Defense = Defense + 10;
                attack = attack + 10;
                break;
            case "Hermit":
                break;
            case "WheelofFortune":
                break;
            case "Justice":
                break;
                 case "HangedMan":
                break;
                 case "Death":
                break;
                 case "Temperance":
                break;
                 case "Devil":
                break;
                 case "Tower":
                break;
                 case "Star":
                break;
                 case "Moon":
                 healthslider.value = healthslider.value - Mathf.Clamp((healthslider.value - 50),healthslider.minValue,healthslider.maxValue);
                break;
                 case "Sun":
                 healthslider.value = healthslider.value + Mathf.Clamp((healthslider.value+50),healthslider.minValue,healthslider.maxValue);
                break;
                 case "Judgement":
                break;
                 case "World":
                break;

            

            default:
                Debug.Log("Activated an unknown card");
                break;
        }
    }
}

//Cards as of 22/11/23//
/*The Fool - Does Nothing  // 
The Magician - Doubles health//
The High Priestess - Heals Selected Player 25% //
The Emperor -  Increases & Deals Damage  //
The Hierophant - Deals Damage //
The Lovers - Shares Your Stats to Selected Player 
The Empress - Increases Defense
The Chariot - Increases own Defense and damages others
Strength - Increased damage and defense 
The Hermit - Current hand repeats
the Wheel of Fortune - Picks between 8 diffrent options
Justice - Sets health the same as used players
the Hanged Man - Removes random card from hand
Death - Wipe Current Deck
Temperance - Sets teams heath as average of total
The Devil - Random cards in deck are now fools 
The Tower - Wipe current hand
The Star - Get back last played card
The Moon - Massively Decrease Health
The Sun - Massively increase Health
Judgement - Random effect from card in your deck
The World - Random effect from any card in game*/