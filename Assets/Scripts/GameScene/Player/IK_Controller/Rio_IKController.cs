using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rio_IKController : MonoBehaviour
{
    private Animator animator;

    public Transform headBone;
    public Transform rightHandTarget; // 오른손 타겟 (무기 손잡이)
    public Transform leftHandTarget; // 왼손 타겟 (무기 그립)
    [SerializeField] private Transform spine; // 상체 뼈대 (Spine 또는 UpperBody)
    [SerializeField] private Transform cameraTransform; // FollowCamera
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void LateUpdate()
    {
        if (spine && cameraTransform)
        {
            // 상체 회전 설정 (카메라 회전을 Spine에 반영)
            Quaternion cameraRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            spine.rotation = cameraRotation;
        }
    }

    // Update is called once per frame
    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // 1. 오른손 IK (무기 위치)
            if (rightHandTarget)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
            }

            // 2. 왼손 IK (Grip 위치)
            if (leftHandTarget)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
            }
        }
    }
}
