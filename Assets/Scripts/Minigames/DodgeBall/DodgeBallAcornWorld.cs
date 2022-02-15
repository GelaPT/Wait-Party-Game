using UnityEngine;

public class DodgeBallAcornWorld : MonoBehaviour
{
    public int team;
    public float speed = 3.0f;

    private void FixedUpdate()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<DodgeBallPlayerController>().team != team)
            {
                collision.gameObject.GetComponent<DodgeBallPlayerController>().Kill();
                Destroy(gameObject);
                DodgeBallManager.Instance.GiveBall(team == 0 ? 1 : 0);
                AudioManager.Instance.PlaySound("sfx_collision");
            }
        } 
        else if (collision.gameObject.name == "Colliders")
        {
            Destroy(gameObject);
            DodgeBallManager.Instance.GiveBall(team == 0 ? 1 : 0);
        }
    }
}
