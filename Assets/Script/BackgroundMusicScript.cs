using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      /**  GameObject backgroundMusicPlayer = GameObject.Find("MusicContainer");
        AudioSource[] backgroundMusic = GetComponents<AudioSource>();
        Debug.Log(backgroundMusic[0].playOnAwake);
        backgroundMusic[0].playOnAwake = false;

        DontDestroyOnLoad(backgroundMusicPlayer); **/
    }

    private static BackgroundMusicScript _instance;

    public static BackgroundMusicScript instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<BackgroundMusicScript>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public void Play()
    {
        //Play some audio!
    }
}
