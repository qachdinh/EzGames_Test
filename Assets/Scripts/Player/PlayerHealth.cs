using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public Animator animator;
    public Slider slider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        if(slider != null)
        {
            slider.maxValue = maxHealth;
            slider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (slider != null)
        {
            slider.value = currentHealth;
        }
        animator.SetTrigger("Hit");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
    }
}
