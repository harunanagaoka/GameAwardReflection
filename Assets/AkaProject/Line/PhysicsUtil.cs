using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class PhysicsUtil:MonoBehaviour
    {
        [SerializeField] private float length;
        void Update()
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            var poses = PhysicsUtil.RefrectionLinePoses(transform.position, transform.forward, length, LayerMask.GetMask("Reflectable")).ToArray();
            lineRenderer.positionCount = poses.Length;
            lineRenderer.SetPositions(poses);
        }

        public static List<Vector3> RefrectionLinePoses(Vector3 position, Vector3 direction, float length, LayerMask layerMask)
        {
            var points = new List<Vector3>() { position };
            while (Physics.Raycast(position, direction, out var hit, length, layerMask))
            {
                position = hit.point;
                points.Add(position);
                length -= hit.distance;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            points.Add(position + direction * length);
            return points;
        }
        public static List<Vector3> RefrectionLinePoses(Vector3 position, float radius, Vector3 direction, float length, LayerMask layerMask)
        {
            var points = new List<Vector3>() { position };
            while (Physics.SphereCast(position, radius, direction, out var hit, length, layerMask))
            {
                position = hit.point + hit.normal * radius;
                points.Add(position);
                length -= hit.distance;
                direction = Vector3.Reflect(direction, hit.normal);
            }
            points.Add(position + direction * length);
            return points;
        }
    }
}

