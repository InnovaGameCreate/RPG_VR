using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VoiceEvent : MonoBehaviour {

    private AudioSource oberon;
    private AudioSource villageDaugther;
    

    // Use this for initialization
    void Start () {
        oberon = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (IsFinished(oberon))
        {
            //シーンを民家に
            SceneManager.LoadScene("民家");
            
        }

	}

    public bool IsFinished(AudioSource audio)
    {
        return audio.time == 0.0f && !audio.isPlaying;
    }

}
