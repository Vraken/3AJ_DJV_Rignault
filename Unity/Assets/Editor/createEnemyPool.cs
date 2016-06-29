using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class createEnemyPool : Editor
{
    [MenuItem("Custom Tools/Create enemy pool")]
    public static void CreateTutorialWindow()
    {
        var window = createPoolWindow.Instance;

        window.Show();
    }
}
