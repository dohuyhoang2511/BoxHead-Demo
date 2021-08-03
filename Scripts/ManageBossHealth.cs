using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBossHealth : MonoBehaviour
{
    float bossHealth = 200f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth <= 0)
        {
            Death();
        }
    }
    public void GotHit(int dame)
    {
        anim.SetTrigger("GetHit");
        bossHealth -= dame;
    }
    void Death()
    {
        gameObject.GetComponentInParent<BossMoveToPlayer>().enabled = false;
        Destroy(gameObject, 0.5f);
        GameObject.Find("GameController").GetComponent<GameController>().IncreasePoint(200);
    }
}
