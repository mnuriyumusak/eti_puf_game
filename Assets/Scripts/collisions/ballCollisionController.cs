using UnityEngine;
using System.Collections;
using System;

/*
bu calss topun çarpıp etkileşime geçtiği objelere tepkilerini içerir. Ön plan player'a aittir. 
*/
public class ballCollisionController : MonoBehaviour {
    //public MyCharacterController characterController;
    public Rigidbody2D rbOfBall;
    void Start()
    {
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
        //if (characterController != null)
        //{
        
            if (coll.gameObject.tag == "pufCharacter1" && coll.gameObject.GetComponent<MyCharacterController>().poi != null)
            {
                MyCharacterController tmp = coll.gameObject.GetComponent<MyCharacterController>();
                if (!tmp.stopMovingLeft)
                rbOfBall.velocity = new Vector2(-tmp.ballReactionSpeed, 0);
                    else if (!tmp.stopMovingRight)
                rbOfBall.velocity = new Vector2(tmp.ballReactionSpeed, 0);

                    if (Math.Abs(Math.Abs(rbOfBall.transform.position.y) - Math.Abs(tmp.poi.charactery)) <= 0.8f &&
                        Math.Abs(Math.Abs(rbOfBall.transform.position.y) - Math.Abs(tmp.poi.charactery)) >= 0.5f &&
                        Math.Abs(Math.Abs(rbOfBall.transform.position.y) - Math.Abs(tmp.poi.bottomLine)) <= 1.2f)//puf üstte top alttaysa topu yukarı çıkarıp fırlatıyor
                    {
                changeBallVector3y(tmp.poi.charactery + 1f);
                tmp.poi.rbOfCharacter.velocity = new Vector3(0, 2, 0);
                    }
                    else if(Math.Abs(Math.Abs(rbOfBall.transform.position.x) - Math.Abs(tmp.poi.characterx)) <= 0.8f &&
                            Math.Abs(Math.Abs(rbOfBall.transform.position.x) - Math.Abs(tmp.poi.charactery)) <= 0.15f)
                    {
                changeBallVector3y(tmp.poi.charactery + 1f);
                tmp.poi.rbOfCharacter.velocity = new Vector3(0, 2, 0);
                    }
            }
        //}
    }
    /*
    public void KnowMe(GameObject player)
    {
        characterController = player.GetComponent<MyCharacterController>();
    }
    */
    public void changeBallVector3x(float newx)
    {
        rbOfBall.transform.position = new Vector3(newx, rbOfBall.transform.position.y, rbOfBall.transform.position.z);
    }

    public void changeBallVector3y(float newy)
    {
        rbOfBall.transform.position = new Vector3(rbOfBall.transform.position.x, newy, rbOfBall.transform.position.z);
    }

}
