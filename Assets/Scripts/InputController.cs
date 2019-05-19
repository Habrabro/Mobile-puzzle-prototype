using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

    public delegate void OnInputHandler(Vector2 direction);
    public static event OnInputHandler OnInput;
    int vAxis, hAxis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        vAxis = (int)Input.GetAxisRaw("Vertical");
        hAxis = (int)Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (OnInput != null) { OnInput(new Vector2(0, 1)); }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (OnInput != null) { OnInput(new Vector2(-1, 0)); }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (OnInput != null) { OnInput(new Vector2(0, -1)); }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (OnInput != null) { OnInput(new Vector2(1, 0)); }
        }
    }
}
