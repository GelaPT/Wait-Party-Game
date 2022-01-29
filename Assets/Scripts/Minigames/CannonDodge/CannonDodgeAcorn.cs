using UnityEngine;

public class CannonDodgeAcorn : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float lifeSpan = 5.0f;
    private float lifeTime = 0.0f;

    private void FixedUpdate()
    {
        transform.position += -transform.up * speed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime > lifeSpan) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();

        CannonDodgeManager.Instance.Kill(player as CannonDodgePlayerController);
    }
}
