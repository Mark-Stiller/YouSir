using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    int keys;
    GameObject keyi;

    Text finish;

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
        keyi = GameObject.Find("uikey");

        facingright = true;
        facingleft = false;

        finish = GameObject.Find("finish").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = h * movespeed;
            if (v.x > 0)
            {
                facingleft = false;
            }
            else if(v.x < 0)
            {
                facingleft = true;
            }
        }
        else v.x = 0;
        if(!grounded)
        {
            g = -0.1f;
            v.y += g;
        }
        else
        {
            g = 0;
        }
        //set facing direction


        if (jumps > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            v.y = 4.5f;
            jumps--;
        }

        //moderate UI display
        //key
        if (keys < 1)
        {
            keyi.SetActive(false);
        }
        else keyi.SetActive(true);
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
        if (climbable && Input.GetKey(KeyCode.UpArrow))
        {
            g = 0;
            v.y = 5;
        }
        else g = -0.1f;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }


        if (v.y < -10)
        {
            v.y = 0;
            grounded = true;
            if (candash) dash = true;
            jumps = maxjumps;
        }

        body.velocity = v;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "StaticSolid" || collision.gameObject.name == "StaticPlatform" || collision.gameObject.name == "plate")
        {
            if (v.y <= 0)
            {
                grounded = true;
                if (candash) dash = true;
                jumps = maxjumps;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "StaticSolid" || collision.gameObject.name == "StaticPlatform")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "plate")
        {
            Destroy(GameObject.Find("gate_plate"));
        }
        if (collision.gameObject.name == "adddash")
        {
            Destroy(collision.gameObject);
            candash = true;
        }
        if (collision.gameObject.name == "addjump")
        {
            Destroy(collision.gameObject);
            maxjumps++;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StaticClimbing")
        {
            climbable = true;
        }
        if (collision.gameObject.name == "key" && Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(collision.gameObject);
            keys++;
        }
        if (collision.gameObject.name == "gate_key" && keys > 0 && Input.GetKeyDown(KeyCode.X))
        {
            Destroy(collision.gameObject);
            keys--;
        }

        //level clear
        if (collision.gameObject.name == "finishdoor")
        {
            finish.GetComponent<Text>().text = "Congratulations!!!" +
                "\nPress <R> to reset the level";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "StaticClimbing")
        {
            climbable = false;
        }
    }

    public void Dash()
    {
        if (candash && dash)
        {
            if (facingleft)
            {
                v.x = -15;
                v.y = 2;
            }
            else
            {
                v.x = 15;
                v.y = 2;
            }
            dash = false;
        }
    }
}
