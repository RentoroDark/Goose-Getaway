using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController player;
    [SerializeField] PlayerData playerData;
    [SerializeField] Vector2 moveInput;
    [SerializeField] float gravity;
    [SerializeField] Vector3 currentMovement;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask layers;
    [SerializeField] Animator animator;
    [SerializeField] GameObject gus;
    [SerializeField] ParticleSystem footsteps;
    [SerializeField] float acceleration = 0;
    private float speed = 4;


    private float jumpBufferMaxTime = 0.1f;
    [SerializeField]private float jumpBufferCurTime;


    void Update()
    {
        HandleJump();
        HandleMovement();
        HandleGravity();
        jumpBufferCurTime -= Time.deltaTime;
        speed += acceleration * Time.deltaTime;
    }


    private void HandleJump()
    {
        if(jumpBufferCurTime > 0 && IsGrounded())
        {
            animator.SetBool("HitTheGround", false);
            animator.SetBool("Jumping", true);
            jumpBufferCurTime = 0;
            currentMovement.y = 5;
        }
        else if (jumpBufferCurTime > 0 && !IsGrounded())
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("FastFalling", true);
            jumpBufferCurTime = 0;
            currentMovement.y = -gravity;
        }
    }
    private void HandleMovement()
    {
        player.Move((currentMovement + new Vector3(0, 0, speed)) * Time.deltaTime);
        transform.localPosition = new Vector3(Math.Clamp(transform.localPosition.x, -1.2f, 1.2f), transform.localPosition.y, transform.localPosition.z); 
    }

    private void HandleGravity()
    {

        if (! player.isGrounded)
        {
            //footsteps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            currentMovement.y -= gravity * Time.deltaTime ;
        }
        if (player.isGrounded)
        {
            //footsteps.Play();
            animator.SetBool("Jumping", false);
            animator.SetBool("FastFalling", false);
            animator.SetBool("HitTheGround", true);
            currentMovement.y = -1f;
        }
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        currentMovement.x = context.ReadValue<float>() * 0.5f * playerData.sensetivity;
        gus.transform.rotation = Quaternion.Euler(0, currentMovement.x * 2, 0);
    }
    public void JumpInput(InputAction.CallbackContext context)
    {
        
        if(context.started)
        {
            jumpBufferCurTime = jumpBufferMaxTime;
        }
    }
    private bool IsGrounded()
    {
        return Physics.CheckBox(groundCheck.position,new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity, layers);
    }

    public void Boost(float speed) //сделать анимацию
    {
        StartCoroutine("BoostCorutine", speed);
    }
    IEnumerator BoostCorutine(float speed)
    {
        currentMovement.z = speed;
        while (transform.localPosition.z <= 0.0f)
        {
            yield return 0;
        }
        currentMovement.z = 0;
    }
}
