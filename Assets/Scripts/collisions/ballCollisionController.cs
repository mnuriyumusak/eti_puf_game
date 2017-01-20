using UnityEngine;
using System.Collections;
using System;

/*
bu calss topun çarpıp etkileşime geçtiği objelere tepkilerini içerir. Ön plan player'a aittir. 
*/
public class ballCollisionController : MonoBehaviour {
    // Use this for initialization
    //public pyhsicsOfItems poi; //tüm fizik ihtiyaçlarının karşılandığı değişken 
    //public MyCharacterController characterController; //karakterin bazı durumlarının bilinmesi gerekiyor mesela sağa mı gidiyor 
                                                      //sola mı gidiyor, zıplama halinde mi vs. O yüzden bu gerekli 
    void Start()
    {
        //poi = GameObject.FindGameObjectWithTag("pufCharacter1").GetComponent<pyhsicsOfItems>();
        //characterController = GameObject.FindGameObjectWithTag("pufCharacter1").GetComponent<MyCharacterController>();// Raycast ile değişecek
    }

    /*
    void FixedUpdate()
    {
        Debug.DrawRay(this.gameObject.transform.position, Vector2.up, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.up, 5f);
        if(hit)
        {
            if (hit.collider.gameObject.tag == "pufCharacter1") 
            {
                if (!hit.collider.gameObject.GetComponent<MyCharacterController>().stopMovingLeft)
                    hit.collider.gameObject.GetComponent<MyCharacterController>().poi.rbOfBall.velocity = new Vector2(-hit.collider.gameObject.GetComponent<MyCharacterController>().ballReactionSpeed, 0);
                else if (!hit.collider.gameObject.GetComponent<MyCharacterController>().stopMovingRight)
                    hit.collider.gameObject.GetComponent<MyCharacterController>().poi.rbOfBall.velocity = new Vector2(hit.collider.gameObject.GetComponent<MyCharacterController>().ballReactionSpeed, 0);

                if (Math.Abs(Math.Abs(hit.collider.gameObject.GetComponent<MyCharacterController>().poi.bally) - Math.Abs(hit.collider.gameObject.GetComponent<MyCharacterController>().poi.charactery)) <= 0.8f &&
                    Math.Abs(Math.Abs(hit.collider.gameObject.GetComponent<MyCharacterController>().poi.bally) - Math.Abs(hit.collider.gameObject.GetComponent<MyCharacterController>().poi.charactery)) >= 0.5f &&
                    Math.Abs(Math.Abs(hit.collider.gameObject.GetComponent<MyCharacterController>().poi.bally) - Math.Abs(hit.collider.gameObject.GetComponent<MyCharacterController>().poi.bottomLine)) <= 1.2f)//puf üstte top alttaysa topu yukarı çıkarıp fırlatıyor
                {
                    hit.collider.gameObject.GetComponent<MyCharacterController>().poi.changeBallVector3y(hit.collider.gameObject.GetComponent<MyCharacterController>().poi.charactery + 1f);
                    hit.collider.gameObject.GetComponent<MyCharacterController>().poi.rbOfCharacter.velocity = new Vector3(0, 2, 0);
                }
            }
        }
        */
        
    //detect if character and ball collide or not
    void OnCollisionEnter2D(Collision2D coll)
    {
        //eğer player üstte ve top aşağıdaysa fırlatıp atıyor topu yuarıya. 
        if (coll.gameObject.tag == "pufCharacter1")// Raycast ile değişicek
        {

            if (!coll.gameObject.GetComponent<MyCharacterController>().stopMovingLeft)
                coll.gameObject.GetComponent<MyCharacterController>().poi.rbOfBall.velocity = new Vector2(-coll.gameObject.GetComponent<MyCharacterController>().ballReactionSpeed, 0);
            else if (!coll.gameObject.GetComponent<MyCharacterController>().stopMovingRight)
                coll.gameObject.GetComponent<MyCharacterController>().poi.rbOfBall.velocity = new Vector2(coll.gameObject.GetComponent<MyCharacterController>().ballReactionSpeed, 0);

            if (Math.Abs(Math.Abs(coll.gameObject.GetComponent<MyCharacterController>().poi.bally) - Math.Abs(coll.gameObject.GetComponent<MyCharacterController>().poi.charactery)) <= 0.8f &&
                Math.Abs(Math.Abs(coll.gameObject.GetComponent<MyCharacterController>().poi.bally) - Math.Abs(coll.gameObject.GetComponent<MyCharacterController>().poi.charactery)) >= 0.5f &&
                Math.Abs(Math.Abs(coll.gameObject.GetComponent<MyCharacterController>().poi.bally) - Math.Abs(coll.gameObject.GetComponent<MyCharacterController>().poi.bottomLine)) <= 1.2f)//puf üstte top alttaysa topu yukarı çıkarıp fırlatıyor
            {
                coll.gameObject.GetComponent<MyCharacterController>().poi.changeBallVector3y(coll.gameObject.GetComponent<MyCharacterController>().poi.charactery + 1f);
                coll.gameObject.GetComponent<MyCharacterController>().poi.rbOfCharacter.velocity = new Vector3(0, 2, 0);
            }
        }
        
    }
    
}
