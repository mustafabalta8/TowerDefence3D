using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler1 : MonoBehaviour
{

    private TextMeshPro label;
    private Vector2Int coordinate = new Vector2Int();
    

    [SerializeField] private Color defaultCoordinateColor = Color.white;
    [SerializeField] private Color blockedCoordinateColor = Color.gray;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f,0.5f,0f);

    
    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        

        label.enabled = false;
        DisplayCoordinates();
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }
    private void ToggleLabels()
    {
        if (Input.GetKey(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    private void SetLabelColor()
    {
        if (GridManager.Instance == null) { return; }

        Node node = GridManager.Instance.GetNode(coordinate);
        if(node==null) { return; }
        Debug.Log(transform.parent.gameObject.name + " is isExplored: "+node.isExplored);
        if (!node.isWalkable)
        {
            label.color = blockedCoordinateColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultCoordinateColor;
        }
    }

    private void DisplayCoordinates()
    {
        coordinate.x = (int)(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = (int)(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinate.x + "," + coordinate.y;
    }
    private void UpdateObjectName()
    {
        transform.parent.name = coordinate.ToString();
    }
}
