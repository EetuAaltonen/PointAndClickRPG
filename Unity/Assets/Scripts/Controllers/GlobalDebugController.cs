using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalDebugController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DebugInfoPanel;

    private TextMeshProUGUI m_TextMousePosition;
    private TextMeshProUGUI m_TextCursorPosition;
    private TextMeshProUGUI m_TextTilePosition;
    private TextMeshProUGUI m_TextTileIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMousePosition = m_DebugInfoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        m_TextCursorPosition = m_DebugInfoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        m_TextTilePosition = m_DebugInfoPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        m_TextTileIndex = m_DebugInfoPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMouseDebugInfo(Vector3 mousePosition, Vector3 cursorPosition, Vector3 tilePosition, TileIndex tileIndex)
    {
        m_TextMousePosition.text = $"Mouse: {mousePosition}";
        m_TextCursorPosition.text = $"Cursor: {cursorPosition}";
        m_TextTilePosition.text = $"Tile: {tilePosition}";
        m_TextTileIndex.text = $"Index: X {tileIndex.X} Z {tileIndex.Z}";
    }
}
