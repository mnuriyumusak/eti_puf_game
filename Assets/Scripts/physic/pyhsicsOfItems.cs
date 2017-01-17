using UnityEngine;
using System.Collections;

/*
Bu class'ta oyundaki tüm objelerin rigidbody'leri fizikleri gameObject'leri tutuluyor ve diğer scriptlerde ihtiyaç
olması halinde diğer tüm scriptler player veya top veya başka bir item ile ilgili fizik gerektiren tüm değişkenlere
bu class üzerinden ulaşabiliyor.
*/
public class pyhsicsOfItems : MonoBehaviour {
    public GameObject character;
    public GameObject ball;
    public Rigidbody2D rbOfCharacter;
    public Rigidbody2D rbOfBall;
    public Transform upperBound,rightBound,leftBound,bottomBound; //boundaries 

    public float bottomLine;
    public float rightLine;
    public float leftLine;
    public float topLine;
    public float ballx;
    public float bally;
    public float characterx;
    public float charactery;
    
    // Use this for initialization
    void Start () {
        rbOfCharacter = character.GetComponent<Rigidbody2D>(); //player'in rigidbody'si tanımlanıyor 
        rbOfBall = ball.GetComponent<Rigidbody2D>();
        //Aşağıda oyun alanının sınırlarının x ve y koorinatlarını float olarak belirliyoruz 
        bottomLine = bottomBound.position.y;
        rightLine = rightBound.position.x;
        leftLine = leftBound.position.x;
        topLine = upperBound.position.y;
      
}
    // Update is called once per frame
    Vector3 lastPosition;
    float minimumMovement = .05f;
    void Update()
    {
        //bu methodda oyun içinde her an topun ve player'in yerini bilmemiz için x ve y koordinatlarını sürekli güncelleniyor
        ballx = rbOfBall.transform.position.x;
        bally = rbOfBall.transform.position.y;
        characterx = rbOfCharacter.transform.position.x;
        charactery = rbOfCharacter.transform.position.y;
         
      //  SetPosition(character.transform.position);

    }
   
   [RPC] void SetPosition(Vector3 newPosition)
    {
       /* float x, y;
        x = newPosition.x;
        y = newPosition.y;
        character.transform.position += new Vector3(x, y) * Time.deltaTime ;*/
      //  character.transform.position = Vector3.Lerp(character.transform.position, newPosition, Time.deltaTime * 5);
        print(newPosition.ToString());
    }

    public void changeBallVector3x(float newx)
    {
        rbOfBall.transform.position = new Vector3(newx, rbOfBall.transform.position.y, rbOfBall.transform.position.z);
    }

    public void changeBallVector3y(float newy)
    {
        rbOfBall.transform.position = new Vector3(rbOfBall.transform.position.x, newy , rbOfBall.transform.position.z);
    }

    public void changeCharacterVector3x(float newx)
    {
        rbOfCharacter.transform.position = new Vector3(newx, rbOfCharacter.transform.position.y, rbOfCharacter.transform.position.z);
    }

    public void changeCharacterVector3y(float newy)
    {
        rbOfCharacter.transform.position = new Vector3(rbOfCharacter.transform.position.x, newy, rbOfCharacter.transform.position.z);
    }

    public void changeCharacterVelocity(float newxVel)
    {
        rbOfCharacter.velocity = new Vector3(newxVel, rbOfCharacter.velocity.y, 0);
    }

    public void openGravityForPlayer()
    {
        rbOfCharacter.gravityScale = 1;
    }
    public void closeGravityForPlayer()
    {
        rbOfCharacter.gravityScale = 0;
    }
    public void KnowMe()
    {

         character = GameObject.FindGameObjectWithTag("pufCharacter1");
         rbOfCharacter = GameObject.FindGameObjectWithTag("pufCharacter1").GetComponent<Rigidbody2D>();
    }

}
