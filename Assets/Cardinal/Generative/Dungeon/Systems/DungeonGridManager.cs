using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGridManager : MonoBehaviour
{
    public List<GameObject> GridPoints;
    public GameObject MarkerObj;
    public void InitGrid(int dungeonSize) 
    {
        for (int i = - 1 * (dungeonSize / 2); i < dungeonSize/2; i++)
        {
            for (int u = -1 * (dungeonSize / 2); u < dungeonSize/2; u++)
            {
                GameObject Marker = Instantiate(MarkerObj);
                Vector3 position = new Vector3(i * 20, 0, u * 20);
                Marker.transform.parent = this.transform;
                Marker.transform.position = position;
                Marker.name = "(" + i + "," + u + ")";
                GridPoints.Add(Marker);
            }
        }
    }
}
