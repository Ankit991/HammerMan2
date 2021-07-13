using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{

    public GameObject ExplosiveParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explosive")
        {
            Destroy(collision.gameObject);
            GameObject explosion= Instantiate(ExplosiveParticle, collision.transform.position, Quaternion.Euler(0, 90, 0f));
            Destroy(explosion, 2f);
        }
        if (collision.gameObject.tag == "Enemy")
        {
               Debug.Log(collision.gameObject.name);
               collision.gameObject.GetComponent<EnemyDeath>().TurnOnRagdoll();
           
        }
       
    }
}
