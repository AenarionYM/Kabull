using System.Collections.Generic;
using UnityEngine;

public class Bielmar : MonoBehaviour
{
    //Objects
    private Transform _firePoint1;
    private Transform _firePoint2;
    private Transform _firePoint3;
    private Transform _firePoint4;
        
    //Variables
    public static bool seesPlayer;
    public float detectionRange = 10f;
    public int attackDelay = 4;

        
    private void Awake()
    {
        Transform[] firePoints = GetComponentsInChildren<Transform>();
        _firePoint1 = firePoints[1];
        _firePoint2 = firePoints[2];
        _firePoint3 = firePoints[3];
        _firePoint4 = firePoints[4];
    }

    private void FixedUpdate()
    {
        List<RaycastHit2D> hits = KabullCheck();

        if (seesPlayer)
        {
            seesPlayer = false; // co to?
        }
            
        if (hits.Count != 0 && !WeaponBielmar.coolDown)
        {
            /*foreach (RaycastHit2D hit in hits)
            {
                Debug.Log($"{hit.collider.name} ||| {hit.distance}");
            }*/
            seesPlayer = true;
        }
    }

    private List<RaycastHit2D> KabullCheck()
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        var position = _firePoint1.position;
        var position1 = _firePoint2.position;
        var position2 = _firePoint3.position;
        var position3 = _firePoint4.position;
            
        RaycastHit2D hit1 = Physics2D.Raycast(position, Vector2.right, detectionRange);
        RaycastHit2D hit2 = Physics2D.Raycast(position1, Vector2.right, detectionRange);
        RaycastHit2D hit3 = Physics2D.Raycast(position2, Vector2.left, detectionRange);
        RaycastHit2D hit4 = Physics2D.Raycast(position3, Vector2.left, detectionRange);
            
            
        Debug.DrawRay(position, Vector2.right * detectionRange, Color.red);
        Debug.DrawRay(position1, Vector2.right * detectionRange, Color.red);
        Debug.DrawRay(position2, Vector2.left * detectionRange, Color.red);
        Debug.DrawRay(position3, Vector2.left * detectionRange, Color.red);

        if (hit1.collider != null && hit1.collider.CompareTag("Player")){hits.Add(hit1);}
        if (hit2.collider != null && hit2.collider.CompareTag("Player")){hits.Add(hit2);}
        if (hit3.collider != null && hit3.collider.CompareTag("Player")){hits.Add(hit3);}
        if (hit4.collider != null && hit4.collider.CompareTag("Player")){hits.Add(hit4);}

        return hits;
    }
}