using UnityEngine;
using System.Collections;
using System;

/*
kalelerin collision tepkilerini içerir. 
*/
public class kaleCollisionController : MonoBehaviour
{
    public Transform kale;
 

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "pufCharacter1" && coll.gameObject.GetComponent<MyCharacterController>().poi != null)
        {
            MyCharacterController tmp = coll.gameObject.GetComponent<MyCharacterController>();
                if (tmp.poi.charactery>= -0.36f)
                {
                    tmp.characterIsOnKale = true;
                    tmp.changeJumpHeight();
                    tmp.canJump = true;
                    tmp.startGravity = false;
                }
            //kale içinden üs direğe çarptıysa rdan geri yere düşmesi için yoksa yukarı doğru çıkmak
            //istiyor hep ve orda takılı kalıyor
            if (tmp.poi.charactery <= -0.8f && tmp.poi.charactery > -1.3f)
                {
                    tmp.startGravity = true;
                    tmp.jumpActiavted = false;
                }
        }

        if (coll.gameObject.tag == "ball")
        {
           // GameObject.FindGameObjectWithTag("SkorTab").GetComponent<GolSkor>().Gol(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D coll) 
    {
        if (coll.gameObject.tag == "pufCharacter1" && coll.gameObject.GetComponent<MyCharacterController>().poi != null)
        {
            MyCharacterController tmp = coll.gameObject.GetComponent<MyCharacterController>();
            tmp.canJump = false;
        }

    }
    /*
    public void KnowMe(GameObject player) 
    {
        characterController = player.GetComponent<MyCharacterController>();
    }
    */
}