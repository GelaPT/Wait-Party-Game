using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollower : MonoBehaviour
{
    [SerializeField]
    private TileRoute paths;

    public int pathIndex = 0;
    public float walkTime = 1f;
    private float speed = 1f;

    private float pathAlpha;
    private Vector3 objectPosition;
    public bool moving = false;
    private Animator animator;
    public float jumpwait = 0.6f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRoute(TileRoute p, int index) //recebe qual dos caminhos tomar, 0 ou 1; recebe o objeto que tem o array de paths.
    {                                            //Esse script vai ler qual path do array baseado no index do argumento.
        pathIndex = index;
        paths = p;
    }

    public void Move()
    {
        pathAlpha = 0f;
        moving = true;
    }

    void Update()
    {
        if (moving)
        {
            StartCoroutine(GoByTheRoute());
        }
    }

    private IEnumerator GoByTheRoute()
    {
        moving = false;

        Vector3 p0 = paths.paths[pathIndex].controlPoints[0].position;
        Vector3 p1 = paths.paths[pathIndex].controlPoints[1].position;
        Vector3 p2 = paths.paths[pathIndex].controlPoints[2].position;
        Vector3 p3 = paths.paths[pathIndex].controlPoints[3].position;

        speed = -0.2f * Vector3.Distance(paths.paths[pathIndex].controlPoints[0].position, paths.paths[pathIndex].controlPoints[3].position) + 4;

        bool jump = paths.ownTile.GetComponent<Tile>().jump[pathIndex];
        animator.SetBool("isJumping", jump);
        animator.SetBool("isWalking", !jump);

        //mover o personagem em si
        while (pathAlpha < 1)
        {
            pathAlpha += Time.deltaTime * speed / walkTime;

            objectPosition = Mathf.Pow(1 - pathAlpha, 3) * p0 + 3 * Mathf.Pow(1 - pathAlpha, 2) * pathAlpha * p1 +
                3 * (1 - pathAlpha) * Mathf.Pow(pathAlpha, 2) * p2 +
                Mathf.Pow(pathAlpha, 3) * p3;

            //rodar para onde está andando
            if (pathAlpha > 0.01 && pathAlpha < 0.9f)
            {
                Vector3 relativePos = objectPosition - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
                transform.rotation = rotation;
            }

            //mover
            transform.position = objectPosition;
            yield return null;
        }

        //após chegar ao fim do caminho
        pathAlpha = 0f;
        moving = false;

        //teste, fazer ele andar pelo mapa todo pelo caminho 0 sempre
        SetRoute(paths.ownTile.GetComponent<Tile>().nextTile[pathIndex].route, 0); //pegar a route do nextTile[0]
        
        if (paths.ownTile.GetComponent<Tile>().jump[pathIndex])
        {
            animator.SetBool("isJumping", true);
        }
        else if(animator.GetBool("isJumping") && !paths.ownTile.GetComponent<Tile>().jump[pathIndex])
        {
            animator.SetBool("isJumping", false);
            yield return new WaitForSeconds(jumpwait);
        }
        Move();
    }
}