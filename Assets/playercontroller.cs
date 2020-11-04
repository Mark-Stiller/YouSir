using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontroller : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 v;
    float g;
    bool grounded, climbable;
    float movespeed;

    int jumps, maxjumps;

    bool jump0, jump1, jump2, jump3, dash, candash;
    GameObject jump0i, jump1i, jump2i, jump3i, dashi;
    bool facingleft, facingright;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        v = Vector2.zero;
        body.velocity = v;
        g = -0.1f;
        grounded = climbable = false;
        movespeed = 10;

        jumps = 1;
        maxjumps = 1;

        //setup the booleans and ui elements
        jump0 = jump1 = jump2 = jump3 = candash = climbable = dash = false;
        jump0i = GameObject.Find("jump0");
        jump1i = GameObject.Find("jump1");
        jump2i = GameObject.Find("jump2");
        jump3i = GameObject.Find("jump3");
        dashi = GameObject.Find("dash");

        facingright = true;
        facingleft = false;
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
        //set facing direction


        if (jumps > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            v.y = 5;
            jumps--;
        }

        //moderate UI display
        switch (jumps)
        {
            case 0:
                jump0i.SetActive(true);
                jump1i.SetActive(false);
                jump2i.SetActive(false);
                jump3i.SetActive(false);
                break;
            case 1:
                jump0i.SetActive(false);
                jump1i.SetActive(true);
                jump2i.SetActive(false);
                jump3i.SetActive(false);
                break;
            case 2:
                jump0i.SetActive(false);
                jump1i.SetActive(false);
                jump2i.SetActive(true);
                jump3i.SetActive(false);
                break;
            case 3:
                jump0i.SetActive(false);
                jump1i.SetActive(false);
                jump2i.SetActive(false);
                jump3i.SetActive(true);
                break;
        }
        if (candash)
        {
            dashi.SetActive(true);
        }
        else dashi.SetActive(false);

        //climbing
        if (climbable && Input.GetKeyDown(KeyCode.UpArrow))
        {
            g = 0;
            v.y = 5;
        }
        else g = -0.1f;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }

        body.velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "StaticSolid")
        {
            grounded = true;
            dash = true;
        }
        if (collision.gameObject.name == "StaticClimbing")
        {
            climbable = true;
        }
        jumps = maxjumps;


        if (collision.gameObject.name == "StaticSolid")
        {
            climbable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "StaticSolid") {
            grounded = false;
        }
        if (collision.gameObject.name == "StaticClimbing")
        {
            climbable = false;
        }
    }

    public void Dash()
    {
        if (dash)
        {
            if (facingleft)
            {
                v.x = -10;
            }
            else
            {
                v.x = 10;
            }
        }
    }
}
