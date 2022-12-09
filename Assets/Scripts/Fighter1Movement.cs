using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fighter1Movement : MonoBehaviour
{
    private float HorizontalMove;
    private float Speed = 8f; 
    private float JumpForce = 16f;
    private bool IsFacingRight = true;

    public Animator Fighter1_Animator;

   

    [SerializeField] private Rigidbody2D Fighter1Rb;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;

    public Transform AttackCheck;
    public float AttackRange = 0.5f;
    public int AttackDamage = 15;
    public LayerMask EnemyLayer;

    public float AttackRate = 2f;
    float NextTimeToAttack = 0f;
    void Update()
    {

        if(Time.time >= NextTimeToAttack)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                NextTimeToAttack = Time.time + 1f / AttackRate;
            }
        }





        Fighter1_Animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));

        HorizontalMove = Input.GetAxisRaw("Horizontal1");

        if (Input.GetButton("Jump1"))
        {
            Fighter1_Animator.SetBool("IsJumping", true);
        }
        if (IsGrounded())
        {
            Fighter1_Animator.SetBool("IsJumping", false);
        }

        


        if (Input.GetButtonDown("Jump1") && IsGrounded())
        {
            Fighter1Rb.velocity = new Vector2(Fighter1Rb.velocity.x, JumpForce);  
        }

        if (Input.GetButtonUp("Jump1") && Fighter1Rb.velocity.y > 0f)
        {
            Fighter1Rb.velocity = new Vector2(Fighter1Rb.velocity.x, Fighter1Rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        Fighter1Rb.velocity = new Vector2(HorizontalMove * Speed, Fighter1Rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);
    }

    private void Flip()
    {
        if (IsFacingRight && HorizontalMove < 0f || !IsFacingRight && HorizontalMove > 0f)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    void Attack()
    {
        Fighter1_Animator.SetTrigger("IsAttacking");

        Collider2D[] HitEnenies = Physics2D.OverlapCircleAll(AttackCheck.position, AttackRange, EnemyLayer);


        foreach(Collider2D Enemy in HitEnenies)
        {
            Enemy.GetComponent<Fighter2Health>().TakeDamage(AttackDamage);
        }

    }
     void OnDrawGizmosSelected()
    {
        if (AttackCheck == null)
            return;

        Gizmos.DrawWireSphere(AttackCheck.position, AttackRange);   
    }
}
