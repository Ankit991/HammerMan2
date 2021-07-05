using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public static UI instance = null;
    public Image circularwaverefill;
    public Image projectilerefill;
    public Image Throwhammerrefill;

   
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(instance);
        }
    }

   
  public  IEnumerator circularwaverefilling()
    {
        while (circularwaverefill.fillAmount<.99f)
        {
            circularwaverefill.fillAmount += .02f;
            yield return new WaitForSeconds(.01f);
        }
      
    }
    public IEnumerator projectilewaverefilling()
    {
        while (projectilerefill.fillAmount < .99f)
        {
            projectilerefill.fillAmount += .02f;
            yield return new WaitForSeconds(.01f);
        }

    }
    public IEnumerator ThrowHammerfilling()
    {
        while (Throwhammerrefill.fillAmount < .99f)
        {
            Throwhammerrefill.fillAmount += .02f;
            yield return new WaitForSeconds(.01f);
        }

    }
}
