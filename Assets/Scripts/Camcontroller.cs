using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
public class Camcontroller : MonoBehaviour
{
  
    public Transform FollowTarget;
    public GameObject Player;
    float mousex;
    float mousey;
 
    float xrot = 0;

    public float cam_position=8f;

    private void Start()
    {
        
       // Cursor.lockState = CursorLockMode.Locked;
       // cam = Camera.main;
      
    }
    private void Update()
    {
       foreach(Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (!EventSystem.current.IsPointerOverGameObject(id))
            {
                if (Screen.width / 2 < touch.position.x&& Player.GetComponent<PlayerAbility>().dropprojectile == false)
                {
                    mousex += /*Input.GetAxis("Mouse X") * 80*/touch.deltaPosition.x * 10 * Time.deltaTime;
                    mousey -= /*Input.GetAxis("Mouse Y") * 80 */touch.deltaPosition.y * 10 * Time.deltaTime;
                }
                
            }
        }

       



        if (Player.GetComponent<PlayerAbility>().dropprojectile == false)
        {
            //mousex += Input.GetAxis("Mouse X") * 80 * 10 * Time.deltaTime;
            //mousey -= Input.GetAxis("Mouse Y") * 80 * 10 * Time.deltaTime;
            xrot = mousey;
            xrot = Mathf.Clamp(xrot, -13f, 30f);
            Vector3 ver = new Vector3(xrot, mousex);
            transform.eulerAngles = ver;
            transform.position = FollowTarget.position - transform.forward * cam_position;
        }

    }
 
}
