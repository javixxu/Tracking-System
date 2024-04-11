using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    ShipController player;
    [SerializeField]
    Transform boss;
    [SerializeField]
    [Tooltip("0 = Mira siempre al player, 1 = mira siempre al boss")]
    float offset = 0.3f;
    [SerializeField]
    [Tooltip("Que tan arriba se coloca la camara")]
    float abovePlayer;
    [SerializeField]
    [Tooltip("Que tan atras se coloca la camara")]
    float behindPlayer;
    [SerializeField]
    [Tooltip("Cuanto lerp se le aplica a la posicion al moverse")]
    float posLerp;
    [SerializeField]
    float lerpVarOnWall = 8;
    [SerializeField]
    [Tooltip("Cuanto lerp se le aplica al objetivo de la camara con respecto a su valor anterior")]
    float lookAtLerp;
    [SerializeField]
    [Tooltip("Cuanto lerp se le aplica al upwards de la camara")]
    float upLerp;

    float wallLerp;
    float floorLerp;

    Vector3 lastLook;

    public ShipController GetPlayer()
    {
        return player;
    }

    private void Start()
    {
        transform.position = player.transform.position;

        Vector3 playerToBoss = boss.position - player.transform.position;
        Vector3 lookAt = (playerToBoss * offset) + player.transform.position;
        transform.LookAt(lookAt, player.transform.up);
        lastLook = lookAt;

        wallLerp = posLerp / lerpVarOnWall;
        floorLerp = posLerp;
    }

    void Update()
    {
        
        if (player.IsOnWall())
        {
            posLerp = wallLerp;
        }
        else
        {
            posLerp = floorLerp;
        }
        

        Vector3 finalPos = player.transform.position;
        finalPos += player.transform.forward * -behindPlayer;
        finalPos += player.transform.up * abovePlayer;
        transform.position = Vector3.Lerp(transform.position, finalPos, posLerp);

        Vector3 playerToBoss = boss.position - player.transform.position;
        Vector3 lookAt = (playerToBoss * offset) + player.transform.position;
        transform.LookAt(Vector3.Lerp(lastLook, lookAt, lookAtLerp), Vector3.Lerp(transform.up, player.transform.up, upLerp));
        lastLook = lookAt;
    }

    private void OnDrawGizmos()
    {
        Vector3 playerToBoss = boss.position - player.transform.position;

        Vector3 lookAt = playerToBoss * offset;

        Gizmos.DrawLine(player.transform.position, lookAt + player.transform.position);
    }
}
