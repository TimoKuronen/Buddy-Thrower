using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointGenerator : MonoBehaviour
{
    public static WaypointGenerator Instance;

    [SerializeField] private Transform emptyTransform;
    [SerializeField] private bool generateSecondFloorWaypoints;
    [SerializeField] private float waypointGridDistance = 1f;
    [SerializeField] private float secondFloorHeightCheck = 4f;
    [SerializeField] private int waypointCountX;
    [SerializeField] private int waypointCountZ;
    [SerializeField] private List<Transform> usedWayPoints;

    private NavMeshPath navMeshPath;

    private void Awake()
    {
        Instance = this;
        navMeshPath = new NavMeshPath();
        GenerateWaypoints();
    }

    void GenerateWaypoints()
    {
        List<Transform> transformList = new List<Transform>();
        List<Transform> secondTransformList = new List<Transform>();
        RaycastHit hit;

        for (int x = 0; x < waypointCountX; x++)
        {
            for (int z = 0; z < waypointCountZ; z++)
            {
                Vector3 pos = transform.position + new Vector3(x * waypointGridDistance, 0, z * waypointGridDistance);
                transformList.Add(Instantiate(emptyTransform, pos, Quaternion.identity, transform));

                if (generateSecondFloorWaypoints)
                    secondTransformList.Add(Instantiate(emptyTransform, pos + new Vector3(0, secondFloorHeightCheck, 0), Quaternion.identity, transform));
            }
        }

        for (int i = 0; i < transformList.Count; i++)
        {
            NavMesh.CalculatePath(transform.position, transformList[i].position, NavMesh.AllAreas, navMeshPath);

            if (navMeshPath.status != NavMeshPathStatus.PathInvalid)
                usedWayPoints.Add(transformList[i]);
            else Destroy(transformList[i].gameObject);
        }


        for (int i = 0; i < secondTransformList.Count; i++)
        {
            Vector3 pos = Vector3.zero;
            if (Physics.Raycast(secondTransformList[i].position, -Vector3.up, out hit, secondFloorHeightCheck))
                pos = hit.point;

            if (pos == Vector3.zero || IsNumberClose(pos.y, transformList[i].position.y, 1))
            {
                Destroy(secondTransformList[i].gameObject);
                continue;
            }

            NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, navMeshPath);

            if (navMeshPath.status != NavMeshPathStatus.PathInvalid)
            {
                secondTransformList[i].position = pos;
                usedWayPoints.Add(secondTransformList[i]);
            }
            else Destroy(secondTransformList[i].gameObject);
        }
    }

    public Transform[] ReturnClosestWaypoints(int count, Vector3 origin)
    {
        Transform[] returnWaypoints = new Transform[count];
        usedWayPoints.Sort((v1, v2) => (v1.position - origin).sqrMagnitude.CompareTo((v2.position - origin).sqrMagnitude));
        for (int i = 0; i < count; i++)
        {
            returnWaypoints[i] = usedWayPoints[i];
        }
        return returnWaypoints;
    }

    public Transform[] ReturnFurthestWaypoints(int count, Vector3 origin)
    {
        Transform[] returnWaypoints = new Transform[count];
        usedWayPoints.Sort((v1, v2) => (v1.position - origin).sqrMagnitude.CompareTo((v2.position - origin).sqrMagnitude));
        int iterationCount = 0;
        for (int i = usedWayPoints.Count - 1; i > usedWayPoints.Count - count - 1; i--)
        {
            Debug.Log(i);
            returnWaypoints[iterationCount] = usedWayPoints[i];
            iterationCount++;
        }
        return returnWaypoints;
    }
    bool IsNumberClose(float a, float b, float tolerance)
    {
        return Mathf.Abs(a - b) < tolerance;
    }
}