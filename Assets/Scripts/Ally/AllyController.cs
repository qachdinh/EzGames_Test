using System.Collections;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    public Animator animator;
    public PunchHitBox punchHitbox;

    public float moveSpeed = 1.5f;
    public float attackRange = 0.5f;
    public int maxHealth = 50;

    private int currentHealth;
    private bool isDead = false;

    private Transform targetEnemy;
    public void EnablePunchHitbox() => punchHitbox.EnableHitbox();
    public void DisablePunchHitbox() => punchHitbox.DisableHitbox();

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        if (targetEnemy == null || targetEnemy.gameObject == null)
        {
            FindClosestEnemy();
            return;
        }

        float distance = Vector3.Distance(transform.position, targetEnemy.position);

        if (distance > attackRange)
        {
            MoveToTarget();
        }
        else
        {
            Attack();
        }
    }

    void MoveToTarget()
    {
        animator.SetBool("isMoving", true);
        Vector3 dir = (targetEnemy.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.forward = dir;
    }

    void Attack()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Attack");
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float minDistance = Mathf.Infinity;
        Transform closest = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy.transform;
            }
        }

        targetEnemy = closest;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hurt);

        StartCoroutine(DieAndNotifyGameManager());
    }

    private IEnumerator DieAndNotifyGameManager()
    {
        yield return new WaitForSeconds(3f);

        var gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.AllyDied();
        }

        Destroy(gameObject);
    }
}
