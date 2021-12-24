using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    public Player Player { get; set; }

    public Vector3 Position { get; set; }

    public Quaternion Rotation { get; set; }

    public float FieldOfView { get; set; }

    public abstract void Update();

    public abstract void Activated();

    public abstract void Deactivated();
}
