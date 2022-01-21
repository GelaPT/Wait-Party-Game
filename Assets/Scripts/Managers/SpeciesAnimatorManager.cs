using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesAnimatorManager : MonoBehaviour
{
    //esta classe está dentro dos prefabs das espécies.
    //No componente chamado Animation, poe a animaçao que quer dar play la e pronto

    private Animation _anim;
    public float minX = 0.7f;
    public float maxX = 1.2f;
    public float minY = 0.6f;
    public float maxY = 1.2f;
    public float minZ = 0.6f;
    public float maxZ = 1.2f;
    public float minSpeed = 0.5f;
    public float maxSpeed = 1.5f;

    private void Start()
    {
        _anim = GetComponent<Animation>();

        //alterar speed ao começar
        foreach (AnimationState state in _anim)
        {
            state.speed = Random.Range(minSpeed, maxSpeed);
        }

        //definir escala aleatoria
        transform.localScale = new Vector3(Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ));
    }

    private void FixedUpdate()
    {
        //chance de alterar speed a meio
        int random = Random.Range(0, 300);
        if (random < 5)
        {
            foreach (AnimationState state in _anim)
            {
                state.speed = Random.Range(minSpeed, maxSpeed);
            }
        }
    }
}
