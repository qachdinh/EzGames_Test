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

    [SerializeField] float maxX;
    [SerializeField] float maxZ;

    private void Update()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;
        Vector3 dir = new Vector3(h, 0, v);

        if(dir.magnitude > 0.01f)
        {
            transform.forward = dir;

            var currentPosition = transform.position + dir.normalized * speed * Time.deltaTime;
            if (currentPosition.z > maxZ || currentPosition.z < -maxZ || currentPosition.x >maxX || currentPosition.x<-maxX) return;
            transform.position += dir.normalized * speed * Time.deltaTime;

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
