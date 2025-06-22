using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public Joystick joystick;
    
    public Animator animator;

    public PunchHitBox punchHitbox;
    public void EnablePunchHitbox() => punchHitbox.EnableHitbox();
    public void DisablePunchHitbox() => punchHitbox.DisableHitbox();

    private void Update()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;
        Vector3 dir = new Vector3(h, 0, v);

        if(dir.magnitude > 0.01f)
        {
            transform.forward = dir;

            transform.position += dir.normalized * speed * Time.deltaTime;

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
