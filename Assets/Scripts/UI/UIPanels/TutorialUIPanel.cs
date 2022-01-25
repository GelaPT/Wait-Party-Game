using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUIPanel : MonoBehaviour
{
    [System.Serializable]
    public class ControlCard
    {
        public Sprite buttonIcon;
        public string buttonActionText;
    }

    public TextMeshProUGUI minigameName;
    public TextMeshProUGUI minigameCategory;
    public Image tutorialImage;
    public TextMeshProUGUI tutorialDescription;
    public ControlsCard controlCard;
    public RectTransform controlPanelParent;
    
    public ControlCard[] controls;
    void Start()
    {
        foreach (ControlCard card in controls)
        {
            ControlsCard newControlCard = Instantiate(controlCard, controlPanelParent);
            newControlCard.buttonIcon.sprite = card.buttonIcon;
            newControlCard.buttonActionText.SetText(card.buttonActionText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
