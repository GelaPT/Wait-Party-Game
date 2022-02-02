using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

using ThinkFastButton = ThinkFastManager.ThinkFastButton;

public class ThinkFastUI : MonoBehaviour
{
    public Image buttonImage;
    public Image crossImage;
    public Sprite triangleSprite;
    public Sprite squareSprite;
    public Sprite heartSprite;

    public TextMeshProUGUI[] playerScores;

    public void UpdatePlayerScores(int index, int points)
    {
        playerScores[index].SetText((ThinkFastManager.Instance.stats[index].points - points) + " (+ " + points + ")");
        StartCoroutine(ResetPlayerScore(index));
    }

    IEnumerator ResetPlayerScore(int index)
    {
        yield return new WaitForSeconds(2.0f);

        playerScores[index].SetText(ThinkFastManager.Instance.stats[index].points.ToString());
    }

    public void ChangeIcon(ThinkFastButton currentButton)
    {
        switch (currentButton)
        {
            case ThinkFastButton.Triangle:
                buttonImage.enabled = true;
                buttonImage.sprite = triangleSprite;
                break;
            case ThinkFastButton.Heart:
                buttonImage.enabled = true;
                buttonImage.sprite = heartSprite;
                break;
            case ThinkFastButton.Square:
                buttonImage.enabled = true;
                buttonImage.sprite = squareSprite;
                break;
            case ThinkFastButton.NotTriangle:
                buttonImage.enabled = true;
                buttonImage.sprite = triangleSprite;
                crossImage.enabled = true;
                break;
            case ThinkFastButton.NotHeart:
                buttonImage.enabled = true;
                buttonImage.sprite = heartSprite;
                crossImage.enabled = true;
                break;
            case ThinkFastButton.NotSquare:
                buttonImage.enabled = true;
                buttonImage.sprite = squareSprite;
                crossImage.enabled = true;
                break;
            case ThinkFastButton.None:
                buttonImage.enabled = false;
                crossImage.enabled = false;
                break;
        }
    }
}
