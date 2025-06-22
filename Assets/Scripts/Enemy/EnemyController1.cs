using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public PunchHitBox punchHitbox;


    public int maxHealth = 50;
    public float moveSpeed = 1.5f;
    public float attackRange = 1f;

    int currentHealth;
    bool isDead = false;
    public void EnablePunchHitbox() => punchHitbox.EnableHitbox();
    public void DisablePunchHitbox() => punchHitbox.DisableHitbox();
    private void Start()
    {
        currentHealth = maxHealth;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    private void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveToPlayer();
        }
        else
        {
            Attack();
        }
    }

    void MoveToPlayer()
    {
        animator.SetBool("isMoving", true);
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward = dir;
    }

    void Attack()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        if (currentHealth <= 0) Die();
        Debug.Log("Got Hit");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        StartCoroutine(DieAndShowVictory());
    }

    private IEnumerator DieAndShowVictory()
    {
        yield return new WaitForSeconds(3f); // th?i l??ng animation Die
        FindObjectOfType<LevelManager>()?.EnemyDefeated();
    }
}
