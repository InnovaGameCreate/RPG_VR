using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//特にイベントとかないMOBキャラの会話
public class Mob : TalkEvent
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        talkNum = talkAudio.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTalk())
            Talk(Random.Range(0, talkNum + 1));
    }
}
