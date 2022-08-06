using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private UnityEngine.AI.NavMeshAgent m_NavMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_NavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetMouseButtonDown(0))
        {
            // Close interaction menu while moving
            m_InteractionMenu.SetActive(false);

            if (m_GlobalTileDataController.RequestTileIndexChange(tileIndex, gameObject))
            {
                m_NavMeshAgent.SetDestination(tileLocation);
            }
        }

        if (Input.GetMouseButtonDown(1))
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
                    RectTransform canvasRectTransform = m_Canvas.GetComponent<RectTransform>();

                    Vector3 mousePos = Input.mousePosition;
                    Vector2 newMenuPosition = new Vector2(mousePos.x, -canvasRectTransform.rect.height + mousePos.y);
                    rectTransform.anchoredPosition = newMenuPosition;

                    // Create menu items
                    float yPos = 0;
                    float menuItemHeight = 18;
                    foreach(string interaction in interactions)
                    {
                        GameObject tempTextBox = Instantiate(m_InteractionMenuItem_Prefab, new Vector2(0, yPos), Quaternion.identity) as GameObject;
                        tempTextBox.transform.SetParent(interactionContent, false);

                        TextMeshProUGUI textElement = tempTextBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                        textElement.text = interaction;

                        yPos -= menuItemHeight;
                    }
                }
                else
                {
                    m_InteractionMenu.SetActive(false);
                }
            }
        }*/
    }
}
