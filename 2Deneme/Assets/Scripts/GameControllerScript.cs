using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

    public GameObject gameOverPanel; // Oyuncu yandığında çıkacak olan ekran
    public Text scoreText;           // Oyuncu yanmadığı sürece ekranın üst kısmında duran text
    int score = 0;                   
    public Text bestText;
    public Text currentText;
    public GameObject newAlert;      // Yeni rekor kırılması halinde verilecek uyarı

	// Oyuncu yandığında
    public void GameOver()
    {
        Invoke("ShowOverPanel", 1.0f); // 1 saniye içinde ShowOverPanel fonksiyonunu çalıştır
    }

    void ShowOverPanel()
    {
        scoreText.gameObject.SetActive(false);  // Score'u disable et

        if(score > PlayerPrefs.GetInt("Best", 0))   // Eğer mevcut skor en iyi skordan iyiyse 
        {
            PlayerPrefs.SetInt("Best", score);  // Yeni en iyi skoru mevcut skora eşitle
            newAlert.SetActive(true);           // Uyarı ver
        }

        // Text'lere en iyi ve mevcut skoru yazdır
        bestText.text = "Best Score : " + PlayerPrefs.GetInt("Best", 0).ToString();
        currentText.text = "Your Score : " + score.ToString();

        // Paneli görünür yap
        gameOverPanel.SetActive(true);
    }

    // Yenien başlat
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevelName); 
    }

    // Skoru arttır
    public void IncrementScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    // Uygulamadan çık
    public void ExitGame()
    {
        Application.Quit();
    }
}
