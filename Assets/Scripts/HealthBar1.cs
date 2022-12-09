using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar1 : MonoBehaviour
{
    public Slider HealthBar;


    public void  SetMaxHealth(int Health)
    {
        HealthBar.maxValue = Health;
        HealthBar.value = Health;   
    }
    public void SetHealth(int Health)
    {
        HealthBar.value = Health;
    }

}
