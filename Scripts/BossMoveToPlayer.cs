using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMoveToPlayer : MonoBehaviour
{
    GameObject player;
    Animator anim;

    public GameObject fire;

    private float distance;
    float timer;
    float timeBossStartShoot = 1.8f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        timer += Time.deltaTime;
        Move();
    }
    void Move()
    {
        if (distance > 1.5f)
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
            anim.SetTrigger("BossAttack");
            Shoot();
        }
    }
    void Shoot()
    {
        if(timer >= timeBossStartShoot)
        {
            Vector3 t = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
            GameObject fireF = (GameObject)Instantiate(fire, t, Quaternion.identity);
            fireF.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 100);
            Destroy(fireF, 2f);
            timer = 0;
        }
    }
}
