using UnityEngine;

namespace BubblePopsClone.Runtime
{
    public class BubleLayout : MonoBehaviour
    {
        [SerializeField] private CircleGrid circleGrid;
        [SerializeField] private Vector2Int dimension;
        
        private Slot[] _slots;
        private Bounds _bounds;

        private const float ColliderThickness = 1;

        public Bounds Bounds
        {
            get => _bounds;
            set => _bounds = value;
        }

        private void Awake()
        {
            Initialize();
            CreateColliders();
        }

        private void Initialize()
        {
            _slots = new Slot[dimension.x * dimension.y];
            for (int y = dimension.y - 1; y >= 0; y--)
            {
                for (int x = 0; x < dimension.x; x++)
                {
                    var cellPos = new Vector2Int(x, y);
                    _slots[(dimension.y - y -1) * dimension.x + x] = new Slot(cellPos);
                }
            }
            
            var min = circleGrid.CellToWorld(_slots[^dimension.x].CellPos) - Vector2.one * circleGrid.Radius;
            var max = circleGrid.CellToWorld(_slots[dimension.x - 1].CellPos) + Vector2.one * circleGrid.Radius;
            _bounds = new Bounds((min + max) / 2, new Vector2(max.x - min.x, max.y - min.y));
        }
        
        private void CreateColliders()
        {
            var center = _bounds.center;
            var min = _bounds.min;
            var max = _bounds.max;

            var halfCollThickness = ColliderThickness / 2;
            var rightColl = gameObject.AddComponent<BoxCollider2D>();
            rightColl.
            rightColl.center = new Vector3(max.x + halfCollThickness, center.y, 0);
            rightColl.size = new Vector3(max.x + halfCollThickness, center.y, 0);
        }

        private void OnDrawGizmos()
        {
            if (!circleGrid)
            {
                return;
            }
            
            Initialize();
            
            foreach (var slot in _slots)
            {
                Gizmos.DrawWireSphere(circleGrid.CellToWorld(slot.CellPos), circleGrid.Radius);
            }

            var oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);
            Gizmos.color = oldColor;
        }

        private class Slot
        {
            public Vector2Int CellPos;

            public Slot(Vector2Int cellPos)
            {
                CellPos = cellPos;
            }
        }
    }
}