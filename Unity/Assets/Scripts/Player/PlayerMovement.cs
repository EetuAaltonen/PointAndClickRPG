using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private UnityEngine.AI.NavMeshAgent m_NavMeshAgent;
    
    [SerializeField]
    private Camera m_Camera;
    
    [SerializeField]
    private GameObject m_TargetDestination;

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

            if (Input.GetMouseButtonDown(0))
            {
                m_NavMeshAgent.SetDestination(tileLocation);
            }

            m_TargetDestination.transform.position = tileLocation;
        }

        
    }
}
