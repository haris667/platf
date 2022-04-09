using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raphael : Hero
{
    static public Raphael S;
    [Header("Класс Raphael, задается в инспекторе:")]
    [SerializeField] private GameObject   projectilePref;
    [SerializeField] private Transform    Gun;
    [SerializeField] private float        speedProjectile;
    [SerializeField] private float        speedUp;
    [SerializeField] private float        delaySpeedUp;
    [Header("Задается динамически:")]
    private float                         timerAttack;
    private float                         timerSpeedUp;
    private float                         shiftSpeed;                                      
    private void Start() 
    {
        if (S == null) S = this;
        else Debug.LogError("Raphael Start - повторное создание эксземпляра");

        timerAttack = Time.time;
        timerSpeedUp = Time.time;
        shiftSpeed = speed;
        
        ability = GetComponent<Ability>();
        ability.abilityRaphaelType = AbilityRaphaelType.spreadShot;
    }
    public override void Attack()
    {
        if(status == HeroStatus.attack && Time.time - timerAttack >= speedAttack) 
        {
            Projectile p = MakeProjectile();
            timerAttack = Time.time;
        } 
        else if(status == HeroStatus.attackDown && Time.time - timerAttack >= speedAttack)
        {
            Projectile p = MakeProjectile(-45);
            timerAttack = Time.time;
        }
        Debug.Log(Time.time - timerAttack);
    }
    public override void Move()
    {   
        if(Input.GetButton("SpeedUp") && Time.time - timerSpeedUp >= delaySpeedUp)
        {
            speed += speedUp;
            timerSpeedUp = Time.time;
        }
        if (Time.time - timerSpeedUp >= delaySpeedUp / 2) speed = shiftSpeed;

        if (Input.GetButton("Horizontal") && status != HeroStatus.attack)
        {
            if(status != HeroStatus.jump || isGrounded) status = HeroStatus.move;

            Vector3 tempPos = pos;
            tempPos.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            if(tempPos.x < pos.x) FlipX(180f);
            else FlipX(0);

            pos = tempPos; 
        }
        if (Input.GetButton("Jump")) Jump();
        if (isGrounded && !Input.GetButton("Horizontal")) status = HeroStatus.idle;

        base.Move();
    }
    public Projectile MakeProjectile() 
    {
        GameObject projGO = Instantiate(projectilePref, Gun.position, transform.rotation);
        if(transform.rotation.y == 0) 
            projGO.GetComponent<Rigidbody2D>().velocity = Vector3.right * speedProjectile;
        else projGO.GetComponent<Rigidbody2D>().velocity = Vector3.left * speedProjectile;

        Projectile proj = projGO.GetComponent<Projectile>();
        return proj;
    }
    public Projectile MakeProjectile(float angles) 
    {
        GameObject projGO = Instantiate(projectilePref, Gun.position, transform.rotation);
        if(transform.rotation.y == 0) 
        {
            projGO.transform.rotation = Quaternion.Euler(0, 0, angles); 
            projGO.GetComponent<Rigidbody2D>().velocity = projGO.transform.rotation * Vector3.right * speedProjectile;
        }
        else 
        {
            projGO.transform.rotation = Quaternion.Euler(0, 0, -angles); 
            projGO.GetComponent<SpriteRenderer>().flipX = true;
            projGO.GetComponent<Rigidbody2D>().velocity = projGO.transform.rotation * Vector3.left * speedProjectile;
        }

        Projectile proj = projGO.GetComponent<Projectile>();
        return proj;
    }
}
