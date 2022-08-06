using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GlobalInputController : MonoBehaviour
{
    [SerializeField]
    private GlobalGameplayController m_GlobalGameplayController;

    [SerializeField]
    private GlobalCameraController m_GlobalCameraController;

    [SerializeField]
    private GlobalTileDataController m_GlobalTileDataController;

    [SerializeField]
    private Canvas m_GlobalCanvas;

    [SerializeField]
    private GameObject m_InteractionMenu;

    [SerializeField]
    private GameObject m_InteractionMenuItem_Prefab;

    [SerializeField]
    private GameObject m_Player;
    private PlayerMotor m_PlayerMotor;

    private int UILayer;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerMotor = m_Player.GetComponent<PlayerMotor>();
        UILayer = LayerMask.NameToLayer("UI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // TODO: Block movement if cursor is over an open UI element
            //if (!IsPointerOverUIElement())
            //{
                // Close interaction menu while moving
                m_InteractionMenu.SetActive(false);

                TileIndex tileIndex = m_GlobalGameplayController.GetCursorTileIndex();
                if (m_GlobalTileDataController.RequestTileIndexChange(UtilityTiles.LocationToTileIndex(m_Player.transform.position), tileIndex, m_Player.GetInstanceID()))
                {
                    m_PlayerMotor.MoveToPoint(UtilityTiles.TileIndexToLocation(tileIndex));
                    //m_PlayerNavMeshAgent.SetDestination(UtilityTiles.TileIndexToLocation(tileIndex));
                }
            //}
        } else if (Input.GetMouseButtonDown(1))
        {
            TileIndex tileIndex = m_GlobalGameplayController.GetCursorTileIndex();

            Ray ray = m_GlobalCameraController.GetMainCamera().ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint, 100))
            {
                IInteractable interactable = hitPoint.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    Debug.Log("Interaction!");
                    tileIndex = interactable.GetTileIndex();
                }
            }

            if (tileIndex != null)
            {
                string[] interactions = m_GlobalTileDataController.RequestTileInteractions(tileIndex);

                if (m_InteractionMenu != null)
                {
                    // Clear old menu items
                    Transform interactionContent = m_InteractionMenu.transform.GetChild(0);
                    if (interactionContent != null)
                    {
                        UtilityGameObjects.DeleteAllChild(interactionContent);
                    }

                    if (interactions.Length > 0)
                    {
                        m_InteractionMenu.SetActive(true);
                        RectTransform rectTransform = m_InteractionMenu.GetComponent<RectTransform>();
                        RectTransform canvasRectTransform = m_GlobalCanvas.GetComponent<RectTransform>();

                        Vector3 mousePos = Input.mousePosition;
                        Vector2 newMenuPosition = new Vector2(mousePos.x, -canvasRectTransform.rect.height + mousePos.y);
                        rectTransform.anchoredPosition = newMenuPosition;

                        // Create menu items
                        float yPos = 0;
                        float menuItemHeight = 18;
                        foreach(string interaction in interactions)
                        {
                            GameObject menuItem = Instantiate(m_InteractionMenuItem_Prefab, new Vector2(0, yPos), Quaternion.identity) as GameObject;
                            menuItem.transform.SetParent(interactionContent, false);

                            TextMeshProUGUI textElement = menuItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                            textElement.text = interaction;

                            yPos -= menuItemHeight;
                        }
                    }
                    else
                    {
                        m_InteractionMenu.SetActive(false);
                    }
                }
            }
        }
    }
}
