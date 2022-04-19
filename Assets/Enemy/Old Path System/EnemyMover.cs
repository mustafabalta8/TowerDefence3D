using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f,5f)] private float speed = 1f;
    [SerializeField] private List<Tile> path = new List<Tile>();
    
    private void OnEnable()
    {
        FindPath();       
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();

        GameObject pathKeeper = GameObject.FindGameObjectWithTag("Path");
        
        foreach(Transform child in pathKeeper.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();
            if(waypoint != null)
            {
                path.Add(waypoint);
            }
            

        }
    }
    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach(Tile waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        gameObject.SetActive(false);
        //TODO lower player's health
    }
    
}
