using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    //Objects
    public TextMeshProUGUI moneyText;
    public Rigidbody2D tb;
    
    //Variables
    public static int money = 0;
    
    private void Awake()
    {
        moneyText.text = money.ToString();
    }

    private void Update()
    {
        moneyText.text = money.ToString();
    }

    private void OnTriggerEnter2D(Collider2D coin)
    {
        if (coin.transform.CompareTag("Coin"))
        {
            money += 1;
            Destroy(coin.gameObject);
            moneyText.text = money.ToString();  
        }
    }
}
