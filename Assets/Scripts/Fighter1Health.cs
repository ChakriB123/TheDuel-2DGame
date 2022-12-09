using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter1Health : MonoBehaviour
{
    public int MaxHealth = 100;
    int CurrentHealth;

    public Animator Fighter1_Animator;
    public HealthBar1 HealthBar;

    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;

        HealthBar.SetHealth(CurrentHealth);

        Fighter1_Animator.SetTrigger("Hurt");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Fighter1_Animator.SetBool("IsDead", true);
        GetComponent<Fighter1Movement>().enabled = false;
        // GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
