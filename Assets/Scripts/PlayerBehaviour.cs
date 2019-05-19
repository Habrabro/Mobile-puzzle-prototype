using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerBehaviour : MonoBehaviour, IFieldObject, IMovable
{
    public GameObject go { get { return gameObject; } }
    List<FieldPosition> positions = new List<FieldPosition>();
    FieldPosition position;
    FieldPosition Position
    {
        get
        {
            return position;
        }
        set
        {
            if (!isReplaying)
            {
                position = value;
                positions.Add(position);
            }
        }
    }
    int turn = 0;
    public bool isReplaying = false;

    public event OnPositionChangeHandler OnPositionChange;

    public GameObject[,] Handler(GameObject[,] matrix, Vector2 direction)
    {
        if (!isReplaying)
        {
            FieldPosition nextPlayerPosition = GetNextPosWithLineMov(matrix, direction);
            if (nextPlayerPosition.origin.x != -1)
            {
                if (OnPositionChange != null) { OnPositionChange(Position, nextPlayerPosition); }
                matrix = UpdateMatrix(matrix, nextPlayerPosition);
                Position = nextPlayerPosition;
            }
            else
            {
                Position = Position;
            }
        }
        else
        {
            if (OnPositionChange != null)
            {
                turn++;
                OnPositionChange(positions[turn - 1], positions[turn]);
            }
        }
        // Debug.Log("x = " + nextPlayerPosition.origin.x + "; y = " + nextPlayerPosition.origin.y);
        return matrix;
    }

    GameObject[,] UpdateMatrix(GameObject[,] matrix, Vector2 newPosition)
    {
        matrix[(int)Position.origin.x, (int)Position.origin.y] = null;
        matrix[(int)newPosition.x, (int)newPosition.y] = gameObject;
        return matrix;
    }

    FieldPosition GetNextPosWithLineMov(GameObject[,] matrix, Vector2 direction)
    {
        int width = matrix.GetLength(0);
        int height = matrix.GetLength(1);
        FieldPosition fieldPosition = new FieldPosition(Position, width, height);
        FieldPosition nextPos;
        int xx, yy;
        int length = (int)Mathf.Abs(direction.x * (width - 1)) + (int)Mathf.Abs(direction.y * (height - 1));
        for (int i = 0; i < length; i++, fieldPosition += direction)
        {
            nextPos = fieldPosition + direction;
            xx = (int)nextPos.origin.x;
            yy = (int)nextPos.origin.y;
            GameObject fieldObject = matrix[xx, yy];
            if (fieldObject != null)
            {
                if (fieldObject.tag == "Block")
                {                    
                    return fieldPosition;
                }
            }
        }
        fieldPosition.origin = new Vector2(-1, -1); Debug.Log("Ray");
        return fieldPosition;
    }

    /*FieldPosition GetNextPosWithStepMov(GameObject[,] matrix, Vector2 direction)
    {
        int width = matrix.GetLength(0);
        int height = matrix.GetLength(1);
        FieldPosition fieldPosition = new FieldPosition(Position, width, height);
        fieldPosition += direction;
        int x = (int)fieldPosition.origin.x; int y = (int)fieldPosition.origin.y;
        GameObject fieldObject = matrix[x, y];
        if (fieldObject != null)
        {
            if (fieldObject.tag == "Block")
            {
                return fieldPosition;
            }
        }
    }*/

    // Use this for initialization
    void Start () {
        Position = new FieldPosition(transform.position, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
