using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    GameObject p;
    Vector3 track;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player");
        track = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        track.y = transform.position.y;
        track.z = transform.position.z;
        track.x = p.transform.position.x;
        transform.position = track;
    }
}
