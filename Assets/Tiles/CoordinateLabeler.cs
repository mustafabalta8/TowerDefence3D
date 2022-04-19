using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro label;
    private Vector2Int coordinate = new Vector2Int();
    private Tile waypoint;

    [SerializeField] private Color defaultCoordinateColor = Color.white;
    [SerializeField] private Color blockedCoordinateColor = Color.gray;
    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Tile>(); // *** 

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
        if (waypoint.IsPlaceable)
        {
            label.color = defaultCoordinateColor;
        }
        else
        {
            label.color = blockedCoordinateColor;
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
