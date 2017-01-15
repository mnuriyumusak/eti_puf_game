using UnityEngine;
using System.Collections;
using System;

/*
kalelerin collision tepkilerini içerir. 
*/
public class kaleCollisionController : MonoBehaviour
{

    public pyhsicsOfItems poi;
    public MyCharacterController characterController; //canJump, sağa gidiyor mu vs gibi bilgiler gerekiyor ondan dolayı gerekli .
    public Transform kale;

    void OnCollisionEnter2D(Collision2D coll)
    {
        //eğer puf kalenin üstündeyse zıplayabilmeli yerden yüksekte olmasına rağmen onu ayarlayan br kod. 
        if (coll.gameObject.tag == "pufCharacter1" || coll.gameObject.tag == "pufCharacter2")
        {
            if (Math.Abs(kale.position.y - poi.charactery) >= 1.90f)
            {
                GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().canJump = true;
                GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().startGravity = false;
            }
        }
        if (coll.gameObject.tag == "ball")
        {

        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "pufCharacter1" || coll.gameObject.tag == "pufCharacter2")
        {
            GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().canJump = false;
        }
    }

    public void KnowMe()
    {
        if(Network.isServer)
            characterController = GameObject.FindGameObjectWithTag("pufCharacter1").GetComponent<MyCharacterController>();
        else
            characterController = GameObject.FindGameObjectWithTag("pufCharacter2").GetComponent<MyCharacterController>();

    }
}