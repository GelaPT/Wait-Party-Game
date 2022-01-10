using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciesAnimatorManager : MonoBehaviour
{
    //esta classe está dentro dos prefabs das espécies.
    //No componente chamado Animation, poe a animaçao que quer dar play la e pronto

    private Animation _anim;

    private void Start()
    {
        _anim = GetComponent<Animation>();

        //alterar speed ao começar
        foreach (AnimationState state in _anim)
        {
            state.speed = Random.Range(0.5f, 1.5f);
        }

        //definir escala randomica
        transform.localScale = new Vector3(Random.Range(0.4f, 1),
            Random.Range(0.7f, 1),
            Random.Range(0.6f, 1));
    }

    private void FixedUpdate()
    {
        //chance de alterar speed a meio
        if (Random.Range(0, 300) == 1)
        {
            foreach (AnimationState state in _anim)
            {
                state.speed = Random.Range(0.5f, 1.5f);
            }
        }
    }
}
