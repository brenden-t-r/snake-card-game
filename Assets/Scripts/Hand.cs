using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    private readonly float Z_OFFSET = 0.2f;
    private readonly float LAYOUT_MIN_WIDTH = 0.2f;
    private readonly float LAYOUT_PREFERRED_WIDTH = 2.8f;
    private readonly int HAND_LAYER = 3;

    [SerializeField] private GameObject prefabCard;
    private int numOfCards = 0;

    public void Draw()
    {
        numOfCards += 1;
        GameObject card = Instantiate(prefabCard, transform);
        card.layer = HAND_LAYER;
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        LayoutElement layoutElement = card.AddComponent<LayoutElement>();
        layoutElement.minWidth = LAYOUT_MIN_WIDTH;
        layoutElement.preferredWidth = LAYOUT_PREFERRED_WIDTH;
        rectTransform.position = new Vector3(0, 0, numOfCards * Z_OFFSET);
        Refresh();
    }

    void Refresh()
    {
        SetLayerAllChildren(transform, HAND_LAYER);
    }
    
    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }
    
    public void ShowAllCards()
    {
        SceneManager.LoadScene("Scenes/CardDisplayDemoScene", LoadSceneMode.Additive);
    }
}
