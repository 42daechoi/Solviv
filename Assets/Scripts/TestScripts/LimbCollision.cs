using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.SlotRacer;
using UnityEngine;

public class LimbCollision : MonoBehaviour
{
    public RagDollController RagDollController;

    private void Start()
    {
        RagDollController = GameObject.FindObjectOfType<RagDollController>().GetComponent<RagDollController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        RagDollController.isGrounded = true;
    }
}
