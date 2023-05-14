using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : MonoBehaviour
{
    
    public GameObject knight;
    private Vector2 direction;
    private bool knightToRight;
    private bool facingRight;
    void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        direction = knight.transform.position - gameObject.transform.position;
        if (direction.x > 0)
        {
            knightToRight = true;
            if(facingRight == false)
            {
                Flip();
            }
        }
        else
        {
            knightToRight = false;
            if (facingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
