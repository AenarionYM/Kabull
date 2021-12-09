using UnityEngine;

public class Melee : MonoBehaviour
{
    //Objects
    public GameObject sword;
    public Animator animator;
    public AudioSource swordSource;
    public AudioClip slash;
    
    //Variables
    public static bool attacking = false;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    void Start()
    {
        sword.SetActive(false);
    }

    void Update()
    {
        AttackCheck();
        animator.SetBool(IsAttacking, attacking);
    }

    void AttackCheck()
    {
        if (Input.GetButtonDown("Melee") && attacking == false && CharacterController.canMove)
        {
            swordSource.clip = slash;
            sword.SetActive(true);
            attacking = true;
            swordSource.Play();
        }
    }
}
