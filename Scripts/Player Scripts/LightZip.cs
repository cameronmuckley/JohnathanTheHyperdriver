using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightZip : MonoBehaviour
{

    public float zipDist = 10;
    ArrayList points = new ArrayList();
    [SerializeField] LayerMask groundMask;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
        points.Clear();
        GameObject[] focalPoints = SceneManager.Instance.focalPoints;
        GameObject focalPoint = null;
        float closestDistance = 99999999f;
        
        for (int i = 0; i < focalPoints.Length; i++)
        {
            float dist = Vector3.Distance(focalPoints[i].transform.position, transform.position);
            if (dist < closestDistance && dist < zipDist)
            {
                focalPoint = focalPoints[i];
                closestDistance = dist;
            }
        }

        if (focalPoint == null) return;

        Vector3 direction = focalPoint.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1000f, groundMask);

        // if the ray hit something
        if (hit.collider != null)
        {
            // if there's a wall between the player and the focal point
            if (hit.distance < closestDistance)
                return;

            gameObject.GetComponent<LineRenderer>().enabled = true;
            points.Add(new Vector2(transform.position.x, transform.position.y));
            points.Add(hit.point);
            if (hit.collider.gameObject.CompareTag("Mirror"))
            {
                hit = ReflectRay(direction, hit);
                points.Add(hit.point);
            }
            if (Input.GetKeyDown("k"))
            {
                transform.position = hit.point + hit.normal * 1;
            }
        }
        int pointCount = 0;
        gameObject.GetComponent<LineRenderer>().SetVertexCount(points.Count);
        foreach (Vector2 point in points)
        {
            gameObject.GetComponent<LineRenderer>().SetPosition(pointCount, point);
            pointCount++;
        }
    }


    RaycastHit2D ReflectRay(Vector2 inDirection, RaycastHit2D hit)
    {
        while (true)
        {
            hit.collider.gameObject.layer = 0;
            Vector3 direction = Vector2.Reflect(inDirection, hit.normal);
            RaycastHit2D newHit = Physics2D.Raycast(hit.point, direction, 1000f, groundMask);
            hit.collider.gameObject.layer = 8;
            if (newHit.collider == null) return newHit;
            gameObject.GetComponent<LineRenderer>().enabled = true;
            points.Add(hit.point);
            points.Add(newHit.point);
            Debug.DrawLine(hit.point, newHit.point);
            
            if (newHit.collider.gameObject.CompareTag("Mirror"))
            {
                inDirection = direction;
                hit = newHit;
            }
            else
            {
                return newHit;
            }
        }
    }
}
