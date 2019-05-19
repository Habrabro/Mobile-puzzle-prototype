using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour, IFieldObject {

    public GameObject go { get { return gameObject; } }

    public GameObject[,] Handler(GameObject[,] matrix, Vector2 direction)
    {
        return matrix;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
