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
    public Transform Player;
    public Enemytype Enemy;
    public int Walkingforce;
    #endregion

    #region Private Field
    private Rigidbody Rb;
    #endregion

    #region monobihaviour method
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
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
        Vector3 dir = Player.transform.position - this.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(this.transform.rotation, rot, 10 * Time.deltaTime);
    }
    private void PlayerMovement()
    {
        float Distancefromplayer = Vector3.Distance(this.transform.position, Player.transform.position);
        if (Distancefromplayer < 1)
        {
            Debug.Log("ToClose to the plater");

            //calling Hit animation 

        }else
        {   
            //adding relative force to moveEnemy
            Rb.AddRelativeForce(Vector3.forward * Walkingforce, ForceMode.Force);
        }
    }
    #endregion

    #region public method
    #endregion

    // Start is called before the first frame update

}
