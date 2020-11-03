using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 v;
    float g;
    bool grounded;
    float movespeed;

    int jumps, maxjumps;

    bool jump0, jump1, jump2, jump3, dash;
    GameObject jump0i, jump1i, jump2i, jump3i, dashi;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        v = Vector2.zero;
        body.velocity = v;
        g = -0.1f;
        grounded = false;
        movespeed = 7;

        jumps = 1;
        maxjumps = 1;

        //setup the booleans and ui elements
        jump0 = jump1 = jump2 = jump3 = dash = false;
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

        if (jumps > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            v.y = 5;
        }

        body.velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "StaticSolid")
        {
            grounded = true;
        }
        jumps = maxjumps;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "StaticSolid") {
            grounded = false;
        }
    }
}
