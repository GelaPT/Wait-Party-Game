using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayerController : PlayerController
{
    public override void Init(Player player, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Vector3 scale = new Vector3(), Vector3 baseVelocity = new Vector3())
    {
        base.Init(player, position, rotation, scale, baseVelocity);
    }

    public override void Respawn()
    {

    }

    public override void Kill()
    {

    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }
}
