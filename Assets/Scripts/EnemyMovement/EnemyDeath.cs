using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Collider[] colliders;
    public Rigidbody[] rigitbody;
    // Start is called before the first frame update
    void Start()
    {
        //disable rig doll
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
            rigitbody[i].useGravity = false;
        }
       
    }

    public void TurnOnRagdoll()
    {
       // this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Animator>().enabled = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
            rigitbody[i].mass = 1;
            rigitbody[i].useGravity = true;
        }
        if (SpawningEnemy.instance.totalenemy > 0)
        {
            SpawningEnemy.instance.totalenemy--;
        }
        Destroy(this.gameObject, 5f);
    }
}
