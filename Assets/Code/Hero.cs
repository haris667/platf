using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroStatus 
{
    idle,
    move,
    jump,
    attack, 
    attackDown
}
public class Hero : MonoBehaviour
{
    [Header("Задается в инспекторе:")]
    [SerializeField] protected int          health;
    [SerializeField] protected int          stamina;
    [SerializeField] protected float        speed;
    [SerializeField] protected int          jumpPower;
    [SerializeField] protected int          speedAttack;
    [SerializeField] protected Ability      ability;

    [Header("Задается динамически:")]
    [SerializeField] private Animator       anim;
    [SerializeField] protected HeroStatus   status;
    [SerializeField] protected bool         isGrounded = false;
    [SerializeField] protected Rigidbody2D  rb;
    public void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        status = HeroStatus.idle;
    }
    public void FixedUpdate()
    {
        Move();
        ChangeAttackStatus();
        AnimTick();
        AbilityAttack();
    }

    public void AbilityAttack()
    {
        if(Input.GetButton("Ability1"))
        { 
            Debug.Log("123");
            ability.ActivateRaphaelAbility();
        }
    }
    virtual public void Attack() {}
    virtual public void Move() {}
    public void FlipX(float AngleRotate)
    {  
        transform.rotation = Quaternion.Euler(0, AngleRotate, 0);
    }
    public void Jump()
    {
        status = HeroStatus.jump;
        if (isGrounded) rb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D (Collision2D collision) 
    {
        if (collision.gameObject.tag == "Ground") isGrounded = true;
    }
    private void OnCollisionExit2D (Collision2D collision) 
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;
    }
     public void AnimTick()
    {
        if(status == HeroStatus.move) anim.SetBool("move", true);
        else anim.SetBool("move", false);

        if(status == HeroStatus.jump) anim.SetBool("jump", true);
        else anim.SetBool("jump", false);
  
        if(status == HeroStatus.attack) anim.SetBool("attack", true);
        else anim.SetBool("attack", false);
        if(status == HeroStatus.attackDown) anim.SetBool("attackDown", true);
        else anim.SetBool("attackDown", false);
        anim.speed = speed / 10;
    }
    public Vector3 pos 
    {
        get { return ( this.transform.position); }
        set { this.transform.position = value; }
    }
    public void ChangeAttackStatus() 
    { 
        if(Input.GetButton("Vertical")  && !Input.GetButton("Horizontal")) status = HeroStatus.attack; 
        if(Input.GetButton("FireDown")  && !Input.GetButton("Horizontal")) status = HeroStatus.attackDown;
    }
}
