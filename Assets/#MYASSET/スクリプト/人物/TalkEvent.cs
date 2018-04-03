using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TalkEvent : MonoBehaviour {

    AudioSource[] talkAudio;
    //List<AudioSource> playerAudioList = new List<AudioSource>();

    // Use this for initialization
    void Start() {
        talkAudio = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (/*トリガー引いたら*/) {
            //    talkAudio[0].PlayOneShot(talkAudio[0].clip);
            //}
        }
    }
    //private void TriggerPressedHandler(object sender, ControllerInteractionEventArgs e)
    //{
    //    Debug.Log("押した");
    //}
}
