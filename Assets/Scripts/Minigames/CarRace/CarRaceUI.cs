using UnityEngine;

public class CarRaceUI : MonoBehaviour
{
    public RectTransform[] playerText;
    public Vector3 offset;

    private void Update()
    {
        Transform[] playerCars = CarRaceManager.Instance.playerSpawns;

        for (int i = 0; i < 4; i++)
        {
            playerText[i].position = Camera.main.WorldToScreenPoint(playerCars[i].position) + (offset * (Camera.main.scaledPixelWidth / 1920f));
        }
    }
}
