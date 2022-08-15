using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Utilities : MonoBehaviour
{
    public static Vector3 getClickEventPositionNavigation(Camera camera, Plane plane)
    {
        Vector3 position = Vector3.zero;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Terrain")))
        {
            position = hit.point;
        }
        else
        {
            plane.Raycast(ray, out var distance);
            position = ray.GetPoint(distance);
        }
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(position, out navMeshHit, Mathf.Infinity, NavMesh.AllAreas);
        return navMeshHit.position;
    }

    public static Vector3 getClickEventPosition(Camera camera, Plane plane)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out var distance);
        var position = ray.GetPoint(distance);
        return position;
    }

    public static IEnumerator runDelayedAction(UnityAction action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
