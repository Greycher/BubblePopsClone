using BubblePopsClone.Runtime;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace BubblePopsClone.Test.Code.Test
{
    public class CircleGridTest
    {
        [Test]
        public void WorldToCellFuncTest()
        {
            EditorSceneManager.EnsureUntitledSceneHasBeenSaved("");
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            EditorSceneManager.SetActiveScene(scene);
            var circleGrid = new GameObject().AddComponent<CircleGrid>();
            circleGrid.Radius = 0.5f;
            for (int x = -50; x < 50; x++)
            {
                for (int y = -50; y < 50; y++)
                {
                    var expectedCell = new Vector2Int(x, y);
                    var world = circleGrid.CellToWorld(expectedCell);
                    var cell = circleGrid.WorldToCell(world);
                    Assert.IsTrue(expectedCell == cell);
                }
            }
            EditorSceneManager.CloseScene(scene, true);
        }
    }
}
