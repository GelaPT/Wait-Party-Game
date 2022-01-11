using UnityEngine;

public class FogMovement : MonoBehaviour
{
    public Vector3 fogSpeed = new Vector3(1.0f, 0.0f, 0.3f);

    static readonly int shPropOffset = Shader.PropertyToID("Vector3_478B0511");

    private MaterialPropertyBlock mpb;
    private MaterialPropertyBlock Mpb
    {
        get
        {
            if(mpb == null) mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }

    private MeshRenderer mr;
    private Vector3 pos;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        pos += Time.deltaTime * fogSpeed;
        Mpb.SetVector(shPropOffset, pos);
        mr.SetPropertyBlock(Mpb);
    }
}
