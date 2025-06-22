using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
