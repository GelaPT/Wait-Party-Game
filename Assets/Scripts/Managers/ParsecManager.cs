/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ParsecGaming;

public class ParsecManager : Singleton<ParsecManager>
{
    private ParsecStreamGeneral streamer;
    public List<Player> players = new();
    private ParsecUnity.API.SessionResultDataData authdata;
    private Parsec parsec;

    protected override void Awake()
    {
        base.Awake();

        players.Add(new Player
        {
            id = 0,
            assignedGuest = new Parsec.ParsecGuest(),
            gamepad = Gamepad.current
        });
        streamer = GetComponent<ParsecStreamGeneral>();
        if(streamer)
        {
            streamer.GuestConnected += StreamerGuestConnected;
            streamer.GuestDisconnected += StreamerGuestDisconnected;
            streamer.GetParsecInstance().GamepadButton += GetParsecButton;
        }   
    }

    private static void GetParsecButton(object sender, Parsec.ParsecGuest guest, Parsec.ParsecGamepadButtonMessage button)
    {
        Debug.Log("Foda-se");
    }

    private void Update()
    {
        if (players[0] == null) return;
        if (InputManager.Instance.GetButton(players[0], InputButton.A)) Debug.Log("Host pressed A");
        if (ParsecUnity.ParsecInput.GetMousePosition(2) != Vector3.zero) Debug.Log("Caralhos ta fodam corno de merda!");
    }

    private void StreamerGuestConnected(object sender, Parsec.ParsecGuest guest)
    {
        int player = players.Count;
        if (player > 0 && player <= players.Count)
        {
            players.Add(new Player(player + 1, guest));
        }
    }

    private void StreamerGuestDisconnected(object sender, Parsec.ParsecGuest guest)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if(players[i] != null && players[i].assignedGuest.id == guest.id)
            {
                players.RemoveAt(i);
            }
        }
    }

    public void GetAccessCode()
    {
        ParsecUnity.API.SessionData sessionData = streamer.RequestCodeAndPoll();
        if(sessionData != null && sessionData.data != null)
        {
            Application.OpenURL(sessionData.data.verification_uri + "/" + sessionData.data.user_code);
        }
    }

    public void AuthenticationPoll(ParsecUnity.API.SessionResultDataData data, ParsecUnity.API.SessionResultEnum status)
    {
        switch (status)
        {
            case ParsecUnity.API.SessionResultEnum.PolledTooSoon:
                break;
            case ParsecUnity.API.SessionResultEnum.Pending:
                Debug.Log("Waiting for users..");
                break;
            case ParsecUnity.API.SessionResultEnum.CodeApproved:
                Debug.Log("Code Aproved!");
                authdata = data;
                streamer.StartParsec(4, false, "Glade Party", "Testing", authdata.id);
                Debug.Log(streamer.GetInviteUrl(authdata));
                break;
            case ParsecUnity.API.SessionResultEnum.CodeInvallidExpiredDenied:
                Debug.Log("Code Expired");
                break;
            case ParsecUnity.API.SessionResultEnum.Unknown:
                Debug.Log("Unknown State!!!");
                break;
            default:
                break;
        }
    }

    private void OnApplicationQuit()
    {
        if (streamer != null) streamer.StopParsec();
    }
}
*/