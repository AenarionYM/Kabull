using UnityEngine;

public class BulletBielmar : MonoBehaviour
{
    //Objects
    public Rigidbody2D rb;

    //Variables
    public bool reversed = false;
    public float bulletDmg = 50f;
    public float speed = 10f;
    public int timeToDisappear = 10;


    private void Start()
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
        StartCoroutine(Bullet.DisappearOverTime(gameObject, timeToDisappear));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.PlayerTakeDmg(bulletDmg);
            Destroy(gameObject);
        }
    }

}
