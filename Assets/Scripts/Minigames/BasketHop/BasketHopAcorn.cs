using UnityEngine;

public class BasketHopAcorn : MonoBehaviour
{
    private float lifeTime = 0.0f;
    public float lifeSpan = 5.0f;
    public float lifePenalty = 0.5f;
    [HideInInspector] public Player player;

    private void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime > lifeSpan)
        {
            Destroy(gameObject);
        }
    }

    public void Score()
    {
        BasketHopManager.Instance.Score(player);
        lifeTime = lifeSpan - lifePenalty;
    }
}
