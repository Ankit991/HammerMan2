using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemy : MonoBehaviour
{
    public static SpawningEnemy instance;
    #region public field
    public GameObject Enemy;
     public int totalenemy;
    public int spawningturn,levelupturn=-2;

    #endregion
    #region private field

    float zmin = 30;
    float zmax = 80;
    #endregion
    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != null)
        {
            Destroy(instance);
        }
    }
    #endregion

    private void Start()
    {
       
    }
    private void Update()
    {
        if (totalenemy <= 0)
        {
            StartCoroutine(SpawnEnemy());

        }
    }
    IEnumerator SpawnEnemy()
    {
        totalenemy = 20;
        spawningturn++;
        levelupturn++;
       
        if (spawningturn - 2 == levelupturn && levelupturn >= 0)
        {
            Debug.Log("call");
            zmin += 90;
            zmax += 80;
        }
        yield return new WaitForSeconds(1f);
       
        for (int i = 0; i < 20; i++)
        {
            float x = Random.Range(-12, 42);
            float z = Random.Range(zmin, zmax);
            Instantiate(Enemy, new Vector3(x, 0, z), Enemy.transform.rotation);
          
        }
       
    }

}
