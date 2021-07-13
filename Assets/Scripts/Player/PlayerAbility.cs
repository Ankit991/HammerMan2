using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerAbility : MonoBehaviour
{
    #region  Private variable
    private Animator anim;
    private Vector3 oldpos;
    private float time;
    bool checking = true;
    bool throwing = true;

    #endregion

    #region public variable

    public GameObject joystick;
    public GameObject HammerReal, Hammerfake;
    public GameObject Hammer_trail;
    public ParticleSystem HammerRecieve;
    public ParticleSystem HammerRecieve_lastsec;

    public Rigidbody hammer;
    public BoxCollider hammer_collider;
    public float throwpower;

    public bool hReturning;
    public Transform target, curvepoint;
    public Transform hammerParent;
    public Transform Throwpos;
  [HideInInspector]public bool HamReturned = true;

    [HideInInspector] public Vector3 HammerhitPoint;
   // [HideInInspector] public bool whenwin;

    public GameObject[] ChangePlayer;
    public int changePlayer_byvalue;
    public float hitdistance; //-- hit distance to calculate the distance btw hit point and player;
   
     
    public  Camera cam;
    public GameObject playerinsideCam;
    #region thorwave effect variable
    public float WaveEffectRadius;
    public int waveeffectpower;
    public ParticleSystem thorwaveparticle;
    public bool refillCircularWavedone = true;
    #endregion

    #region dropProjectile
    public bool dropprojectile;
    public GameObject projectile;
    public GameObject Blastparticle;
    public bool RefillProjectiledone = true;
    #endregion
    #endregion
    // Start is called before the first frame update
    #region MonoBehaviour callback
    void Start()
    {
       
        Hammer_trail.SetActive(false);
        anim = GetComponent<Animator>();
        projectile.SetActive(false);
        Blastparticle.SetActive(false);

      
        Hammerfake.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GeneratecircularWave();
        }
        if (hReturning)
        {
            if (time < 1f)
            {
                hammer.position = curve(time, oldpos, curvepoint.position, target.position);
                hammer.rotation = Quaternion.Slerp(hammer.transform.rotation, target.rotation, 10 * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {
                resethammer();
            }
        }
    }
    
   
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            changePlayer_byvalue++;
            if (changePlayer_byvalue > ChangePlayer.Length)
            {
                changePlayer_byvalue = 0;
            }
            for(int i = 0; i < ChangePlayer.Length; i++)
            {
                
                if (changePlayer_byvalue == i)
                {
                    ChangePlayer[i].SetActive(true);
                }else if (changePlayer_byvalue != i)
                {
                    ChangePlayer[i].SetActive(false);
                }
            }
        }  //for Changing the Character in Gamescene

      
      
        
            if (Input.GetMouseButton(0) && dropprojectile)
            {
            joystick.SetActive(false);
                DropProjectile();
            }
            else
          if (Input.GetMouseButtonUp(0) && dropprojectile)
            {
                joystick.SetActive(true);
                dropprojectile = false;
                projectileEffect();
                projectile.SetActive(false);

            }

        //if hammer not returning then we set trigger of hammer off;
        //if (!hReturning&&Vector3.Distance(this.transform.position, HammerReal.transform.position) > 4)
        //{
        //    hammer_collider.isTrigger = false;
        //}
        Physics.IgnoreCollision(hammer_collider, GetComponent<Collider>());

    }

    #endregion
   
    #region public method
    public void ThrowHammer()
    {
        hReturning = false;
        HammerReal.SetActive(true);
        Hammerfake.SetActive(false);
        Hammer_trail.SetActive(true);
        checking = false;
       
      
        hammer.transform.parent = null;
      

        hammer.isKinematic = false;

        //hammer.velocity = Throwpos.transform.forward * throwpower*hitdistance;
        hammer.velocity = cam.transform.forward * throwpower;
      // hammer.AddForce(cam.transform.forward * throwpower, ForceMode.Impulse);
        hammer.AddTorque(hammer.transform.TransformDirection(Vector3.up) * 100, ForceMode.Impulse);

    }
    public void ThrowSound()
    {
        SoundManager.instance.SoundPlay(0);
    }
    public void OnPointerUP()
    {
        if (HamReturned)
        {
            HamReturned = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0), 100);
            anim.SetTrigger("Throw");
            StartCoroutine(ReturnHammerBack());
        }
       
    }
    public void OnPointerDown()
    {
        ReturnHammer();
    }
    public void GeneratecircularWave()
    {
        if (refillCircularWavedone)
        {
            playerinsideCam.SetActive(true);
            anim.SetTrigger("Waveattack");
            refillCircularWavedone = false;
           
            StartCoroutine(RefillCircularWavePower());
        }
      
       
    }
    public void DropProjectileButtonEnter()
    {
        dropprojectile = true;
    }
    public void DropProjectile()
    {
        if (RefillProjectiledone)
        {
             Ray ray = cam.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 50))
        {
           // Debug.Log(hit.collider.name+" "+hit.point);
            projectile.SetActive(true);
            Vector3 hitpoint = hit.point;
            hitpoint.y = 1.1f;
            projectile.transform.position = hitpoint;
        }
        Blastparticle.SetActive(false);
        }  
       
    }
    /// <summary>
    /// not working method 
    /// </summary>
    public void projectileEffect()
    {
      
        if (RefillProjectiledone)
        {
            RefillProjectiledone = false;
            Collider playercol = this.GetComponent<Collider>();
            Collider[] col = Physics.OverlapSphere(projectile.transform.position, WaveEffectRadius);
            foreach (Collider hit in col)
            {
               
                    
              
                if (hit != playercol)
                {
                    if (hit.gameObject.tag == "Enemy")
                    {
                        hit.gameObject.GetComponent<EnemyDeath>().TurnOnRagdoll();
                    }

                    Rigidbody hitrb = hit.GetComponent<Rigidbody>();
                    if (hitrb != null)
                    {
                        hitrb.AddExplosionForce(waveeffectpower, projectile.transform.position, WaveEffectRadius, 3.0f, ForceMode.Impulse);
                    }
                }
            }
            Blastparticle.transform.position = projectile.transform.position;
            Blastparticle.SetActive(true);
            StartCoroutine(RefillProjectilePower());
        }
      
       
       
    }
    #endregion
   

    #region private method
   
   
   private void ReturnHammer()
    {
        
        HammerRecieve.Play();
        anim.SetBool("Pull", true);
      //  hammer_collider.isTrigger = true;
        checking = true;
        time = 00f;
        oldpos = hammer.position;
        hReturning = true;
        hammer.velocity = Vector3.zero;
        hammer.isKinematic = true;
       // SoundManager.instance.SoundPlay(1);
    }
   private  void resethammer()
    {
        HammerReal.SetActive(false);
        Hammerfake.SetActive(true);
        StartCoroutine(Particle());
        Hammer_trail.SetActive(false);
        HammerRecieve.Stop();
        anim.SetBool("Pull", false);
        hReturning = false;
        hammer.transform.parent = hammerParent;
        hammer.position = target.position;
        hammer.rotation = target.rotation;
        throwing = true;
        SoundManager.instance.SoundPlay(1);
    }
   private Vector3 curve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
    #endregion

    #region IEnumerator method
    IEnumerator RefillCircularWavePower()
    {
        #region check collider from the radius to apply force and playing waveparticle
        yield return new WaitForSeconds(2);
        Collider playercol = this.GetComponent<Collider>();
        Collider[] col = Physics.OverlapSphere(transform.position, WaveEffectRadius);
        foreach (Collider hit in col)
        {
            if (hit != playercol)
            {
                if (hit.gameObject.tag == "Enemy")
                {
                    hit.gameObject.GetComponent<EnemyDeath>().TurnOnRagdoll();
                }
                Rigidbody hitrb = hit.GetComponent<Rigidbody>();
                if (hitrb != null)
                {
                    hitrb.AddExplosionForce(waveeffectpower, transform.position, WaveEffectRadius, 3.0f, ForceMode.Impulse);
                }
            }
        }
        thorwaveparticle.Play();
        #endregion


        playerinsideCam.SetActive(false);
        yield return new WaitForSeconds(2f);
        UI.instance.circularwaverefill.fillAmount = 0f;
        StartCoroutine(UI.instance.circularwaverefilling());
        refillCircularWavedone = true;
    }
    IEnumerator RefillProjectilePower()
    {
        UI.instance.projectilerefill.fillAmount = 0f;
        StartCoroutine(UI.instance.projectilewaverefilling());
        yield return new WaitForSeconds(2f);
        RefillProjectiledone = true;
    }
   
    IEnumerator Particle()
    {
        yield return new WaitForSeconds(.05f);
        HammerRecieve_lastsec.Play();
    }
    IEnumerator ReturnHammerBack()
    {
        UI.instance.Throwhammerrefill.fillAmount = 0;
        StartCoroutine(UI.instance.ThrowHammerfilling());
      
        yield return new WaitForSeconds(1.5f);
        ReturnHammer();
        yield return new WaitForSeconds(1f);
        HamReturned = true;
    }
    #endregion
}
