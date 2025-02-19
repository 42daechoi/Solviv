using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetMoveAnim(float horizontal, float vertical, float offset)
    {
        if (_animator == null) return;
        
        _animator.SetFloat("Horizontal", horizontal * offset);
        _animator.SetFloat("Vertical", vertical * offset);
    }
}
