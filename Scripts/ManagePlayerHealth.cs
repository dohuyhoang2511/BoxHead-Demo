using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagePlayerHealth : MonoBehaviour
{
    float playerHealth = 1000;
    public Slider healthBar;

    Animator anim;
    AudioSource audioS;

    public AudioClip playerHurt;

    float timeHealing = 10f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();

        healthBar.maxValue = 1000;
        healthBar.value = playerHealth;
        healthBar.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.deltaTime;
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
        if(timer >= timeHealing)
        {
            if(playerHealth < 1000 && playerHealth > 0)
            {
                Healing();
            }
        }    
    }
    public void GotHit(int dame)
    {
        audioS.clip = playerHurt;
        audioS.Play();

        anim.SetTrigger("GetHit");
        playerHealth -= dame;

        healthBar.value = playerHealth;
    }
    void Healing()
    {
        playerHealth += 50;

        healthBar.value = playerHealth;

        timer = 0;
    }
}
