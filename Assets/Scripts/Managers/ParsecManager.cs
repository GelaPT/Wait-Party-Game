using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParsecGaming;
using ParsecUnity;

public class ParsecManager : Singleton<ParsecManager>
{
    private ParsecStreamGeneral streamer;

    protected override void Awake()
    {
        base.Awake();

        streamer = GameObject.Find("Main Camera").GetComponent<ParsecStreamFull>();
        if(streamer)
        {
            //streamer.
        }
    }
}
