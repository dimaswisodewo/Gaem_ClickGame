using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickTarget : MonoBehaviour
{
    private int randNum = 0;
    public float timerku = 1.5f;
    public GameObject[] gridObj;
    public Text skor, lives, gameOver, highScore;
    private int skor_angka = 0;
    private int lives_angka = 2;

    void OnMouseDown()
    {
        if (lives_angka > 0)
        {
            Debug.Log("HIT!!!");
            skor_angka += 1;
            skor.text = "Skor = " + skor_angka.ToString();
            Respawn();
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("Scene01");
    }

    void Respawn()
    {
        transform.position = gridObj[randNum].transform.position;
        timerku = 1.5f;
    }

    void LivesMinus()
    {
        lives_angka -= 1;
        lives.text = "Lives = " + lives_angka.ToString();
    }

    void GameOver()
    {
        enabled = false;
        gameOver.gameObject.SetActive(true);
    }

    void Start()
    {
        gameOver.gameObject.SetActive(false);
        highScore.text = "Top Skor = "+PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    void HighScore()
    {
        if (skor_angka > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", skor_angka);
            highScore.text = "Top Skor = "+skor_angka.ToString();
        }
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        timerku -= Time.deltaTime;
        randNum = UnityEngine.Random.Range(0, 9);
        if (Math.Round(timerku) == 0)
        {
            Debug.Log(randNum);
            transform.position = gridObj[randNum].transform.position;
            timerku = 1.5f;
            LivesMinus();
            if (lives_angka <= 0)
            {
                HighScore();
                GameOver();
            }
            Respawn();
        }
    }

}
