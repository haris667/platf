using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;
    private void Start() 
    {
        bndCheck = GetComponent<BoundsCheck>();
    }
    private void Update() 
    {
        if (bndCheck.offRight || bndCheck.offLeft)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "Enemy") Destroy(other.gameObject);
    }
}
