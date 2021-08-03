using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcMoveToPlayer : MonoBehaviour
{
    GameObject player;
    Animator anim;

    private float distance;
    int dame = 100;
    float timer;
    float timeOrcAttack = 1.5f;
    bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        distance = Vector3.Distance(transform.position, player.transform.position);
        Move();
        if (isAttack)
        {
            if (timer >= timeOrcAttack)
            {
                GameObject.Find("Player").GetComponent<ManagePlayerHealth>().GotHit(dame);
                timer = 0;
            }
            isAttack = false;
        }
    }
    void Move()
    {
        if (distance > 0.5f)
        {
            Vector3 t = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            transform.LookAt(t);
            GetComponentInChildren<NavMeshAgent>().destination = player.transform.position;
            GetComponentInChildren<NavMeshAgent>().isStopped = false;
            anim.SetBool("ZombieWalk", true);
        }
        else
        {
            GetComponentInChildren<NavMeshAgent>().isStopped = true;
            anim.SetBool("ZombieWalk", false);
            anim.SetTrigger("ZombieAttack");
            isAttack = true;
        }
    }
}
