using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator _animator;
    
    [Range(0,1)] public float animatorBlend = 0;

    [SerializeField] private GameObject _face;
    [Range(0, 2)] public int faceIndex = 0;
    [SerializeField] private Texture[] _faces;
    [SerializeField] private bool _changeFace = false;
    [SerializeField] private bool _jump = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        _animator.SetFloat("MoveBlend", animatorBlend);
        if (_changeFace)
        {
            _face.GetComponent<SkinnedMeshRenderer>().materials[0].mainTexture = _faces[faceIndex];
            _changeFace = false;
        }
        _animator.SetBool("isJumping", _jump);
    }
}
