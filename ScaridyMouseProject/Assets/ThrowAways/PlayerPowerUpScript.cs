using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUpScript : MonoBehaviour
{
    //Referenser
    private Rigidbody2D rb;

    //Jump variables
    private float jumpTime;
    [SerializeField]
    [Range(0, 100)]
    private float jumpForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate ()
    {
        if (Input.GetKeyDown("space"))
        {
            Vector3 jumpDirection = transform.forward;
            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);

        }
    }
}
