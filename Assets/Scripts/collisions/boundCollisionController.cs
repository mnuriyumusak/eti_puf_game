using UnityEngine;
using System.Collections;
using System;

/*
Şu anda kullanımda değil, boundaries'in collision tepkilerini içerir. 
*/
public class boundCollisionController : MonoBehaviour {

    public MyCharacterController characterController;
    public bool isRightLeft;

    void Start()
    {
        characterController = GameObject.FindGameObjectWithTag("myin").GetComponent<myInput>().character1Controller;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (characterController != null)
        {
            if (coll.gameObject.tag == "pufCharacter1" && characterController.poi != null)
            {
                if (characterController.poi.character.GetInstanceID() == coll.gameObject.GetInstanceID())
                {

                }
            }
        }
 
        if (coll.gameObject.tag == "ball")
        {
 
        }
    }

 
}
