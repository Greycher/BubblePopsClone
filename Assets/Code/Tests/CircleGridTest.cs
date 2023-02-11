using System.Collections;
using System.Collections.Generic;
using BubblePopsClone;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class CircleGridTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void WorldToCellFuncTest()
    {
        EditorSceneManager.EnsureUntitledSceneHasBeenSaved("");
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        EditorSceneManager.SetActiveScene(scene);
        var circleGrid = new GameObject().AddComponent<CircleGrid>();
        circleGrid.Radius = 0.5f;
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
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
