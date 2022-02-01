using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
public class Path
{
    public Transform[] controlPoints = new Transform[4];
}

public class TileRoute : MonoBehaviour
{
    public Path[] paths;
    
    //public Transform[] controlPoints;
    [SerializeField]
    private GameObject ownTile;
    private Tile[] nextTile;

    private Vector3 gizmosPosition;
    private void Awake()
    {
        nextTile = ownTile.GetComponent<Tile>().nextTile;

        for(int i = 0; i < paths.Length; i++)
        {
            //ends desse pros proximos
            paths[i].controlPoints[3].position = nextTile[i].GetComponentInChildren<TileRoute>().transform.position;
            
            if(paths[i].controlPoints[2].position == paths[i].controlPoints[0].position)
                paths[i].controlPoints[2].position = nextTile[i].GetComponentInChildren<TileRoute>().transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        foreach(Path path in paths) {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                gizmosPosition = Mathf.Pow(1 - t, 3) * path.controlPoints[0].position +
                    3 * Mathf.Pow(1 - t, 2) * t * path.controlPoints[1].position +
                    3 * (1 - t) * Mathf.Pow(t, 2) * path.controlPoints[2].position +
                    Mathf.Pow(t, 3) * path.controlPoints[3].position;
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(gizmosPosition, 0.25f);
            }
            foreach(Transform t in path.controlPoints)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(t.position, 0.2f);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(path.controlPoints[0].position.x, path.controlPoints[0].position.y, path.controlPoints[0].position.z), new Vector3(path.controlPoints[1].position.x, path.controlPoints[1].position.y, path.controlPoints[1].position.z));
            Gizmos.DrawLine(new Vector3(path.controlPoints[2].position.x, path.controlPoints[2].position.y, path.controlPoints[2].position.z), new Vector3(path.controlPoints[3].position.x, path.controlPoints[3].position.y, path.controlPoints[3].position.z));

            Handles.color = Color.white;
            Handles.Label(path.controlPoints[0].position, "Start");
            Handles.Label(path.controlPoints[3].position, "End");
        }
    }
}
