using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileSpawner : MonoBehaviour
{
    public GameObject sizeObj;
    ISize size;
    public GameObject tile;
    int width, height;

    // Use this for initialization
    void Start () {
        size = GetComponent<ISize>();
        width = size.Width;
        height = size.Height;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Instantiate(tile, new Vector3(transform.position.x + i, transform.position.y + j, 1),
                    Quaternion.identity);
            }
        }

        Vector3 fieldCenter = new Vector3(width / 2, height / 2, -10);
        Camera.main.transform.position = fieldCenter;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
