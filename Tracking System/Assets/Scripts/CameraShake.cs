using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float strengh = 1;
    float time = 0;

    [SerializeField]
    float fovDecrease;

    float maxTime = 0;

    private void Update()
    {
        if (time > 0)
        {
            transform.position = transform.parent.position + transform.up * Random.Range(-strengh * (time / maxTime), strengh * (time / maxTime)) + transform.right * Random.Range(-strengh * (time / maxTime * 2), strengh * (time / maxTime * 2));
            time -= Time.deltaTime;
            if (time < 0)
            {
                transform.position = transform.parent.position;
            }
        }
        /*
        if (player.IsOnWall())
        {
            cam.fieldOfView -= fovDecrease * Time.deltaTime;
        }
        else
        {
            if (cam.fieldOfView < ogFOV)
            {
                cam.fieldOfView += fovDecrease * Time.deltaTime * 3;
                if (cam.fieldOfView < ogFOV)
                    cam.fieldOfView = ogFOV;
            }
        }
        */
    }

    public void Shake(float duration, float fuerza)
    {
        strengh = fuerza;
        time = duration;
        maxTime = time;
    }
}
