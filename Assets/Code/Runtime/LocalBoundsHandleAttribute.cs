using SceneGUIAttributes.Runtime;
using UnityEngine;

namespace BubblePopsClone.Runtime
{
    public class LocalBoundsHandleAttribute : SceneGUIFieldAttributeWithColorProperty 
    {
        protected override Color DefaultColor => Color.white;

        public LocalBoundsHandleAttribute()
        {
            ToggleWithGizmos = true;
        }
    }
}