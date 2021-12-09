using UnityEngine;

public class Sword : MonoBehaviour
{
    //Objects
    public AudioSource swordSource;
    public AudioClip slash;
    public AudioClip slashHit;
    public AudioClip deflect;

    //Variables
    public int deflectForce = 10;
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject gameObj = other.gameObject;
        Enemy enemy = gameObj.GetComponent<Enemy>();
        Bullet bullet = gameObj.GetComponent<Bullet>();

        if (enemy != null)
        {
            swordSource.clip = slashHit;
            swordSource.Play();
            enemy.EnemyTakeDmg(damage);
        }

        if (bullet != null)
        {
            swordSource.clip = deflect;
            swordSource.Play();
            
            Vector3 force = transform.position - other.transform.position;
            force.Normalize();

            Collider2D collider = gameObj.GetComponent<Collider2D>();
            collider.enabled = true;
            collider.tag = "Friend";
            bullet.rb.freezeRotation = false;
            bullet.rb.AddForce(-force * deflectForce * 10);;
            bullet.rb.AddForce(Vector3.up  * deflectForce * 5);;
            bullet.rb.AddTorque(deflectForce * 5);;
            bullet.rb.gravityScale = 1;
        }
    }

    void StopAnimation()
    {
        gameObject.SetActive(false);
        Melee.attacking = false;
    }
}
