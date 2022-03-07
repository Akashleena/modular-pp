// A C# program to check if a given point
// lies inside a given polygon
// Refer https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
// for explanation of functions onSegment(),
// orientation() and doIntersect()
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class CheckifinsideObstacle
{

    // Define Infinite (Using INT_MAX
    // caused overflow problems)
    static int INF = 10000;


    // Given three collinear points p, q, r,
    // the function checks if Vector3 q lies
    // on line segment 'pr'
    static bool onSegment(Vector3 p, Vector3 q, Vector3 r)
    {
        if (q.x <= Math.Max(p.x, r.x) &&
            q.x >= Math.Min(p.x, r.x) &&
            q.y <= Math.Max(p.y, r.y) &&
            q.y >= Math.Min(p.y, r.y))
        {
            return true;
        }
        return false;
    }

    // To find orientation of ordered triplet (p, q, r).
    // The function returns following values
    // 0 --> p, q and r are collinear
    // 1 --> Clockwise
    // 2 --> Counterclockwise
    private int orientation(Vector3 p, Vector3 q, Vector3 r)
    {
        float val = (q.z - p.z) * (r.x - q.x) -
                (q.x - p.x) * (r.z - q.z);

        if (val == 0)
        {
            return 0; // collinear
        }
        return (val > 0) ? 1 : 2; // clock or counterclock wise
    }

    // The function that returns true if
    // line segment 'p1q1' and 'p2q2' intersect.
    private bool doIntersect(Vector3 p1, Vector3 q1,
                            Vector3 p2, Vector3 q2)
    {
        // Find the four orientations needed for
        // general and special cases
        int o1 = orientation(p1, q1, p2);
        int o2 = orientation(p1, q1, q2);
        int o3 = orientation(p2, q2, p1);
        int o4 = orientation(p2, q2, q1);

        // General case
        if (o1 != o2 && o3 != o4)
        {
            return true;
        }

        // Special Cases
        // p1, q1 and p2 are collinear and
        // p2 lies on segment p1q1
        if (o1 == 0 && onSegment(p1, p2, q1))
        {
            return true;
        }

        // p1, q1 and p2 are collinear and
        // q2 lies on segment p1q1
        if (o2 == 0 && onSegment(p1, q2, q1))
        {
            return true;
        }

        // p2, q2 and p1 are collinear and
        // p1 lies on segment p2q2
        if (o3 == 0 && onSegment(p2, p1, q2))
        {
            return true;
        }

        // p2, q2 and q1 are collinear and
        // q1 lies on segment p2q2
        if (o4 == 0 && onSegment(p2, q1, q2))
        {
            return true;
        }

        // Doesn't fall in any of the above cases
        return false;
    }

    // Returns true if the Vector3 p lies
    // inside the polygon[] with n vertices
    public bool isInside(Vector3[] polygon, int n, Vector3 p)
    {
        // There must be at least 3 vertices in polygon[]
        if (n < 3)
        {
            return false;
        }

        // Create a Vector3 for line segment from p to infinite
        Vector3 extreme = new Vector3(INF, p.y);

        // Count intersections of the above line
        // with sides of polygon
        int count = 0, i = 0;
        do
        {
            int next = (i + 1) % n;

            // Check if the line segment from 'p' to
            // 'extreme' intersects with the line
            // segment from 'polygon[i]' to 'polygon[next]'
            if (doIntersect(polygon[i],
                            polygon[next], p, extreme))
            {
                // If the Vector3 'p' is collinear with line
                // segment 'i-next', then check if it lies
                // on segment. If it lies, return true, otherwise false
                if (orientation(polygon[i], p, polygon[next]) == 0)
                {
                    return onSegment(polygon[i], p,
                                    polygon[next]);
                }
                count++;
            }
            i = next;
        } while (i != 0);
        // Debug.Log("check count odd or even" + count);
        // Return true if count is odd, false otherwise
        return (count % 2 == 1); // Same as (count%2 == 1)
    }

    // // Driver Code
    // public static void Main(String[] args)
    // {
    //     Vector3[] polygon1 = {new Vector3(0, 0),
    //                         new Vector3(10, 0),
    //                         new Vector3(10, 10),
    //                         new Vector3(0, 10)};
    //     int n = polygon1.Length;
    //     Vector3 p = new Vector3(20, 20);
    //     if (isInside(polygon1, n, p))
    //     {
    //         return true;
    //         Debug.Log("Yes");

    //     }
    //     else
    //     {
    //         return false;
    //         Debug.Log("No");
    //     }
    //     p = new Vector3(5, 5);
    //     if (isInside(polygon1, n, p))
    //     {
    //         Console.WriteLine("Yes");
    //     }
    //     else
    //     {
    //         Console.WriteLine("No");
    //     }
    //     Vector3[] polygon2 = {new Vector3(0, 0),
    //                         new Vector3(5, 5),
    //                         new Vector3(5, 0)};
    //     p = new Vector3(3, 3);
    //     n = polygon2.Length;
    //     if (isInside(polygon2, n, p))
    //     {
    //         Console.WriteLine("Yes");
    //     }
    //     else
    //     {
    //         Console.WriteLine("No");
    //     }
    //     p = new Vector3(5, 1);
    //     if (isInside(polygon2, n, p))
    //     {
    //         Console.WriteLine("Yes");
    //     }
    //     else
    //     {
    //         Console.WriteLine("No");
    //     }
    //     p = new Vector3(8, 1);
    //     if (isInside(polygon2, n, p))
    //     {
    //         Console.WriteLine("Yes");
    //     }
    //     else
    //     {
    //         Console.WriteLine("No");
    //     }
    //     Vector3[] polygon3 = {new Vector3(0, 0),
    //                         new Vector3(10, 0),
    //                         new Vector3(10, 10),
    //                         new Vector3(0, 10)};
    //     p = new Vector3(-1, 10);
    //     n = polygon3.Length;
    //     if (isInside(polygon3, n, p))
    //     {
    //         Console.WriteLine("Yes");
    //     }
    //     else
    //     {
    //         Console.WriteLine("No");
    //     }
    // }
}
