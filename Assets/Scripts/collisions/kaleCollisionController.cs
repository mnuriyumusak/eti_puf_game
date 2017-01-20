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
        if (coll.gameObject.tag == "pufCharacter1" && characterController.poi != null)//Burada ki değişecek
        {
            //Deneme için buarya yazıyoruz
            if(characterController.poi.character.GetInstanceID() == coll.gameObject.GetInstanceID())
            {
                if (Math.Abs(kale.position.y - poi.charactery) >= 1.90f)
                {
                    GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().canJump = true;
                    GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().startGravity = false;
                }
            }
            
        }
        if (coll.gameObject.tag == "ball")
        {
            
           // GameObject.FindGameObjectWithTag("SkorTab").GetComponent<GolSkor>().Gol(gameObject);
        }
    }
    void OnCollisionExit2D(Collision2D coll)//burada ki değişecek
    {
        if (coll.gameObject.tag == "pufCharacter1" && characterController.poi != null)
        {
            if (characterController.poi.character.GetInstanceID() == coll.gameObject.GetInstanceID())
            {
                GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().canJump = false;

            }
        }
    }

    public void KnowMe(GameObject player)// Burada ki değişecek
    {
        //if(Network.isServer)
            characterController = player.GetComponent<MyCharacterController>();
       // else
      //      characterController = GameObject.FindGameObjectWithTag("pufCharacter2").GetComponent<MyCharacterController>();

    }
}