using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public BehaviourForPet behaviour;//ACCESS BEHAVIOURFORPET SCRIPT

    public Button b_Hunger, b_Fun, b_Energy;//ALLOWS BUTTONS TO BE ASSIGNED VIA INSPECTOR
    // Start is called before the first frame update
    void Start()
    {
        b_Hunger.onClick.AddListener(HungerClicked);//LISTENS FOR BUTTON BEING CLICKED
        b_Fun.onClick.AddListener(FunClicked);
        b_Energy.onClick.AddListener(EnergyClicked);
    }

    void HungerClicked(){Debug.Log("Hunger Clicked"); behaviour.Hunger += 1;}
    void FunClicked(){Debug.Log("Fun Clicked"); behaviour.Fun += 1;}
    void EnergyClicked(){Debug.Log("Energy Clicked");behaviour.Energy += 1;}//OUTPUTS TO CONSOLE
}
