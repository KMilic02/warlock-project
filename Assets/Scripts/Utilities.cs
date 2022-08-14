using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Vector3 getClickEventPosition(Camera camera)
    {
        Vector3 position = new Vector3(0, 0, 0);

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Terrain")))
        {
            position = hit.point;
            Debug.Log(position);
        }

        return position;
    }
}
