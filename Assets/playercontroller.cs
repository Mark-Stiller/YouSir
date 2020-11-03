using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 v;
    float g;
    bool grounded;
    float movespeed;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        v = Vector2.zero;
        body.velocity = v;
        g = -0.1f;
        grounded = false;
        movespeed = 7;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = h * movespeed;
        }
        else v.x = 0;
        if(!grounded)
        {
            v.y += g;
        }
        body.velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }
}
