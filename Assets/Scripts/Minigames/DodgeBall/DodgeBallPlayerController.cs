using UnityEngine;
using System.Collections;

public class DodgeBallPlayerController : PlayerController
{
    public GameObject acornHand;
    private GameObject acorn;
    public GameObject acornWorld;

    public int team = 0;

    private Rigidbody rb;

    private Transform rightHand;

    private float speed = 4.0f;

    private bool isStunned = false;
    public int balls = 0;
    private bool hasBall = false;
    private bool isAiming = false;

    private float accelTimer = 0.0f;
    private float accelSpan = 2.0f;
    private float deaccelTimer = 0.0f;
    private float deaccelSpan = 0.4f;

    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
        collider.height = 1.5f;
        collider.center = Vector3.up;

        StartCoroutine(FindBone());

        if (Player.AI)
        {
            AIController = gameObject.AddComponent<DodgeBallAIController>();
            AIController.Player = (AIPlayer)Player;
        }
    }

    IEnumerator FindBone()
    {
        yield return new WaitUntil(() => transform.GetChild(0).Find("Armature/Root/spine1/spine2/spine3/r.shoulder/r.arm1/r.arm2/r.hand/r.hand_end") != null);

        rightHand = transform.GetChild(0).Find("Armature/Root/spine1/spine2/spine3/r.shoulder/r.arm1/r.arm2/r.hand/r.hand_end");
    }

    protected override void Update()
    {
        if (DodgeBallManager.Instance.ended) return;

        base.Update();

        if (isStunned) return;

        hasBall = balls > 0;

        Vector2 direction = InputManager.GetAxis(Player, InputAxis.Left);

        if (direction.magnitude < 0.3f) direction = Vector2.zero;

        if (hasBall)
        {
            if(isAiming)
            {
                BaseVelocity = Vector3.zero;
                Velocity = Vector3.zero;

                if(!InputManager.GetButton(Player, InputButton.A) || InputManager.GetButton(Player, InputButton.X))
                {
                    if(direction != Vector2.zero)
                    {
                        ThrowBall();
                    }

                    isAiming = false;
                    Animator.SetBool("isAiming", false);
                    return;
                }

                if(direction.magnitude > 0.2f) transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(Vector2.down, new Vector2(direction.x, direction.y * -1)), 0);

                return;
            }

            if (InputManager.GetButton(Player, InputButton.A))
            {
                isAiming = true;
                Animator.SetBool("isAiming", true);
                return;
            }
        }

        direction.y *= -1;

        if (direction.magnitude > 0.2f) transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(Vector2.down, direction), 0);

        direction.y *= -1;

        Animator.SetFloat("MoveBlend", direction.magnitude);

        BaseVelocity = (isAiming ? speed/2 : speed)* new Vector3(direction.x, 0, direction.y);

        if (BaseVelocity == Vector3.zero)
        {
            accelTimer = 0.0f;
            deaccelTimer += Time.deltaTime;
        }
        else
        {
            deaccelTimer = 0.0f;
            accelTimer += Time.deltaTime;
        }
    }

    protected override void FixedUpdate()
    {
        if (BaseVelocity == Vector3.zero)
        {
            Velocity = Vector3.Lerp(Velocity, Vector3.zero, deaccelTimer / deaccelSpan);
        }
        else
        {
            Velocity = Vector3.Lerp(Velocity, BaseVelocity, accelTimer / accelSpan);
        }

        rb.velocity = Velocity;
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    public void PickUpBall()
    {
        balls++;

        acorn = Instantiate(acornHand, rightHand.transform.position, rightHand.transform.rotation, rightHand);

        Animator.SetBool("hasBall", true);
    }

    public void ThrowBall()
    {
        balls--;

        Destroy(acorn);
        
        if (balls <= 0)
        {
            Animator.SetBool("hasBall", false);
        }
        else
        {
            acorn = Instantiate(acornHand, rightHand.transform.position, rightHand.transform.rotation, rightHand);
        }

        GameObject ball = Instantiate(acornWorld, transform.position + transform.forward + new Vector3(0.3f, 1.3f, 0.0f), transform.rotation);
        ball.GetComponent<DodgeBallAcornWorld>().team = team;
    }

    public override void Kill()
    {
        if (balls > 0)
        {
            if (Player.ID == 0)
            {
                if ((PlayerManager.Instance.Players[2].PlayerController as DodgeBallPlayerController).balls == 0)
                {
                    (PlayerManager.Instance.Players[2].PlayerController as DodgeBallPlayerController).PickUpBall();
                    (PlayerManager.Instance.Players[2].PlayerController as DodgeBallPlayerController).balls += balls - 1;
                }
                else
                {
                    (PlayerManager.Instance.Players[2].PlayerController as DodgeBallPlayerController).balls += balls;
                }
            }
            if (Player.ID == 1)
            {
                if ((PlayerManager.Instance.Players[3].PlayerController as DodgeBallPlayerController).balls == 0)
                {
                    (PlayerManager.Instance.Players[3].PlayerController as DodgeBallPlayerController).PickUpBall();
                    (PlayerManager.Instance.Players[3].PlayerController as DodgeBallPlayerController).balls += balls - 1;
                }
                else
                {
                    (PlayerManager.Instance.Players[3].PlayerController as DodgeBallPlayerController).balls += balls;
                }
            }
            if (Player.ID == 2)
            {
                if ((PlayerManager.Instance.Players[0].PlayerController as DodgeBallPlayerController).balls == 0)
                {
                    (PlayerManager.Instance.Players[0].PlayerController as DodgeBallPlayerController).PickUpBall();
                    (PlayerManager.Instance.Players[0].PlayerController as DodgeBallPlayerController).balls += balls - 1;
                }
                else
                {
                    (PlayerManager.Instance.Players[0].PlayerController as DodgeBallPlayerController).balls += balls;
                }
            }
            if (Player.ID == 3)
            {
                if ((PlayerManager.Instance.Players[1].PlayerController as DodgeBallPlayerController).balls == 0)
                {
                    (PlayerManager.Instance.Players[1].PlayerController as DodgeBallPlayerController).PickUpBall();
                    (PlayerManager.Instance.Players[1].PlayerController as DodgeBallPlayerController).balls += balls - 1;
                }
                else
                {
                    (PlayerManager.Instance.Players[1].PlayerController as DodgeBallPlayerController).balls += balls;
                }                
            }
        }

        isStunned = true;
        Destroy(acorn);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Animator.SetTrigger("Stun");
        Invoke("DestroySelf", 3f);
    }

    private void DestroySelf()
    {
        Instantiate(DodgeBallManager.Instance.PlayerSmoke, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
