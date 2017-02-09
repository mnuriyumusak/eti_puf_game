using UnityEngine;
using System.Collections;
using System;
/*
Player'ın nerde ne yapacağı şu anki durumunun ne olduğu vs her tülü bilgiyi edinebileceğimiz bir class. 
*/
public class MyCharacterController : MonoBehaviour
{

    public enum Directions { Forwards, Backwards, Nowhere }
    public pyhsicsOfItems poi; //pyhsic of items , fizikle ilgili herşey poi'den çekilecek. 

    private float MovmentSpeed = 5; //hareket hızı değişkeni
    public bool canJump = true; //karakterin zıplayabilir mi olduğunu bildirir. Mesela havadayken 1 daha zıplayamaz.
    public bool caprazSagaGidiyor = false; //çapraz yani hem jump hem de sağ ve ya sol tuşa basılıp basılmadığı gösterir. 
    public bool caprazSolaGidiyor = false; //çapraz yani hem jump hem de sağ ve ya sol tuşa basılıp basılmadığı gösterir. 
    public bool caprazdaYonDegisti = false; //çapraz giderken tam ters yöndeki tuşa basılıp basılmadığının anlaşılmasına yarar. 
    private float JumpForce;
    public float JumpForceKatsayisi = 14; //zıplama gücü 
    public float JumpSmoothKatsayisi = 0.95f; //yukarda yavaşlama katsayısı 
    private float jumpStartHeight = 1.5f;
    public float maxJumpHeight = 1.5f;
    private Vector3 movement; //movement vektörü sağa sola giderken kullanılan bir değişken 
    private Vector3 jumpMovement; //movement vektörünün zıplarken gideceği yolu belirleyen vektör 
    public IENumaratorHandler timehadnler;
    public bool stopMovingRight = true; //butona basılma bitinceye kadar bu false döner
    public bool stopMovingLeft = true; //butona basılma bitinceye kadar bu false döner
    public float speedOfFall = 2; //karakterin zıplayıp düşerken onu aşağı çeken kuvvetin büyüklüğü, daha hızlı düşcek böylece. 
    public float ballReactionSpeed = 3.5f; //topa bir cisim vurduğunda topun karşı tepkideki hızı 
    public bool startGravity = false;
    public bool jumpActiavted = false;
    public bool characterIsOnKale = false;
 
    public void changeJumpHeight()
    {
        //kalenin üstünden zıplarken biraz daha üste çıkması için gerekli
        if (characterIsOnKale)
            maxJumpHeight = 3.10f;
        else
            maxJumpHeight = 1.5f;
    }

    void Update()
    {

        if (poi != null)
        {
            //yere ininci gravitiy açıyor,  havadayken kapatıyor 
            if (canJump)
                poi.openGravityForPlayer();

            if (jumpActiavted)
            {
                //yukarı doğru azalan bir force bu da smooth bir zıplama sağlar yani duvara çarpıyomuş gibi değil
                JumpForce = JumpSmoothKatsayisi + (JumpForceKatsayisi - ((JumpForceKatsayisi / (maxJumpHeight - jumpStartHeight)) * (poi.rbOfCharacter.transform.position.y - jumpStartHeight)));

                jumpMovement = new Vector3(0f, JumpForce * Time.deltaTime, 0f);

                jumpMovement = jumpMovement + poi.rbOfCharacter.transform.position;
                poi.rbOfCharacter.transform.position = jumpMovement;
            }
            if (poi.rbOfCharacter.transform.position.y > -3.29 && startGravity)
            {
                //aşağı doğru azalan bir force bu da smooth bir zıplama sağlar yani duvara çarpıyomuş gibi değil
                JumpForce = JumpSmoothKatsayisi + (JumpForceKatsayisi - ((JumpForceKatsayisi / (maxJumpHeight - jumpStartHeight)) * (poi.rbOfCharacter.transform.position.y - jumpStartHeight)));

                jumpMovement = new Vector3(0f, -JumpForce * Time.deltaTime, 0f);
               
                jumpMovement = jumpMovement + poi.rbOfCharacter.transform.position;
                poi.rbOfCharacter.transform.position = jumpMovement;
            }

            if (poi.rbOfCharacter.transform.position.y >= maxJumpHeight - 0.2f)
            {
                startGravity = true;
                jumpActiavted = false;
            }
        }
        
    }

    void Start()
    {
        //poi = new pyhsicsOfItems(this.gameObject, upperBound, rightBound, leftBound, bottomBound);
        //poi = GameObject.FindGameObjectWithTag("poi").GetComponent<pyhsicsOfItems>();
        // gameObject.GetComponent<NetworkView>().stateSynchronization = NetworkStateSynchronization.Unreliable;
    }

    //bu method player'in sağa sola gitmesi için gereken kodun olduğu yani sağa git butonuna basınca myInput class'ından
    //bu method çağırılarak hareket sağlanıyor. 
    public void movingActionItself(Directions Direction)
    {
        if (poi.rbOfCharacter.velocity.x > 0 && Direction == Directions.Backwards)
            poi.changeCharacterVelocity(0f);
        if (poi.rbOfCharacter.velocity.x < 0 && Direction == Directions.Forwards)
            poi.changeCharacterVelocity(0f);

        if (Direction != Directions.Nowhere)
        {
            if (Direction == Directions.Forwards)
            {
                movement = new Vector3(MovmentSpeed * Time.deltaTime, 0.0f, 0f);
            }
            else
            {
                movement = new Vector3(-MovmentSpeed * Time.deltaTime, 0.0f, 0f);
            }
            movement = movement + poi.rbOfCharacter.transform.position;
            poi.rbOfCharacter.transform.position = movement;
        }

    }
 
    //player'in zıplamasının esas olarak gerçekleştiği metod. 
    public void Jump()
    {
        //sola ve zıplamaya birden basılmışsa sağa çapraz gitmesi için 
        if (!stopMovingLeft && canJump)
        {
            //poi.rbOfCharacter.velocity = new Vector3(-JumpForce / 2, JumpForce, 0);
            canJump = false;
            caprazSolaGidiyor = true;
            jumpActiavted = true;
            poi.closeGravityForPlayer();
            jumpStartHeight = poi.rbOfCharacter.transform.position.y;
            changeMovementSpeed();
        }
        //sağa ve zıplamaya birden basılmışsa sola çapraz gitmesi için 
        else if (!stopMovingRight && canJump)
        {
            //poi.rbOfCharacter.velocity = new Vector3(JumpForce / 2, JumpForce, 0);
            canJump = false;
            caprazSagaGidiyor = true;
            jumpActiavted = true;
            poi.closeGravityForPlayer();
            jumpStartHeight = poi.rbOfCharacter.transform.position.y;
            changeMovementSpeed();
        }
        //düz bir şekilde zıplaması için 
        else if (canJump)
        {
            //poi.rbOfCharacter.velocity = new Vector3(0, JumpForce, 0);
            canJump = false;
            jumpActiavted = true;
            poi.closeGravityForPlayer();
            jumpStartHeight = poi.rbOfCharacter.transform.position.y;
            changeMovementSpeed();
        }
    }

    public void changeMovementSpeed()
    {
        if (canJump)
            MovmentSpeed = 5;
        else
            MovmentSpeed = 2.5f;
    }



}
