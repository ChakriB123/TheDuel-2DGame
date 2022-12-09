using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fighter2Health : MonoBehaviour
{

    public int MaxHealth = 100;
    int CurrentHealth;

    public Animator Fighter2_Animator;
    public HealthBar2 HealthBar;

    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        HealthBar.SetHealth(CurrentHealth);

        Fighter2_Animator.SetTrigger("Hurt");

        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Fighter2_Animator.SetBool("IsDead", true);
        GetComponent<Fighter2Movement>().enabled = false;
       // GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    
    
}
