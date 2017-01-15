using UnityEngine;
using System.Collections;
using System;

/*
Şu anda kullanımda değil, boundaries'in collision tepkilerini içerir. 
*/
public class boundCollisionController : MonoBehaviour {

    public pyhsicsOfItems poi;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "pufCharacter")
        {
 
        }
        if (coll.gameObject.tag == "ball")
        {
 
        }
    }
}
