using System.Collections;
using UnityEngine;

public class WeaponBielmar : MonoBehaviour
{
    //Objects
    private Transform _firePoint1;
    private Transform _firePoint2;
    private Transform _firePoint3;
    private Transform _firePoint4;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabReversed;
    public Animator animator;
    public new SpriteRenderer renderer;
    //private AudioSource bielmarShoot;

    //Variables
    public int attackDelay = 4;
    public float health = 1000f;
    public float damage = 20f;
    public static bool coolDown = false;
    private static readonly int IsShooting = Animator.StringToHash("isShooting");

    private void Awake()
    {
        Transform[] firePoints = GetComponentsInChildren<Transform>();
        _firePoint1 = firePoints[1];
        _firePoint2 = firePoints[2];
        _firePoint3 = firePoints[3];
        _firePoint4 = firePoints[4];
    }

    void FixedUpdate()
    {
        if (Bielmar.seesPlayer)
        {
            StartCoroutine(Attack(attackDelay));
        }
    }
    
    IEnumerator Attack(int delayTimeSec)
    {
        //Delay przed atakiem i cooldown
        coolDown = true;
        yield return new WaitForSeconds(1);
        
        //Pierwsza salwa
        animator.SetBool(IsShooting, true);
        Shoot();
        
        yield return new WaitForSeconds(delayTimeSec / 2f);
        //Druga
        Shoot();

        yield return new WaitForSeconds(delayTimeSec / 2f);
        //Koniec collowego downa
        coolDown = false;
        animator.SetBool(IsShooting, false);
    }

    void Shoot()
    {
        if (gameObject.GetComponent<WeaponBielmar>().enabled)
        {
            Instantiate(bulletPrefabReversed, _firePoint1.position, _firePoint1.rotation);
        
            Instantiate(bulletPrefabReversed, _firePoint2.position, _firePoint2.rotation);
        
            Instantiate(bulletPrefab, _firePoint3.position, _firePoint3.rotation);
        
            Instantiate(bulletPrefab, _firePoint4.position, _firePoint4.rotation);
        }
    }
}