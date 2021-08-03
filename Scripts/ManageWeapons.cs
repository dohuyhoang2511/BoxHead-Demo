using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageWeapons : MonoBehaviour
{
    GameObject player;
    GameObject objectTarget;
    Transform firePoint;
    AudioSource audioS;

    public AudioClip gunShoot;
    public AudioClip rocketShoot;
    public AudioClip explo;
    public AudioClip mineExplo;

    public GameObject grenade;
    public GameObject wall;
    public GameObject mine;
    public GameObject gasTank;
    public GameObject rocketExplo;

    Ray rayFromPlayer;
    RaycastHit hit;

    private LineRenderer lineRenderer;

    private const int WEAPON_PISTOL = 0;
    private const int WEAPON_UZI = 1;
    private const int WEAPON_GRENADE = 2;
    private const int WEAPON_WALL = 3;
    private const int WEAPON_GAS_TANK = 4;
    private const int WEAPON_MINE = 5;
    private const int WEAPON_ROCKET = 6;
    private int activeWeapon = WEAPON_PISTOL;

    private float timer;
    private bool timerStarted;
    private bool canShoot = true;
    private bool canThrow = true;
    private bool canSet = true;
    private int currentWeapon;

    private bool[] hasWeapon;
    private int[] ammos;
    private int[] maxAmmos;
    private float[] reloadTime;
    private string[] weaponName;
    private int[] dame;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        firePoint = GameObject.Find("FirePoint").transform;
        audioS = GetComponent<AudioSource>();

        ammos = new int[7];
        hasWeapon = new bool[7];
        maxAmmos = new int[7];
        reloadTime = new float[7];
        weaponName = new string[7];
        dame = new int[7];

        hasWeapon[WEAPON_PISTOL] = true;
        hasWeapon[WEAPON_UZI] = false;
        hasWeapon[WEAPON_GRENADE] = false;
        hasWeapon[WEAPON_WALL] = false;
        hasWeapon[WEAPON_GAS_TANK] = false;
        hasWeapon[WEAPON_MINE] = false;
        hasWeapon[WEAPON_ROCKET] = false;

        weaponName[WEAPON_PISTOL] = "PISTOL";
        weaponName[WEAPON_UZI] = "UZI";
        weaponName[WEAPON_GRENADE] = "GRENADE";
        weaponName[WEAPON_WALL] = "WALL";
        weaponName[WEAPON_GAS_TANK] = "GAS TANK";
        weaponName[WEAPON_MINE] = "MINE";
        weaponName[WEAPON_ROCKET] = "ROCKET";

        ammos[WEAPON_PISTOL] = 50;
        ammos[WEAPON_UZI] = 0;
        ammos[WEAPON_GRENADE] = 0;
        ammos[WEAPON_WALL] = 0;
        ammos[WEAPON_GAS_TANK] = 0;
        ammos[WEAPON_MINE] = 0;
        ammos[WEAPON_ROCKET] = 0;

        maxAmmos[WEAPON_PISTOL] = 500;
        maxAmmos[WEAPON_UZI] = 1000;
        maxAmmos[WEAPON_GRENADE] = 100;
        maxAmmos[WEAPON_WALL] = 50;
        maxAmmos[WEAPON_GAS_TANK] = 50;
        maxAmmos[WEAPON_MINE] = 50;
        maxAmmos[WEAPON_ROCKET] = 50;

        reloadTime[WEAPON_PISTOL] = 0.7f;
        reloadTime[WEAPON_UZI] = 0.1f;
        reloadTime[WEAPON_GRENADE] = 3.0f;
        reloadTime[WEAPON_WALL] = 1.0f;
        reloadTime[WEAPON_GAS_TANK] = 1.5f;
        reloadTime[WEAPON_MINE] = 1.0f;
        reloadTime[WEAPON_ROCKET] = 5.0f;

        dame[WEAPON_PISTOL] = 50;
        dame[WEAPON_UZI] = 50;
        dame[WEAPON_GRENADE] = 100;
        dame[WEAPON_WALL] = 0;
        dame[WEAPON_GAS_TANK] = 100;
        dame[WEAPON_MINE] = 100;
        dame[WEAPON_ROCKET] = 500;

        currentWeapon = WEAPON_PISTOL;

    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;
            if (timer >= reloadTime[currentWeapon])
            {
                timerStarted = false;
                canShoot = true;
                canThrow = true;
                canSet = true;
            }
        }

        SetRay(10);
        
        if (Input.GetKey(KeyCode.Space))
        {
            if(Physics.Raycast(rayFromPlayer, out hit, 10))
            {
                print(hit.collider.gameObject.tag);
                if(currentWeapon == WEAPON_PISTOL || currentWeapon == WEAPON_UZI || currentWeapon == WEAPON_ROCKET)
                {
                    Shoot();
                }    
                if(currentWeapon == WEAPON_GRENADE)
                {
                    Throw();
                }
                if (currentWeapon == WEAPON_WALL || currentWeapon == WEAPON_GAS_TANK || currentWeapon == WEAPON_MINE)
                {
                    Set();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CheckWeapon();
        }

        GameObject.Find("gunInfo").GetComponent<Text>().text = weaponName[currentWeapon] + "(" + ammos[currentWeapon] + ")";
    }
    void CheckWeapon()
    {
        if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && hasWeapon[WEAPON_WALL] && hasWeapon[WEAPON_GAS_TANK] && hasWeapon[WEAPON_MINE] && hasWeapon[WEAPON_ROCKET])
        {
            currentWeapon++;
            if (currentWeapon > 6)
            {
                currentWeapon = 0;
            }
        }
        else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && hasWeapon[WEAPON_WALL] && hasWeapon[WEAPON_GAS_TANK] && hasWeapon[WEAPON_MINE])
        {
            currentWeapon++;
            if (currentWeapon > 5)
            {
                currentWeapon = 0;
            }
        }
        else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && hasWeapon[WEAPON_WALL] && hasWeapon[WEAPON_GAS_TANK])
        {
            currentWeapon++;
            if (currentWeapon > 4)
            {
                currentWeapon = 0;
            }
        }
        else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && hasWeapon[WEAPON_WALL])
        {
            currentWeapon++;
            if (currentWeapon > 3)
            {
                currentWeapon = 0;
            }
        }
        else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE])
        {
            currentWeapon++;
            if (currentWeapon > 2)
            {
                currentWeapon = 0;
            }
        }
        else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI])
        {
            if (currentWeapon == WEAPON_PISTOL)
            {
                currentWeapon = WEAPON_UZI;
            }
            else
            {
                currentWeapon = WEAPON_PISTOL;
            }
        }
        print("Current Weapon: " + weaponName[currentWeapon] + "(" + ammos[currentWeapon] + ")");
    }
    void Shoot()
    {
        if (currentWeapon == WEAPON_PISTOL && ammos[currentWeapon] >= 1 && canShoot)
        {
            audioS.clip = gunShoot;
            audioS.Play();

            ammos[currentWeapon]--;

            if (hit.collider.transform.position != null)
            {
                DrawLine(firePoint.position, hit.collider.transform.position, 0.15f);
            }
            if (hit.collider.gameObject.tag == "orc")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageOrcHealth>().GotHit(dame[WEAPON_PISTOL]);
            }
            if (hit.collider.gameObject.tag == "boss")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageBossHealth>().GotHit(dame[WEAPON_PISTOL]);
            }
            if (hit.collider.gameObject.tag == "gas_tank")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageGasTankHealth>().GotHit(dame[WEAPON_PISTOL]);
            }
            if (hit.collider.gameObject.tag == "weapon_wall")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageWallHealth>().GotHit(dame[WEAPON_PISTOL]);
            }

            canShoot = false;
            timer = 0.0f;
            timerStarted = true;
        }
        if (currentWeapon == WEAPON_UZI && ammos[currentWeapon] >= 1 && canShoot)
        {
            audioS.clip = gunShoot;
            audioS.Play();

            ammos[currentWeapon]--;

            if (hit.collider.transform.position != null)
            {
                DrawLine(firePoint.position, hit.collider.transform.position, 0.15f);
            }
            if (hit.collider.gameObject.tag == "orc")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageOrcHealth>().GotHit(dame[WEAPON_UZI]);
            }
            if (hit.collider.gameObject.tag == "boss")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageBossHealth>().GotHit(dame[WEAPON_UZI]);
            }
            if (hit.collider.gameObject.tag == "gas_tank")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageGasTankHealth>().GotHit(dame[WEAPON_UZI]);
            }
            if (hit.collider.gameObject.tag == "weapon_wall")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageWallHealth>().GotHit(dame[WEAPON_UZI]);
            }

            canShoot = false;
            timer = 0.0f;
            timerStarted = true;
        }
        if (currentWeapon == WEAPON_ROCKET && ammos[currentWeapon] >= 1 && canShoot)
        {
            audioS.clip = rocketShoot;
            audioS.Play();

            ammos[currentWeapon]--;

            if (hit.collider.transform.position != null)
            {
                DrawLine(firePoint.position, hit.collider.transform.position, 0.15f);
            }
            if (hit.collider.gameObject.tag == "orc")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageOrcHealth>().GotHit(dame[WEAPON_ROCKET]);
            }
            if (hit.collider.gameObject.tag == "boss")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageBossHealth>().GotHit(dame[WEAPON_ROCKET]);
            }
            if (hit.collider.gameObject.tag == "gas_tank")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageGasTankHealth>().GotHit(dame[WEAPON_ROCKET]);
            }
            if (hit.collider.gameObject.tag == "weapon_wall")
            {
                objectTarget = hit.collider.gameObject;
                objectTarget.GetComponent<ManageWallHealth>().GotHit(dame[WEAPON_ROCKET]);
            }
            Instantiate(rocketExplo, hit.collider.transform.position, Quaternion.identity);

            audioS.clip = explo;
            audioS.Play();

            canShoot = false;
            timer = 0.0f;
            timerStarted = true;
        }
    }
    void Throw()
    {
        if (currentWeapon == WEAPON_GRENADE && ammos[currentWeapon] >= 1 && canThrow)
        {
            ammos[currentWeapon]--;
            GameObject grenadeF = (GameObject)Instantiate(grenade, firePoint.transform.position, Quaternion.identity);
            grenadeF.GetComponent<Rigidbody>().AddForce(firePoint.transform.forward * 100);
            grenadeF.GetComponent<Grenade>().dame = dame[WEAPON_GRENADE];
            audioS.clip = explo;
            audioS.PlayDelayed(2f);

            canThrow = false;
            timer = 0.0f;
            timerStarted = true;
        }
    }
    void Set()
    {
        if (currentWeapon == WEAPON_WALL && ammos[currentWeapon] >= 1 && canSet)
        {
            ammos[currentWeapon]--;

            Vector3 t = new Vector3(firePoint.position.x, firePoint.position.y - 0.5f, firePoint.position.z);
            Instantiate(wall, t, Quaternion.identity);
            
            canSet = false;
            timer = 0.0f;
            timerStarted = true;
        }
        if (currentWeapon == WEAPON_GAS_TANK && ammos[currentWeapon] >= 1 && canSet)
        {
            ammos[currentWeapon]--;

            Instantiate(gasTank, firePoint.transform.position, Quaternion.identity);

            canSet = false;
            timer = 0.0f;
            timerStarted = true;
        }
        if (currentWeapon == WEAPON_MINE && ammos[currentWeapon] >= 1 && canSet)
        {
            ammos[currentWeapon]--;

            Vector3 t = new Vector3(firePoint.position.x, firePoint.position.y - 0.5f, firePoint.position.z);
            GameObject mineF = (GameObject)Instantiate(mine, t, Quaternion.identity);
            mineF.GetComponent<Mine>().dame = dame[WEAPON_MINE];
            audioS.clip = mineExplo;
            audioS.PlayDelayed(10f);

            canSet = false;
            timer = 0.0f;
            timerStarted = true;
        }
    }
    void SetRay(int distance)
    {
        Vector3 startRay = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rayFromPlayer = new Ray(startRay, new Vector3(0, 0, player.transform.position.z + distance));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rayFromPlayer = new Ray(startRay, new Vector3(0, 0, player.transform.position.z - distance));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rayFromPlayer = new Ray(startRay, new Vector3(player.transform.position.x - distance, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rayFromPlayer = new Ray(startRay, new Vector3(player.transform.position.x + distance, 0, 0));
        }
        Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * distance, Color.red);
    }
    void DrawLine(Vector3 start, Vector3 end, float duration)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;

        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ammo_pack")
        {
            if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && hasWeapon[WEAPON_WALL] && hasWeapon[WEAPON_GAS_TANK] && hasWeapon[WEAPON_MINE] && !hasWeapon[WEAPON_ROCKET])
            {
                hasWeapon[WEAPON_ROCKET] = true;
                ammos[WEAPON_PISTOL] += 50;
                ammos[WEAPON_UZI] += 100;
                ammos[WEAPON_GRENADE] += 5;
                ammos[WEAPON_WALL] += 5;
                ammos[WEAPON_GAS_TANK] += 5;
                ammos[WEAPON_MINE] += 5;
                ammos[WEAPON_ROCKET] += 5;
            }
            else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && hasWeapon[WEAPON_WALL] && hasWeapon[WEAPON_GAS_TANK] && !hasWeapon[WEAPON_MINE] && !hasWeapon[WEAPON_ROCKET])
            {
                hasWeapon[WEAPON_MINE] = true;
                ammos[WEAPON_PISTOL] += 50;
                ammos[WEAPON_UZI] += 100;
                ammos[WEAPON_GRENADE] += 5;
                ammos[WEAPON_WALL] += 5;
                ammos[WEAPON_GAS_TANK] += 5;
                ammos[WEAPON_MINE] += 5;
            }
            else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && hasWeapon[WEAPON_WALL] && !hasWeapon[WEAPON_GAS_TANK] && !hasWeapon[WEAPON_MINE] && !hasWeapon[WEAPON_ROCKET])
            {
                hasWeapon[WEAPON_GAS_TANK] = true;
                ammos[WEAPON_PISTOL] += 50;
                ammos[WEAPON_UZI] += 100;
                ammos[WEAPON_GRENADE] += 5;
                ammos[WEAPON_WALL] += 5;
                ammos[WEAPON_GAS_TANK] += 5;
            }
            else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && hasWeapon[WEAPON_GRENADE] && !hasWeapon[WEAPON_WALL] && !hasWeapon[WEAPON_GAS_TANK] && !hasWeapon[WEAPON_MINE] && !hasWeapon[WEAPON_ROCKET])
            {
                hasWeapon[WEAPON_WALL] = true;
                ammos[WEAPON_PISTOL] += 50;
                ammos[WEAPON_UZI] += 100;
                ammos[WEAPON_GRENADE] += 5;
                ammos[WEAPON_WALL] += 5;
            }
            else if (hasWeapon[WEAPON_PISTOL] && hasWeapon[WEAPON_UZI] && !hasWeapon[WEAPON_GRENADE] && !hasWeapon[WEAPON_WALL] && !hasWeapon[WEAPON_GAS_TANK] && !hasWeapon[WEAPON_MINE] && !hasWeapon[WEAPON_ROCKET])
            {
                hasWeapon[WEAPON_GRENADE] = true;
                ammos[WEAPON_PISTOL] += 50;
                ammos[WEAPON_UZI] += 100;
                ammos[WEAPON_GRENADE] += 5;
            }
            else if (hasWeapon[WEAPON_PISTOL] && !hasWeapon[WEAPON_UZI] && !hasWeapon[WEAPON_GRENADE] && !hasWeapon[WEAPON_WALL] && !hasWeapon[WEAPON_GAS_TANK] && !hasWeapon[WEAPON_MINE] && !hasWeapon[WEAPON_ROCKET])
            {
                hasWeapon[WEAPON_UZI] = true;
                ammos[WEAPON_PISTOL] += 50;
                ammos[WEAPON_UZI] += 100;
            }
            if (ammos[WEAPON_PISTOL] > maxAmmos[WEAPON_PISTOL])
                ammos[WEAPON_PISTOL] = maxAmmos[WEAPON_PISTOL];
            if (ammos[WEAPON_UZI] > maxAmmos[WEAPON_UZI])
                ammos[WEAPON_UZI] = maxAmmos[WEAPON_UZI];
            if (ammos[WEAPON_GRENADE] > maxAmmos[WEAPON_GRENADE])
                ammos[WEAPON_GRENADE] = maxAmmos[WEAPON_GRENADE];
            if (ammos[WEAPON_WALL] > maxAmmos[WEAPON_WALL])
                ammos[WEAPON_WALL] = maxAmmos[WEAPON_WALL];
            if (ammos[WEAPON_GAS_TANK] > maxAmmos[WEAPON_GAS_TANK])
                ammos[WEAPON_GAS_TANK] = maxAmmos[WEAPON_GAS_TANK];
            if (ammos[WEAPON_MINE] > maxAmmos[WEAPON_MINE])
                ammos[WEAPON_MINE] = maxAmmos[WEAPON_MINE];
            if (ammos[WEAPON_ROCKET] > maxAmmos[WEAPON_ROCKET])
                ammos[WEAPON_ROCKET] = maxAmmos[WEAPON_ROCKET];
            Destroy(other.gameObject);
        }
    }
}
