using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Private field
    private AudioSource Hammersound;
    #endregion

    #region public field
    public AudioClip[] sound;
    public static SoundManager instance = null;
    [HideInInspector] public bool Iswalking;
    #endregion
    // Start is called before the first frame update
    #region MonoBehaviour callback
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Hammersound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    #endregion

    #region Public method
    public void SoundPlay(int i)
    {
        Hammersound.clip = sound[i];
        switch (i)
        {
            case 1:
                Hammersound.volume = .5f;
                break;
            //case 2:
            //    Hammersound.loop = true;
            //    break;
            default:
                Hammersound.volume = 1;
                
                break;
        }
        //if (i == 1)
        //{

        //}
        //else
        //{
        //    Hammersound.volume = 1f;
        //}
        if (!Hammersound.isPlaying)
        {
            Hammersound.Play();
        }
      
    }
    #endregion
}
