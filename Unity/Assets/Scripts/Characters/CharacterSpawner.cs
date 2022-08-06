using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SpawnPrefab;

    [SerializeField]
    private GameObject m_GlobalCharacters;

    [SerializeField]
    private GlobalTileDataController m_GlobalTileDataController;

    private GameObject m_InstanceObject;

    private TileIndex m_TileIndex;

    private UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
    private float m_RequestDestinationTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_InstanceObject = Instantiate(m_SpawnPrefab, transform.position, Quaternion.identity);
        m_InstanceObject.transform.parent = m_GlobalCharacters.transform;

        m_NavMeshAgent = m_InstanceObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        m_TileIndex = UtilityTiles.LocationToTileIndex(transform.position);

        m_GlobalTileDataController.AddTileData(m_TileIndex, m_InstanceObject, m_GlobalCharacters.name);
    }

    // Update is called once per frame
    void Update()
    {
        m_RequestDestinationTimer += Time.deltaTime;
        float seconds = m_RequestDestinationTimer % 60;
        if (seconds > 5)
        {
            // TODO: Fix code. Check wandering area limits.
            /*m_RequestDestinationTimer = 0;
            Vector3 TargetLocation = UtilityTiles.TileIndexToLocation(m_TileIndex);
            Debug.Log($"TargetLocation: {m_TileIndex} -> {TargetLocation}");
            if (m_GlobalTileDataController.RequestTileIndexChange( m_TileIndex, m_InstanceObject))
            {
                m_NavMeshAgent.SetDestination(TargetLocation);
            }
            else
            {
                Debug.Log($"TileIndex BLOCKED: {m_TileIndex}");
            }
            m_TileIndex = new TileIndex(m_TileIndex.X + 1, m_TileIndex.Z);*/
        }
    }
}
