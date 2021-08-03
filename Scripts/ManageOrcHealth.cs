using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageOrcHealth : MonoBehaviour
{
    int dame = 100;
    float orcHealth = 100f;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (orcHealth <= 0)
        {
            Death();
        }
    }
    public void GotHit(int dame)
    {
        anim.SetTrigger("GetHit");
        orcHealth -= dame;
    }
    void Death()
    {
        gameObject.GetComponentInParent<OrcMoveToPlayer>().enabled = false;
        Destroy(gameObject, 0.5f);
        GameObject.Find("GameController").GetComponent<GameController>().IncreasePoint(100);
    }
}
