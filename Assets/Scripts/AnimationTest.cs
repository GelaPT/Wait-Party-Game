using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator _animator;
    
    [Range(0,1)] public float animatorBlend = 0;
    [SerializeField] private bool _jump = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _animator.SetFloat("MoveBlend", animatorBlend);
        _animator.SetBool("isJumping", _jump);
    }
}
