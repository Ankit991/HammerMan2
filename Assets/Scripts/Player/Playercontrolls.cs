using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrolls : MonoBehaviour
{
    float hor, ver;
    private Animator anim;
    private Rigidbody rb;
    public Joystick joystick;
    public int WalkingSpeed;
    public LayerMask GroundLayer;
    public Transform GroundCheck;
    public Camera cam;
    [SerializeField] private bool iswalking = false;
    [SerializeField] bool isground = true;
    private AudioSource audiosourse;
   

    //script Reference
    PlayerAbility playerability;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audiosourse = GetComponent<AudioSource>();
        playerability = GetComponent<PlayerAbility>();
    }
    private void FixedUpdate()
    {
        PcInputPlayerMovement();
        if (playerability.HamReturned)
        {
            TouchinputMovement();
        }
     
    }
    // Update is called once per frame
    void Update()
    {
        CastRay();
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    changePlayer_byvalue++;
        //    if (changePlayer_byvalue > ChangePlayer.Length)
        //    {
        //        changePlayer_byvalue = 0;
        //    }
        //    for (int i = 0; i < ChangePlayer.Length; i++)
        //    {

        //        if (changePlayer_byvalue == i)
        //        {
        //            ChangePlayer[i].SetActive(true);
        //        }
        //        else if (changePlayer_byvalue != i)
        //        {
        //            ChangePlayer[i].SetActive(false);
        //        }
        //    }
      //  }  //for Changing the Character in Gamescene
           // RotatePlayer();
           //if (Input.GetMouseButtonUp(0))
           //{
           //    anim.SetTrigger("Throw");

        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    ReturnHammer();
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        anim.SetBool("IsGround", isground);

    }
    void CastRay()
    {

        RaycastHit hit;
        if (Physics.Raycast(GroundCheck.position, transform.TransformDirection(Vector3.down), out hit, 1f, GroundLayer))
        {

            isground = true;
            // Throwpos.transform.LookAt(hit.point);
            //  transform.LookAt(hit.point);
            //  disBTw = Vector3.Distance(hammer.position, hit.point);
            // hitdistance = Vector3.Distance(Throwpos.position, hit.point);
        }
        else
        {

            isground = false;
        }

        //  Debug.DrawRay(Camera.main.transform.position, hit.collider.transform.position, Color.red);


    }
    private void PcInputPlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        if ((Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Run", true);
            WalkingSpeed = 250;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Run", false);
            WalkingSpeed = 150;
        }
        anim.SetFloat("x", hor);
        anim.SetFloat("y", ver);
        Vector3 movement = transform.forward * ver + transform.right * hor;
        rb.AddForce(movement * WalkingSpeed);
        // transform.Translate(movement * WalkingSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            iswalking = true;
        }
        else
        {
            iswalking = false;
        }
        if (iswalking && !audiosourse.isPlaying)
        {
            audiosourse.Play();
        }
        else if (!iswalking)
        {
            audiosourse.Stop();
        }
    }
    private void TouchinputMovement()
    {
       

        if (!playerability.dropprojectile)
        {
           hor = joystick.Horizontal;
           ver = joystick.Vertical;
            anim.SetFloat("x", hor);
            anim.SetFloat("y", ver);
            Vector3 movement = transform.forward * ver + transform.right * hor/*new Vector3(hor, 0, ver)*/;
            rb.AddForce(movement * WalkingSpeed);
            float angle = cam.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), 360 * Time.deltaTime);
        }

        if (hor > 0 || ver > 0)
        {
            //  float dirangle = Mathf.Atan2(hor, ver) * Mathf.Rad2Deg;
            //Quaternion rot = Quaternion.Euler(0, dirangle, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 360 * Time.deltaTime);
            iswalking = true;
        }
        else
        {
            iswalking = false;
        }
    }
    public void Jump()
    {
        if (isground & !iswalking)
        {
            rb.velocity = Vector3.up * 8;
            anim.SetTrigger("Jump");
            isground = false;
        }
        else if (isground && iswalking)
        {
            rb.velocity = Vector3.up * 8;
            anim.SetTrigger("RunJump");
            isground = false;
        }

    }
}
