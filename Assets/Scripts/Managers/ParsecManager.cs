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

        streamer = GetComponent<ParsecStreamFull>();
        if(streamer)
        {
            streamer.GuestConnected += StreamerGuestConnected;
            streamer.GuestDisconnected += StreamerGuestDisconnected;
        }
    }

    private void StreamerGuestConnected(object sender, Parsec.ParsecGuest guest)
    {

    }

    private void StreamerGuestDisconnected(object sender, Parsec.ParsecGuest guest)
    {

    }
}
