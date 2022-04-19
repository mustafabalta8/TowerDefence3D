using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f,5f)] private float speed = 1f;

    private List<Node> path = new List<Node>();
    
    private void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);       
        
        //StartCoroutine(FollowPath());
    }

    public void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = PathFinder.instance.StartCoordinates;
        }
        else
        {          
            coordinates = GridManager.Instance.GetCoordinatesFromPosition(transform.position);
        }
        //StopCoroutine(FollowPath());
        StopAllCoroutines();
        path.Clear();
        path = PathFinder.instance.GetNewPath(coordinates);
        
        StartCoroutine(FollowPath());
        
    }
   
    private void ReturnToStart()
    {
        transform.position = GridManager.Instance.GetPositionFromCoordinates(PathFinder.instance.StartCoordinates);
    }

    private IEnumerator FollowPath()
    {
        for(int i = 1;i<path.Count;i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = GridManager.Instance.GetPositionFromCoordinates(path[i].coordinates);
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
