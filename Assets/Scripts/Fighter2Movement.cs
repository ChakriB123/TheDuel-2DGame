using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter2Movement : MonoBehaviour
{
    private float HorizontalMove;
    private float Speed = 8f;
    private float JumpForce = 16f;
    private bool IsFacingRight = false;

    public Animator Fighter2_Animator;

    [SerializeField] private Rigidbody2D Fighter2Rb;
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

        if (Time.time >= NextTimeToAttack)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Attack();
                NextTimeToAttack = Time.time + 1f / AttackRate;
            }
        }





        HorizontalMove = Input.GetAxisRaw("Horizontal2");

        Fighter2_Animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));

        if (Input.GetButton("Jump2"))
        {
            Fighter2_Animator.SetBool("IsJumping", true);
        }
        if (IsGrounded())
        {
            Fighter2_Animator.SetBool("IsJumping", false);
        }

        if (Input.GetButtonDown("Jump2") && IsGrounded())
        {
            Fighter2Rb.velocity = new Vector2(Fighter2Rb.velocity.x, JumpForce);
        }

        if (Input.GetButtonUp("Jump2") && Fighter2Rb.velocity.y > 0f)
        {
            Fighter2Rb.velocity = new Vector2(Fighter2Rb.velocity.x, Fighter2Rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        Fighter2Rb.velocity = new Vector2(HorizontalMove * Speed, Fighter2Rb.velocity.y);
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
        Fighter2_Animator.SetTrigger("IsAttacking");

        Collider2D[] HitEnenies = Physics2D.OverlapCircleAll(AttackCheck.position, AttackRange, EnemyLayer);


        foreach (Collider2D Enemy in HitEnenies)
        {
            Enemy.GetComponent<Fighter1Health>().TakeDamage(AttackDamage);
        }

    }
    void OnDrawGizmosSelected()
    {
        if (AttackCheck == null)
            return;

        Gizmos.DrawWireSphere(AttackCheck.position, AttackRange);
    }
}
