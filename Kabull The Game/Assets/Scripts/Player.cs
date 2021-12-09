using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Player : MonoBehaviour
{
    //Objects
    public GameObject sight;
    private static List<Transform> _sightPoints;
    private static Transform _firePoint;
    public List<AudioClip> enemySpotted;
    public AudioSource audioSource;
    public AudioSource kabullFall;
    public GameObject deathScreen;
    public GameObject heart;
    public Slider hpSlider;
    public Slider freeSlider;
    public Slider expSlider;
    public Gradient gradient;
    public Image fill;
    public Transform playerTransform;
    public LayerMask mainMask;

    // Variables
    public float playerHp = 100f;
    public float freestylResource = 100f;
    static public float freestyle;
    public float playerExp = 10f;
    public static int kabullMoney;

    void Awake()
    {
        _firePoint = transform.Find("Weapon_Głowa");
        _sightPoints = new List<Transform>(sight.GetComponentsInChildren<Transform>());
        freestyle = freestylResource;
        hpSlider.maxValue = playerHp;
        freeSlider.maxValue = freestylResource;
    }

    void Update()
    {
        if (freestyle < freestylResource)
        {
            StartCoroutine(FreestyleRegen());
        }

        if (freestyle > freestylResource)
        {
            freestyle = freestylResource;
        }

        hpSlider.value = playerHp;
        freeSlider.value = freestyle;
        expSlider.value = playerExp;

        fill.color = gradient.Evaluate(hpSlider.normalizedValue);
        DetectObject();
    }

    private void FixedUpdate()
    {
        List<RaycastHit2D> hits = EnemyCheck();
        if (hits.Count != 0)
        {
            Random random = new Random();
            int i = random.Next(0, enemySpotted.Count);
            audioSource.clip = enemySpotted[i];
            audioSource.Play();
            hits[0].collider.gameObject.tag = "Bielmar_Spotted";
        }
    }

    public void Automat()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Random random = new Random();
            int randint = random.Next(0, 1 + 1);
            if (randint == 1 && Money.money > 0 && playerHp != hpSlider.maxValue)
            {
                Money.money--;
                playerHp += 50;
                Debug.Log("Marusz Max Leczonko");
            }
            else if (randint == 0 && Money.money > 0 && playerHp != hpSlider.maxValue)
            {
                Money.money--;
            }
        }
    }

    private IEnumerator FreestyleRegen()
    {
        yield return new WaitForSeconds(1);
        freestyle += 0.1f;
    }

    public void PlayerTakeDmg(float dmg)
    {
        playerHp -= dmg;

        if (playerHp <= 0)
        {
            GameObject kabull = gameObject;
            CharacterController.canMove = false;
            kabull.GetComponent<Rigidbody2D>().freezeRotation = false;
            kabull.tag = "Player_Dead";
            kabullFall.Play();
            deathScreen.SetActive(true);
            heart.SetActive(false);
        }
    }

    public static List<RaycastHit2D> EnemyCheck()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        foreach (Transform sightPoint in _sightPoints)
        {
            RaycastHit2D hit = Physics2D.Linecast(_firePoint.position, sightPoint.position);
            Debug.DrawLine(_firePoint.position, sightPoint.position, Color.green);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Bielmar"))
            {
                hits.Add(hit);
            }
        }

        return hits;
    }

    void DetectObject()
    {
        Collider2D trig = Physics2D.OverlapCircle(playerTransform.position, 0.5f, mainMask);
        if (trig != null)
        {
            string naem = trig.gameObject.name;
            if (naem == "Automat")
            {
                Automat();
            }
        }
    }
}