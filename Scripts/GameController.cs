using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int point = 0;

    public GameObject startMenu;
    public Button btnStartGame;
    public Button btnHowToPlay;
    public Button btnBack;
    public Button btnRestart;
    Scene t;
    // Start is called before the first frame update
    void Start()
    {
        t = SceneManager.GetActiveScene();
        Time.timeScale = 0;

        if (t.name == "PlayScene")
        {
            startMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        t = SceneManager.GetActiveScene();
        if (t.name == "PlayScene")
        {
            GameObject.Find("Point").GetComponent<Text>().text = "Point : " + point;
        }
    }
    public void IncreasePoint(int p)
    {
        point += p;
    }
    public void StartGame()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGameButtonHover()
    {
        btnStartGame.GetComponent<Image>().color = Color.black;
        btnStartGame.GetComponentInChildren<Text>().color = Color.white;
    }
    public void StartGameButtonIdle()
    {
        btnStartGame.GetComponent<Image>().color = Color.white;
        btnStartGame.GetComponentInChildren<Text>().color = Color.black;
    }
    public void StartGameButtonClick()
    {
        btnStartGame.GetComponent<Image>().color = Color.red;
        btnStartGame.GetComponentInChildren<Text>().color = Color.red;
    }
    public void HowToPlayButtonHover()
    {
        btnHowToPlay.GetComponent<Image>().color = Color.black;
        btnHowToPlay.GetComponentInChildren<Text>().color = Color.white;
    }
    public void HowToPlayButtonIdle()
    {
        btnHowToPlay.GetComponent<Image>().color = Color.white;
        btnHowToPlay.GetComponentInChildren<Text>().color = Color.black;
    }
    public void HowToPlayButtonClick()
    {
        btnHowToPlay.GetComponent<Image>().color = Color.red;
        btnHowToPlay.GetComponentInChildren<Text>().color = Color.red;
    }
    public void BackButtonHover()
    {
        btnBack.GetComponent<Image>().color = Color.black;
        btnBack.GetComponentInChildren<Text>().color = Color.white;
    }
    public void BackButtonIdle()
    {
        btnBack.GetComponent<Image>().color = Color.white;
        btnBack.GetComponentInChildren<Text>().color = Color.black;
    }
    public void BackButtonClick()
    {
        btnBack.GetComponent<Image>().color = Color.red;
        btnBack.GetComponentInChildren<Text>().color = Color.red;
    }
    public void RestartButtonHover()
    {
        btnRestart.GetComponent<Image>().color = Color.black;
        btnRestart.GetComponentInChildren<Text>().color = Color.white;
    }
    public void RestartButtonIdle()
    {
        btnRestart.GetComponent<Image>().color = Color.white;
        btnRestart.GetComponentInChildren<Text>().color = Color.black;
    }
    public void RestartButtonClick()
    {
        btnRestart.GetComponent<Image>().color = Color.red;
        btnRestart.GetComponentInChildren<Text>().color = Color.red;
    }
}
