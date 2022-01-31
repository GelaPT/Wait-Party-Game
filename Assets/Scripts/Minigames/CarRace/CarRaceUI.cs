using UnityEngine;
using TMPro;

public class CarRaceUI : MonoBehaviour
{
    public RectTransform[] playerText;
    public Transform[] playerCars;
    public Vector3 offset;

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            playerText[i].position = Camera.main.WorldToScreenPoint(playerCars[i].position) + (offset * (Camera.main.scaledPixelWidth / 1920f));
        }
    }
}
