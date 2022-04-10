using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boris : Hero
{
    static public Boris B;
   
    void Start()
    {
        if (B == null) B = this;
    }

    public override void Move()
    {
        
       

        if (Input.GetButton("HorizontalPlayer2") && status != HeroStatus.attack)
        {
            if (status != HeroStatus.jump || isGrounded) status = HeroStatus.move;

            Vector3 tempPos = pos;
            tempPos.x += speed * Input.GetAxis("HorizontalPlayer2") * Time.deltaTime;
            if (tempPos.x < pos.x) FlipX(180f);
            else FlipX(0);

            pos = tempPos;
        }
        if (Input.GetButtonDown("JumpPlayer2")) Jump();
        if (isGrounded && !Input.GetButton("HorizontalPlayer2")) status = HeroStatus.idle;

        base.Move();
    }
}
