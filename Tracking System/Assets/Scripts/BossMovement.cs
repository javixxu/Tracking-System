using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField]
    Transform player;

    SplineFollower splineFollower;

    float maxDistance = 500;

    private void Awake()
    {
        splineFollower = GetComponent<SplineFollower>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        splineFollower.followSpeed = (/*100000*/50000 / distance);
    }
}
