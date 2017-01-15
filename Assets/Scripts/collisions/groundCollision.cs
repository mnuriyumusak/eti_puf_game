using UnityEngine;
using System.Collections;
using System;
/*
Ground'un tepkilerini içerir. 
*/
public class groundCollision : MonoBehaviour {

    public MyCharacterController characterController; //canJump player yere değince aktifleştiği ve canJump'a bu class'tan erişildiği
                                                      //için bu gerekli
    public pyhsicsOfItems poi; //fiziksel değişkenlerin yerleri lazım olduğundan gerekli. 
    public Transform ground; //yerin x ve y koordinatları gerektiğinden gerekli .
    public myInput inputManager; //yere değince sağa gidiyosa sağa hareket durduğu için çarpmadan dolayı tekrar aktifleştirmek için
                                 //myInput classından tekrar sağa veya sola git metodları seçiliyor.

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "pufCharacter1")
        {
           coll.gameObject.GetComponent<MyCharacterController>().canJump = true;
           coll.gameObject.GetComponent<MyCharacterController>().caprazSagaGidiyor = false;
           coll.gameObject.GetComponent<MyCharacterController>().caprazSolaGidiyor = false;
            coll.gameObject.GetComponent<MyCharacterController>().caprazdaYonDegisti = false;
            coll.gameObject.GetComponent<MyCharacterController>().startGravity = false;


            if (!coll.gameObject.GetComponent<MyCharacterController>().stopMovingRight)
                inputManager.butonRight.Select();
            if (!coll.gameObject.GetComponent<MyCharacterController>().stopMovingLeft)
                inputManager.butonLeft.Select();
        }
        //top bazen yere gömülüyor onu yukarı çıkarmak için yazılmış bir kod.
        if (coll.gameObject.tag == "ball")
        {
            if (Math.Abs(ground.position.y - poi.bally) <= 0.9f)
            {
                poi.changeBallVector3y(poi.charactery);
            }
        }
    }

    public void KnowMe()
    {
 
        characterController = GameObject.FindGameObjectWithTag("pufCharacter1").GetComponent<MyCharacterController>();
 


    }

}
