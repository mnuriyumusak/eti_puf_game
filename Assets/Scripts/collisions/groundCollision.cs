using UnityEngine;
using System.Collections;
using System;
/*
Ground'un tepkilerini içerir. 
*/
public class groundCollision : MonoBehaviour {

    public MyCharacterController characterController; //canJump player yere değince aktifleştiği ve canJump'a bu class'tan erişildiği
                                                      //için bu gerekli
    //public pyhsicsOfItems poi; //fiziksel değişkenlerin yerleri lazım olduğundan gerekli. 
    public Transform ground; //yerin x ve y koordinatları gerektiğinden gerekli .
    public myInput inputManager; //yere değince sağa gidiyosa sağa hareket durduğu için çarpmadan dolayı tekrar aktifleştirmek için
                                 //myInput classından tekrar sağa veya sola git metodları seçiliyor.

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(characterController != null)
        {
            if (coll.gameObject.tag == "pufCharacter1" && characterController.poi != null)
            {
                if (characterController.poi.character.GetInstanceID() == coll.gameObject.GetInstanceID())
                {
                    characterController.characterIsOnKale = false;
                    characterController.changeJumpHeight();
                    if (!characterController.canJump)
                    {
                        if (!characterController.stopMovingRight)
                            inputManager.butonRight.Select();
                        if (!characterController.stopMovingLeft)
                            inputManager.butonLeft.Select();
                    }

                    characterController.canJump = true;
                    characterController.caprazSagaGidiyor = false;
                    characterController.caprazSolaGidiyor = false;
                    characterController.caprazdaYonDegisti = false;
                    characterController.startGravity = false;
                    characterController.changeMovementSpeed();
                }

            }

            //top bazen yere gömülüyor onu yukarı çıkarmak için yazılmış bir kod.
            if (coll.gameObject.tag == "ball")
            {
                if (Math.Abs(ground.position.y - characterController.poi.bally) <= 0.9f)
                {
                    characterController.poi.changeBallVector3y(characterController.poi.charactery);
                }
            }
        }
       

    }

    public void KnowMe(GameObject player, myInput mi)
    {
        characterController = player.GetComponent<MyCharacterController>();
        inputManager = mi;
    }

}
