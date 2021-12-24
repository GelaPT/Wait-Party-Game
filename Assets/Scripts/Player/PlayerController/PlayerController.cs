using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public Player Player { get; set; }

    public Vector3 Position { get; set; }

    public Quaternion Rotation { get; set; }

    public Vector3 BaseVelocity { get; set; }

    public Vector3 Velocity { get; set; }

    public abstract void Update();
}
