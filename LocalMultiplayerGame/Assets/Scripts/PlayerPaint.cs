using System.Collections.Generic;
using UnityEngine;

public class PlayerPaint : MonoBehaviour
{
   private LineRenderer lineRenderer;
   private List<Vector3> trailPositions = new List<Vector3>();

    private void Start()
    {
        lineRenderer= GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;

        if (gameObject.name == "Player1")
        {
            lineRenderer.material.color = Color.blue;
        }
        else if (gameObject.name == "Player2")
        {
            lineRenderer.material.color = Color.red;
        }
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (trailPositions.Count == 0 || Vector3.Distance(trailPositions[trailPositions.Count - 1], currentPosition) > 0.1f)
        {
            trailPositions.Add(currentPosition);
            lineRenderer.positionCount = trailPositions.Count;
            lineRenderer.SetPositions(trailPositions.ToArray());
        }
    }
}
