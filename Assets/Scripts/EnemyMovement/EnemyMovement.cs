using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public  enum Enemytype {
    FiretypeEnemy,HittypeEnemy
}
public class EnemyMovement : MonoBehaviour
{
    #region Public Field
   
    public Enemytype Enemy;
    public int Walkingforce;
    
    #endregion

    #region Private Field
    private Rigidbody Rb;
    private Animator anim;
    public bool AttackRandom;
    #endregion

    #region monobihaviour method
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        InvokeRepeating("ChangeAttack", .1f, 4f);
    }
    public void FixedUpdate()
    {
        PlayerMovement();
        LookTowardPLayer();
    }
    #endregion

    #region Private Method
    private void LookTowardPLayer()
    {
       
            Vector3 dir = Gamemanager.instance.player.transform.position - this.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(this.transform.rotation, rot, 10 * Time.deltaTime);
     
    }
    private void PlayerMovement()
    {
        float Distancefromplayer = Vector3.Distance(this.transform.position, Gamemanager.instance.player.transform.position);
        
        if (Distancefromplayer < 2)
        {
            Debug.Log("ToClose to the plater");
          
            if (AttackRandom)
            {
                anim.SetTrigger("Attack");

            }
            else
            {
                anim.SetTrigger("Attack1");
            }
            anim.SetBool("Walk", false);


        }else
        {
            //adding relative force to moveEnemy
            anim.SetBool("Walk", true);
            Rb.AddRelativeForce(Vector3.forward * Walkingforce, ForceMode.Force);
        }
    }
    #endregion

    #region public method
    public void ChangeAttack()
    {
        AttackRandom = !AttackRandom;
    }
    #endregion


    // Start is called before the first frame update

}
