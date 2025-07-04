using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHitBox : MonoBehaviour
{
    private Collider hitbox;

    public string targetTag = "Enemy";
    public int damage = 10;

    private void Awake()
    {
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false;
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            var enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxAttack);
            }

            var player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.sfxHit);
                Debug.Log("Punch");
            }
        }
    }
}
