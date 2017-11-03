using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeControllerScript : MonoBehaviour {

    public float scrollSpeed = 5.0f;        // hareket hızı
    public GameObject[] challenges;         // challengeları içinde tutan dizi
    public float frequency = 1.0f;          // challenge çıkma sıklığı
    float counter = 0.0f;                   // sayaç
    public Transform challengesSpawnPoint;  // challenge çıktığı nokta
    bool isGameOver = false;                // oyun bitti mi kontrolü

	// Use this for initialization
	void Start () { // oyun başladığında 
        GenerateRandomChallenge();
	}
	
	// Update is called once per frame
	void Update () { // yukarıdaki satıra bak
        if (isGameOver) return; // oyun bittiyse bir şey yapma gerisine bakmadan bu fonksiyondan çık

        // Generate Objects
        if (counter <= 0.0f) // sayaç 0'dan büyükse yeni challenge oluştur
        {
            GenerateRandomChallenge();
        }
        else // sayacı azalt
        {
            counter -= Time.deltaTime * frequency;
        }

        // Scrolling
        GameObject currentChild; // mevcut oyun objesi
        for(int i=0; i < transform.childCount; i++) // oyun objesinin mevcut her çocuğunda bu işlemi yap
        {
            currentChild = transform.GetChild(i).gameObject; // oyun objesinin çocuğunu seç
            ScrollChallenge(currentChild);                   // onu kaydır
            if(currentChild.transform.position.x <= -17.0f)  // eğer baya uzağa gittiyse
            {
                Destroy(currentChild);                       // onu yok et
            }
        }

	}

    void ScrollChallenge (GameObject currentChallenge) // kaydırma fonksiyonu
    {
        currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    }

    void GenerateRandomChallenge() // rastgele challenge spawnlama
    {
        GameObject newChallenge = Instantiate(challenges[Random.Range(0, challenges.Length)], challengesSpawnPoint.position, Quaternion.identity) as GameObject;
        newChallenge.transform.parent = transform;
        counter = 1.0f; // sayacı 1'e eşitle
    }

    public void GameOver() // oyun bittiyse bitsin
    {
        isGameOver = true;
        transform.GetComponent<GameControllerScript>().GameOver();
    }
}
