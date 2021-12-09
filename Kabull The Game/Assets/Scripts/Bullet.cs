using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Objects
    public Rigidbody2D rb;

    //Variables
    public float bulletDmg =20f;
    public bool reversed = false;
    public float speed = 8f;
    public int timeToDisappear = 10;
    
    void Start()
    {
        if (!reversed)
        {
            speed = speed * -1;
        }
        else
        {
            speed = speed * 1;
        }
        
        rb.velocity = transform.right * speed;
        StartCoroutine(DisappearOverTime(gameObject, timeToDisappear));
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        Enemy enemy = other.GetComponent<Enemy>();
        
        if (enemy != null)
        {
            enemy.EnemyTakeDmg(bulletDmg);
            Destroy(gameObject);
        }
        
        if (player != null && !gameObject.CompareTag("Friend"))
        {
            player.PlayerTakeDmg(bulletDmg);
            Destroy(gameObject);
        }
    }
    
    public static IEnumerator DisappearOverTime(GameObject bullet, int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
