using UnityEngine;
using System.Collections.Generic;

public class SetWalkability : MonoBehaviour
{
    Grids gg;
    Node n;
    Pathfinding pf;
    public Transform testPrefab;
    public Transform seeker;
    public Transform target;
    private void Awake()
    {
        gg = gameObject.GetComponent<Grids>();
      
  
    }
    void Start()
    {
       pf = gameObject.GetComponent<Pathfinding>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {

            pf.pathLineRenderer.positionCount = 0;
            pf.finalpathLineRenderer.positionCount = 0;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            pf.FindPath(seeker.position, target.position);
        }
    }

    public void SetNodewalkability(List<List<Vector3>> finalobstacleList, int gridSizeX, int gridSizeY)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = gg.worldBottomLeft + Vector3.right * (x * gg.nodeDiameter + gg.nodeRadius) + Vector3.forward * (y * gg.nodeDiameter + gg.nodeRadius);
                foreach (var fol in finalobstacleList)
                {
                        for (int i = 0; i < fol.Count; i++)
                        { 
                            if (worldPoint.x == fol[i].x && worldPoint.y == fol[i].y && worldPoint.z== fol[i].z)
                            {
                                //n.walkable = false;
                                gg.grid[x, y] = new Node(false, worldPoint, x, y);
                                
                                Vector3 objectPOS5 = worldPoint;
                                var obstacleprefab = Instantiate(testPrefab, objectPOS5, Quaternion.identity);
                                obstacleprefab.GetComponent<Renderer>().material.color = Color.red;

                            }
                            else
                            {
                                //n.walkable = true;
                                gg.grid[x, y] = new Node(true, worldPoint, x, y);
                            }
                        }

                }
            }
        }
            pf.FindPath(seeker.position, target.position);
     
    }

}
