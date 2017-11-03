using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float jumpPower;         // Zıplama Gücü
    Rigidbody2D myRigidbody;        // Karakterin fiziksel bedeni
    public bool isGrounded = false; // Karakter zeminde mi
    float posX = 0.0f;              // Karakterin pozisyonu
    bool isGameOver = false;        // Oyun bitti mi ? 
    ChallengeControllerScript myChallengeController;    // Başka sınıflardan da kullanacağımız
    GameControllerScript myGameController;              // Fonksiyonlar var bu nedenle tanımlama yapıyoruz

	// Use this for initialization
	void Start () { // Başlangıçta
        myRigidbody = transform.GetComponent<Rigidbody2D>(); // Fiziksel bedeni mevcut karakterin fiziksel bedenine eşitle
        posX = transform.position.x;                         // Pozisyonu pozisyona eiştle
        myChallengeController = GameObject.FindObjectOfType<ChallengeControllerScript>();
        myGameController = GameObject.FindObjectOfType<GameControllerScript>();
	} // Tanımlamalar böyle findobjectoftype<sinifismi>(); şeklinde yapılıyor
	
	void FixedUpdate () { // Rigidbody ile uğraşırken fixedupdate kullanılır
	    if(Input.GetKey(KeyCode.Space) && isGrounded && !isGameOver) // Oyun bitmemişse ve karakter yerdeyse ve space'e basılırsa
        { // Karakterin fiziksel bedenine yukarı doğru kuvvet uygula
            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
        }	

        // Hit the face check
        if(transform.position.x < posX)
        { // Eğer karakterimiz başlangıçtaki yerinde değilse oyunu bitir
            GameOver();
        }
	}

    // Oyun bitti
    public void GameOver() 
    {
        isGameOver = true;
        myChallengeController.GameOver();
    }

    // Bir çarpışma olduğunda
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground") // Eğer çarpışılan nesne ground tagına sahipse yerdeyiz demektir
        {
            isGrounded = true;
        }

        if (other.collider.tag == "Enemy") // Eğer çarpışılan nesne enemy tagına sahipse yandık demektir
        {
            GameOver();
        }
    }

    void OnCollisionStay2D(Collision2D other) // Çarpışma devam ederken
    {
        if (other.collider.tag == "Ground") // eğer yerdeysek yerdeyiz demektir
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) // Çarpışma bittiğinde
    {
        if (other.collider.tag == "Ground") // Çarpışmadan ayrıldığımız eleman ground tagına sahipse yerde değiliz demektir
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D (Collider2D other) // Tetikleyici
    {
        if(other.tag == "Star") // Tetiklediğimiz çarpıştırıcı star tagına sahipse
        {
            myGameController.IncrementScore(); // puanı arttır
            Destroy(other.gameObject);         // yıldızı yok et
        }
    }
}
