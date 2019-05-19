using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FieldManager : MonoBehaviour, ISize {

    public int width, height;
    public int Width { get; set; }
    public int Height { get; set; }
    GameObject[,] matrix;

    void InitializeMatrix()
    {
        matrix = new GameObject[width, height];
        var oList = FindObjectsOfType<MonoBehaviour>().OfType<IFieldObject>();
        int i, j;
        foreach (var o in oList)
        {
            i = (int)o.go.transform.position.x;
            j = (int)o.go.transform.position.y;
            if (i < width && j < height)
            {
                o.go.transform.position = new Vector2(i, j);
                matrix[i, j] = o.go;
                Debug.Log(o.go.name + ": " + i + "; " + j);
            }
        }
    }

    void IterateOnField(Vector2 direction)
    {
        int i, j;
        int iInit = 0; int jInit = 0;
        int w = width; int h = height;
        int m = 1;
        GameObject gObj;
        List<GameObject> objQueue = new List<GameObject>();

        if (direction.y < 0)
        {
            i = 0; j = 0;
            w += h; h = w - h; w -= h;
            m = 1;
        }
        if (direction.y > 0)
        {
            iInit = h - 1; jInit = w - 1;
            w = -1; h = -1;
            m = -1;
        }
        if (direction.x > 0)
        {
            iInit = w - 1; jInit = h - 1;
            w = -1; h = -1;
            m = -1;
        }
    
        for (i = iInit; i * m < w * m; i += m)
        {
            for (j = jInit; j * m < h * m; j += m)
            {
                if (direction.x == 0)
                {
                    gObj = matrix[j, i];
                }
                else
                {
                    gObj = matrix[i, j];
                }
                if (gObj != null)
                {
                    // Debug.Log("FieldManager: i = " + i + "; j = " + j);
                    objQueue.Add(gObj);
                }
            }
        }
        
        foreach (GameObject o in objQueue)
        {
            matrix = o.GetComponent<IFieldObject>().Handler(matrix, direction);
        }
    }

    // Use this for initialization
    void Start()
    {
        Width = width;
        Height = height;
        InitializeMatrix();
        InputController.OnInput += IterateOnField;
    }
}
