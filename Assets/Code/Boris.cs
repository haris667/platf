using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boris : Hero
{
    
    static public Boris B;

    [Header(" ласс Boris, задаетс€ в инспекторе:")]
    [SerializeField] protected float rollingRange;
    [SerializeField] protected float restartRolling;

    [Header(" ласс Boris,задаетс€ динамически:")]
    [SerializeField] private bool flip;
    [SerializeField] private bool isFlip;
    [SerializeField] private float timeLastRolling;
    


    void Start()
    {
        if (B == null) B = this;
        isFlip = false;
        flip = false;
        timeLastRolling = Time.time;
    }

    public override void Move()
    {
        if (Input.GetButtonDown("Fire1")) Roll();
       

        if (Input.GetButton("HorizontalPlayer2") && status != HeroStatus.attack && !isFlip)
        {
            if (status != HeroStatus.jump || isGrounded) status = HeroStatus.move;

            Vector3 tempPos = pos;
            tempPos.x += speed * Input.GetAxis("HorizontalPlayer2") * Time.deltaTime;
            if (tempPos.x < pos.x)
            {
                FlipX(180f);
                flip = true;
            }
            else
            {
                FlipX(0);
                flip = false;
            }

            pos = tempPos;
        }
        if (Input.GetButton("JumpPlayer2")) Jump();
        if (isGrounded && !Input.GetButton("HorizontalPlayer2")) status = HeroStatus.idle;

        base.Move();
    }

    private void Roll()
    {
        if (isGrounded && Time.time - timeLastRolling > restartRolling)
        {
            Vector3 roll;
            if (flip) roll.x = -rollingRange;
            else roll.x = rollingRange;
            roll.y = 1;
            roll.z = 0;
            rb.AddForce(roll, ForceMode2D.Impulse);
            timeLastRolling = Time.time;
            isFlip = true;
            Invoke("IsFlipFalse", restartRolling); //не знаю как подругому это сделать, сильно не бей
        }
    }
    private void IsFlipFalse()
    {
        isFlip = false;
    }
}
