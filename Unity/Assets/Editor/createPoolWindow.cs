using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class GameObjectExtensions
{
    public static IEnumerable<GameObject> GetChildren(this GameObject go)
    {
        yield return go;

        for (var i = 0; i < go.transform.childCount; i++)
        {
            foreach (var child in go.transform.GetChild(i).gameObject.GetChildren())
            {
                yield return child;
            }
        }
    }

    public static IEnumerable<GameObject> GetChildren(this GameObject[] gos)
    {
        return gos.SelectMany(go1 => go1.GetChildren());
    }
}

public class createPoolWindow : EditorWindow
{
    #region Singleton Implementation

    private static createPoolWindow instance;
    public static createPoolWindow Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new createPoolWindow();
            }
            return instance;
        }
    }

    private createPoolWindow()
    {

    }
    #endregion

    private int enemiesNumber = 50;
    private GameObject enemyPrefab = null;
    private GameObject enemyPool = null;


    public void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of enemies");
        enemiesNumber = EditorGUILayout.IntField(enemiesNumber);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Enemy prefab");
        enemyPrefab = (GameObject)EditorGUILayout.ObjectField(enemyPrefab, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Pool prefab");
        enemyPool = (GameObject)EditorGUILayout.ObjectField(enemyPool, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create Pool"))
        {
            generatEnemyPool();
        }

        EditorGUILayout.EndVertical();
    }

    private void generatEnemyPool()
    {
        GameObject[] enemies = new GameObject[enemiesNumber];

        var pool = GameObject.Instantiate(enemyPool);
        pool.transform.position = new Vector3(0, -10, 0);
        Undo.RegisterCreatedObjectUndo(pool, "PoolCreation");

        for (var i = 0; i < enemiesNumber; i++)
        {
            enemies[i] = GameObject.Instantiate(enemyPrefab);
            enemies[i].transform.position = new Vector3(0, -10, 0);
            enemies[i].gameObject.SetActive(false);
            enemies[i].GetComponent<enemyNavigation>().pool = pool.GetComponent<SCRIPT_enemyPool>();
            Undo.RegisterCreatedObjectUndo(enemies[i], "PoolCreation");
        }
        
        pool.GetComponent<SCRIPT_enemyPool>().enemies = enemies;

        Selection.gameObjects.GetChildren().All((spawner) =>
        {
            spawner.GetComponent<SCRIPT_enemySpawner>().pool = pool.GetComponent<SCRIPT_enemyPool>();
            return true;
        });
    }
}