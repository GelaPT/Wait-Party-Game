using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator _animator;

    [Range(0,1)] public float animatorBlend = 0;
    [SerializeField] private bool _jump = false;
    [SerializeField] private bool _stun = false;
    [SerializeField] private bool _crouch = false;
    [SerializeField, Range(0,8)] private int _animationTestInt = 0;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _animator.SetFloat("MoveBlend", animatorBlend);
        _animator.SetBool("isJumping", _jump);
        _animator.SetBool("isStunned", _stun);
        _animator.SetBool("isCrouching", _crouch);
        _animator.SetInteger("AnimationTestInt", _animationTestInt);
    }
}
