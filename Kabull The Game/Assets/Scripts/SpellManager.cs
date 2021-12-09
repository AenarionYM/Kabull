using System.Collections;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    //Objects
    private Transform _firePoint;
    public AudioSource audioSource;
    
    //Slots
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    private GameObject _currentSlot;
    
    //Spells Prefabs
    public GameObject poczetPrefab;
    public GameObject fatPrefab;
    public GameObject sosnaPrefab;
    
    //Spells Sounds
    public AudioClip poczetSound;
    public AudioClip fatSound;
    //public AudioClip sosnaSound;
    
    //Spells Variables
    public float poczetCost = 20f;
    public float fatCost = 45f;
    public float bitRideSpeed = 1.30f; 
    public int bitRideTime = 3;
    public float sosnaCost = 50f;

    private void Awake()
    {
        _firePoint = transform.Find("Weapon_Głowa");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Spell1") && CharacterController.canMove)
        {
            SlotChecker(1);
        }

        if (Input.GetButtonDown("Spell2") && CharacterController.canMove)
        {
            SlotChecker(2);
        }
        
        if (Input.GetButtonDown("Spell3") && CharacterController.canMove)
        {
            StartCoroutine(BitsRide());
        }
    }

    void SlotChecker(int slot)
    {
        switch (slot)
        {
            case 1:
                _currentSlot = slot1;
                break;
            case 2:
                _currentSlot = slot2;
                break;
            case 3:
                _currentSlot = slot3;
                break;
        }

        if (_currentSlot.name == "poczet" && Player.freestyle >= poczetCost)
        {
            Player.freestyle -= poczetCost;
            PoczetShoot();
        }

        if (_currentSlot.name == "fat" && Player.freestyle >= fatCost)
        {
            Player.freestyle -= fatCost;
            //FatShoot();
            SosnaShoot();
        }
    }

    void PoczetShoot()
    {
        audioSource.clip = poczetSound;
        audioSource.Play();
        Instantiate(poczetPrefab, _firePoint.position, _firePoint.rotation);
    }
    
    void FatShoot()
    {
        audioSource.clip = fatSound;
        audioSource.Play();
        Instantiate(fatPrefab, _firePoint.position, _firePoint.rotation);
    }

    void SosnaShoot()
    {
        //_audioSource.clip = sosnaSound();
        //_audioSource.Play();
        Instantiate(sosnaPrefab, _firePoint.position, _firePoint.rotation);
    }
    
    private IEnumerator BitsRide()
    {
        float sis = CharacterController.movementSpeed;
        CharacterController.movementSpeed = CharacterController.movementSpeed * bitRideSpeed;
        yield return new WaitForSeconds(bitRideTime);
        CharacterController.movementSpeed = sis;
    }
}
