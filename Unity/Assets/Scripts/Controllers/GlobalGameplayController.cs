using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameplayController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement m_PlayerMovement;

    [SerializeField]
    private GlobalTileDataController m_GlobalTileDataController;

    [SerializeField]
    private GlobalCameraController m_GlobalCameraController;

    [SerializeField]
    private GlobalDebugController m_GlobalDebugController;

    [SerializeField]
    private GameObject m_CursorTileIndicator;

    private TileIndex m_CursorTileIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = m_GlobalCameraController.GetMainCamera().ScreenPointToRay(Input.mousePosition);
        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint, 100))
        {
            Vector3 tileLocation = UtilityTiles.LocationToTile(hitPoint.point);
            m_CursorTileIndex = UtilityTiles.LocationToTileIndex(m_CursorTileIndicator.transform.position);
            m_CursorTileIndicator.transform.position = tileLocation;

            m_GlobalDebugController.UpdateMouseDebugInfo(
                hitPoint.point,
                m_CursorTileIndicator.transform.position,
                tileLocation,
                m_CursorTileIndex
            );
        }
    }

    public TileIndex GetCursorTileIndex()
    {
        return m_CursorTileIndex;
    }
}
