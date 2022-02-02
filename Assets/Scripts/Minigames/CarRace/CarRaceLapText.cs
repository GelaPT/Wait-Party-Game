using UnityEngine;
using TMPro;

public class CarRaceLapText : MonoBehaviour
{
    public TextMeshProUGUI lapText;

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;
        transform.position += transform.up * 4 * Time.deltaTime;

        if (timer > 1.0f) Destroy(gameObject);
    }
}
