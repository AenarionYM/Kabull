using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Objects
    public AudioSource hurtSound;
    
    //Variables
    public float enemyHp = 100f;

    public void EnemyTakeDmg(float dmg)
    {
        enemyHp -= dmg;

        if (enemyHp <= 0)
        {
            hurtSound.Play();
            
            CapsuleCollider2D[] colliders = gameObject.GetComponents<CapsuleCollider2D>();
            Rigidbody2D rbody = gameObject.GetComponent<Rigidbody2D>();
            
            if (gameObject.CompareTag("Bielmar") || gameObject.CompareTag("Bielmar_Spotted"))
            {
                GetComponent<WeaponBielmar>().enabled = false;
            }
            
            //Disable colliders && jump
            foreach (var coll in colliders)
            {
                coll.enabled = false;
            }
            
            rbody.velocity += Vector2.up * 4;
            
            /*if (gameObject.CompareTag("Enemy") && gameObject.name == "LightBandit")
            {
                
            }*/
            Destroy(gameObject, 4);
        }
    }
}
