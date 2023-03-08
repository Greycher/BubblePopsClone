using System;
using System.Collections.Generic;
using SceneGUIAttributes.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BubblePopsClone.Runtime
{
    public class BubbleLauncher : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private BubleLayout bubleLayout;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField, PositionHandle] private Vector3 launchPosition;

        private List<Vector3> _rayPointList = new List<Vector3>();

        public void OnPointerDown(PointerEventData eventData)
        {
            lineRenderer.enabled = true;
            UpdateLineRenderer(eventData.position);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            UpdateLineRenderer(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lineRenderer.enabled = false;
        }

        private void UpdateLineRenderer(Vector3 mousePos)
        {
            var pointedPos = ProjectMousePosToXYPlane(mousePos);
            var direction = (pointedPos - launchPosition).normalized;
            var ray = new Ray(launchPosition, direction);
            var limit = 5;
            _rayPointList.Clear();
            _rayPointList.Add(ray.origin);
            RaycastHit hit;
            while (!Physics.Raycast(ray, out hit))
            {
                if (--limit < 0)
                {
                    throw new Exception();
                }
                ray = BounceRayOfBounds(ray, bubleLayout.Bounds);
                _rayPointList.Add(ray.origin);
            }
            _rayPointList.Add(hit.point);
            lineRenderer.SetPositions(_rayPointList.ToArray());
        }

        private Vector3 ProjectMousePosToXYPlane(Vector3 mousePosition)
        {
            var ray = Camera.main.ScreenPointToRay(mousePosition);
            var plane = new Plane(Vector3.back, 0);
            plane.Raycast(ray, out float d);
            return ray.GetPoint(d);
        }

        private Ray BounceRayOfBounds(Ray ray, Bounds bounds)
        {
            bounds.IntersectRay(ray, out float d);
            var pos = ray.GetPoint(d);
            Vector3 normal;
            if (Mathf.Approximately(pos.x, bounds.min.x))
            {
                normal = Vector3.right;
            }
            else if(Mathf.Approximately(pos.x, bounds.max.x))
            {
                normal = Vector3.left;
            }
            else
            {
                throw new Exception();
            }

            return new Ray(pos, Vector3.Reflect(ray.direction, normal));
        }
    }
}