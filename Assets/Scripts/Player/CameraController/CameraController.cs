using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player Player { get; set; }

    public Vector3 Position { get; set; }

    public Quaternion Rotation { get; set; }

    public Vector3 Scale { get; set; }

    public float FieldOfView { get; set; }

    public Camera Camera { get; set; }

    public virtual void Init(Player player, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Vector3 scale = new Vector3(), float fov = 60.0f, Camera camera = null)
    {
        Player = player;
        Position = position;
        Rotation = rotation == new Quaternion() ? Quaternion.identity : rotation;
        Scale = scale;
        FieldOfView = fov;
        Camera = camera != null ? camera : Camera.main;
    }

    public virtual void Update()
    {

    }

    public virtual void Activated()
    {

    }

    public virtual void Deactivated()
    {

    }
}
