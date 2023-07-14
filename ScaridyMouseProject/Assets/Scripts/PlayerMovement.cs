using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public bool isMovingLeft = false;
    public bool isMovingRight = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //S�tt kontrollera bools s� att man bara kan g� ett ett h�ll samtidigt och 
        if (Input.GetKey("a"))
        {
            isMovingRight = false;
            isMovingLeft = true;
        }
        if (Input.GetKeyUp("a"))
        {
            isMovingLeft = false;
        }

        if (Input.GetKeyDown("d"))
        {
            isMovingLeft = false;
            isMovingRight = true;
        }
        if (Input.GetKeyUp("d"))
        {
            isMovingRight = false;
        }
    }

    void FixedUpdate () 
    {
        if (!isMovingLeft && !isMovingRight) 
        {
            rb.velocity = new Vector2(0, 0);
            return; 
        }
        //Beteende om man g�r v�nster
        if (isMovingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }

        //Beteende man g�r h�ger
        if (isMovingRight)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }
    }
}
