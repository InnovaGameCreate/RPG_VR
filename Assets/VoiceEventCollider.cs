using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceEventCollider : MonoBehaviour
{


    private VoiceEvent voiceEvent;

    // Use this for initialization
    void Start()
    {
        voiceEvent = GameObject.Find("VoiceEvent").GetComponent<VoiceEvent>();
        Debug.Log(voiceEvent);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player"))
        {
            Debug.Log("あたったぞ２");
            switch (voiceEvent.nowVoiceNo)
            {
                case VoiceEvent.VoiceKind.T_B_ELFY:
                    Debug.Log("あたったぞ3");
                    voiceEvent.isEventHappen = true;
                    break;
                case VoiceEvent.VoiceKind.T_A_THOMAS:
                    voiceEvent.isEventHappen = true;
                    break;
            }

        }

    }

   
}
