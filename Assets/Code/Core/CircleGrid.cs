using UnityEngine;
using UnityEngine.Assertions;

namespace BubblePopsClone
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
            return new Vector2(cellPosition.x, cellPosition.y) * (2 * radius) 
                   + new Vector2(radius + cellPosition.y % 2 * radius, radius);
        }
        
        public Vector2Int WorldToCell(Vector2 p)
        {
            var y = (int)((p.y - radius) / (2 * radius));
            var x = (int)((p.x - radius - y % 2 * radius) / (2 * radius));
            return new Vector2Int(x, y);
        }

        private void OnDrawGizmos()
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
