using UnityEngine;

public class BoardCameraController : CameraController
{
    public override void Init(Player player, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Vector3 scale = new Vector3(), float fov = 60.0f, Camera camera = null)
    {
        base.Init(player, position, rotation, scale, fov, camera);
    }

    public override void Activated()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivated()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}