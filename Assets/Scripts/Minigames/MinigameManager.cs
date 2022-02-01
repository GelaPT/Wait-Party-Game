using UnityEngine;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public Transform[] playerSpawns;

    public List<MinigameStats> stats = new();

    public bool onTutorial = true;

    public GameObject PlayerSmoke;
    public class MinigameStats
    {
        public Player player;
        public int place;
        public int points;

        public MinigameStats(Player player)
        {
            this.player = player;
            place = 0;
            points = 0;
        }
    }

    protected virtual void Init()
    {
        for(int i = 0; i < 4; i++)
        {
            if(PlayerManager.Instance.Players[i] != null)
            {
                stats.Add(new MinigameStats(PlayerManager.Instance.Players[i]));
            }
        }
    }

    protected virtual void Update()
    {

    }

    public virtual List<MinigameStats> EndMinigame()
    {
        List<MinigameStats> notHandled = stats;
        List<MinigameStats> final = new();

        for (int place = 1; place < 5; place++)
        {
            if (notHandled.Count == 1)
            {
                notHandled[0].place = place;
                final.Add(notHandled[0]);
                break;
            }

            int highestIndex = 0;
            
            for(int i = 1; i < notHandled.Count; i++)
            {
                if(notHandled[i].points > notHandled[highestIndex].points)
                {
                    highestIndex = i;
                }
            }

            int lastHighestPoint = notHandled[highestIndex].points;
            notHandled[highestIndex].place = place;

            final.Add(notHandled[highestIndex]);
            notHandled.RemoveAt(highestIndex);

            for(int i = notHandled.Count - 1; i >= 0; i--)
            {
                if(notHandled[i].points == lastHighestPoint)
                {
                    notHandled[i].place = place;
                    final.Add(notHandled[i]);
                    notHandled.RemoveAt(i);
                }
            }

            if (notHandled.Count == 0) break;
        }
        
        foreach(var fin in final)
        {
            if (fin.points == 0) fin.place = 4;
        }

        return final;
    }
}
