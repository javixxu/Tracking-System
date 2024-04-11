using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject toSpawn;
    [SerializeField]
    Transform player;
    [SerializeField]
    SplineFollower boss;
    [SerializeField]
    SplineComputer track;
    [SerializeField]
    bool respectRoadNormals;
    [SerializeField]
    float maxPosVariation;

    LayerMask trackLayer;

    List<GameObject> spawns = new List<GameObject>();
    List<double> spawnperc = new List<double>();

    private void Start()
    {
        trackLayer = track.gameObject.layer;
    }

    private void Update()
    {
        //if (cont > nextSpawn)
        //{
        //    SpawnObject(GetWhereToSpawnPercent());
        //    cont = 0;
        //    nextSpawn = UnityEngine.Random.Range(tMin, tMax);
        //}
        //else
        //{
        //    cont += Time.deltaTime;
        //}

        DespawnPassedObjects();
    }

    public void SpawnThis()
    {
        SpawnObject(GetWhereToSpawnPercent());
    }

    private double GetWhereToSpawnPercent()
    {
        SplineSample nearestPer = new SplineSample();
        track.Project(nearestPer, boss.transform.position);
        return nearestPer.percent + 0.1 > 1.0 ? nearestPer.percent - 0.9 : nearestPer.percent + 0.1;
    }

    private void SpawnObject(double percentInTrack)
    {
        SplineSample referencia = track.Evaluate(percentInTrack);

        Vector3 forwardDelObjeto = referencia.forward;
        Vector3 upwardDelObjeto = referencia.up;

        Vector3 rayDir = -referencia.up;

        var rot = UnityEngine.Random.Range(-maxPosVariation, maxPosVariation);

        rayDir = Quaternion.AngleAxis(rot, forwardDelObjeto) * rayDir;

        Quaternion rotacionFinal;

        if (!respectRoadNormals)
        {
            rotacionFinal = Quaternion.LookRotation(forwardDelObjeto, upwardDelObjeto);
        }
        else
        {
            var objDir = Quaternion.AngleAxis(rot / 2f, forwardDelObjeto) * referencia.up;
            rotacionFinal = Quaternion.LookRotation(forwardDelObjeto, objDir);
        }


        if (Physics.Raycast(referencia.position, rayDir, out RaycastHit hit, 300, ~trackLayer))
        {
            spawns.Add(Instantiate(toSpawn, hit.point, rotacionFinal));
            spawnperc.Add(percentInTrack);
        }
    }

    private void DespawnPassedObjects()
    {
        if (spawnperc.Count > 0)
        {
            SplineSample nearestPer = new SplineSample();
            track.Project(nearestPer, player.transform.position);
            double minClamp = (nearestPer.percent - 0.1) < 0 ? nearestPer.percent + 0.9 : nearestPer.percent - 0.1;

            double maxClamp = (minClamp + 0.5) > 1.0 ? minClamp - 0.5 : minClamp + 0.5;

            if (minClamp < 0.5)
            {
                if (!(spawnperc[0] > minClamp && spawnperc[0] < maxClamp))
                {
                    spawnperc.RemoveAt(0);
                    Destroy(spawns[0]);
                    spawns.RemoveAt(0);
                }
            }
            else
            {
                if (spawnperc[0] < minClamp && spawnperc[0] > maxClamp)
                {
                    spawnperc.RemoveAt(0);
                    Destroy(spawns[0]);
                    spawns.RemoveAt(0);
                }
            }
        }
    }

}
