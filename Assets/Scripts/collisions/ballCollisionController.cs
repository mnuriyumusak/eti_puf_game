using UnityEngine;
using System.Collections;
using System;

/*
bu calss topun çarpıp etkileşime geçtiği objelere tepkilerini içerir. Ön plan player'a aittir. 
*/
public class ballCollisionController : MonoBehaviour {
    // Use this for initialization
    public pyhsicsOfItems poi; //tüm fizik ihtiyaçlarının karşılandığı değişken 
    public MyCharacterController characterController; //karakterin bazı durumlarının bilinmesi gerekiyor mesela sağa mı gidiyor 
                                                      //sola mı gidiyor, zıplama halinde mi vs. O yüzden bu gerekli 
     
    void Start()
    {
        poi = GameObject.FindGameObjectWithTag("poi").GetComponent<pyhsicsOfItems>();
        characterController = GameObject.FindGameObjectWithTag("pufCharacter1").GetComponent<MyCharacterController>();
 


    }

    //detect if character and ball collide or not
    void OnCollisionEnter2D(Collision2D coll)
    {
        //eğer player üstte ve top aşağıdaysa fırlatıp atıyor topu yuarıya. 
        if (coll.gameObject.tag == "pufCharacter1" || coll.gameObject.tag == "pufCharacter2")
        {
            if(!GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().stopMovingLeft)
                poi.rbOfBall.velocity = new Vector2(-GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().ballReactionSpeed, 0);
            else if (!GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().stopMovingRight)
                poi.rbOfBall.velocity = new Vector2(GameObject.FindGameObjectWithTag(coll.gameObject.tag).GetComponent<MyCharacterController>().ballReactionSpeed, 0);

            if (Math.Abs(Math.Abs(poi.bally) - Math.Abs(poi.charactery)) <= 0.8f &&
                Math.Abs(Math.Abs(poi.bally) - Math.Abs(poi.charactery)) >= 0.5f && 
                Math.Abs(Math.Abs(poi.bally) - Math.Abs(poi.bottomLine)) <= 1.2f)//puf üstte top alttaysa topu yukarı çıkarıp fırlatıyor
            {
                poi.changeBallVector3y(poi.charactery + 1f);
                poi.rbOfCharacter.velocity = new Vector3(0, 2, 0);
            }
        }
    }
}
