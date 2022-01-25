using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player Player { get; set; }

    public Vector3 Position { get; set; }

    public Quaternion Rotation { get; set; }

    public Vector3 Scale { get; set; }

    public Vector3 BaseVelocity { get; set; }

    public Vector3 Velocity { get; set; }

    public Animator Animator { get; set; }

    public virtual void Init(Player player, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Vector3 scale = new Vector3(), Vector3 BaseVelocity = new Vector3())
    {
        Player = player;
        Position = Vector3.zero;
        Rotation = Quaternion.identity;
        Scale = Vector3.one;
        BaseVelocity = Vector3.zero;
        Velocity = Vector3.zero;
    }

    public virtual void Respawn()
    {

    }

    public virtual void Kill()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }
}
