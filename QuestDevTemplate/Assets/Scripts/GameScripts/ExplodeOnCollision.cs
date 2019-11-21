using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Static" || collision.gameObject.tag == "Target")
        {
            GameObject newExplosion = GameObject.Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(newExplosion, 3f);
        }       
    }
}
