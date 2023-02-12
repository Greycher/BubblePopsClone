using UnityEngine;

namespace BubblePopsClone.Runtime
{
    public class CircleGrid : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField] private Vector2Int gizmoCellCount = Vector2Int.one * 10;

        public float Radius
        {
            get => radius;
            set => radius = value;
        }

        public Vector2 CellToWorld(Vector2Int cellPosition)
        {
            var p =  new Vector2(cellPosition.x, cellPosition.y) * (
                2 * radius) + new Vector2(cellPosition.y % 2 * radius, 0); ;
            return transform.TransformPoint(p);
        }
        
        public Vector2Int WorldToCell(Vector2 p)
        {
            p = transform.InverseTransformPoint(p);
            var y = (int)(p.y / (2 * radius));
            var x = (int)((p.x - y % 2 * radius) / (2 * radius));
            return new Vector2Int(x, y);
        }

        private void OnDrawGizmosSelected()
        {
            for (int x = 0; x < gizmoCellCount.x; x++)
            {
                for (int y = 0; y < gizmoCellCount.y; y++)
                {
                    Gizmos.DrawWireSphere(CellToWorld(new Vector2Int(x, y)), radius);
                }
            }
        }
    }
}
