using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
    
    [SerializeField]
    private Camera m_Camera;
    
    [SerializeField]
    private GameObject m_TargetDestination;

    [SerializeField]
    private GlobalTileController m_GlobalTileController;

    [SerializeField]
    private Canvas m_Canvas;

    [SerializeField]
    private TextMeshProUGUI m_TextMousePosition;
    [SerializeField]
    private TextMeshProUGUI m_TextCursorLocation;
    [SerializeField]
    private TextMeshProUGUI m_TextTileLocation;
    [SerializeField]
    private TextMeshProUGUI m_TextTileIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_NavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint))
        {
            Vector3 tileLocation = UtilityTiles.LocationToTile(hitPoint.point);
            m_TextMousePosition.text = $"Mouse: {hitPoint.point}";
            m_TextTileLocation.text = $"Tile: {tileLocation}";
            TileIndex tileIndex = UtilityTiles.LocationToTileIndex(m_TargetDestination.transform.position);
            m_TextTileIndex.text = $"Index: X {tileIndex.X} Z {tileIndex.Z}";

            if (Input.GetMouseButtonDown(0))
            {
                if (m_GlobalTileController.RequestTileIndex(tileIndex, gameObject))
                {
                    //Debug.Log($"{UtilityTiles.LocationToTileIndex(hitPoint.point)} clicked");
                    m_NavMeshAgent.SetDestination(tileLocation);
                }
            }

            m_TargetDestination.transform.position = tileLocation;
            m_TextCursorLocation.text = $"Cursor: {m_TargetDestination.transform.position}";
        }

        
    }
}
