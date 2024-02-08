using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    [SerializeField]
    private Material highlightMaterial;

    private Environment environment;

    private Transform highlight;
    private Transform selectableTile;
    private Material originalMaterialHighlight;
    private RaycastHit raycastHit;

    void Start()
    {
        environment = GetComponent<Environment>();
    }

    void Update()
    {
        Highlight();

        if (CanPlaceTile()) {
            PlaceTile();
        }
    }

    private void PlaceTile()
    {
        selectableTile = raycastHit.transform;
        highlight = null;

        bool selectionIsBlankTile = selectableTile.CompareTag("Selectable") && selectableTile.gameObject.activeSelf;
        if (!selectionIsBlankTile) {
            return;
        }

        GameObject tileToPlace = Instantiate(GameManager.Instance.SelectedTile, transform);
        environment.Tiles.Add(tileToPlace.GetComponent<Tile>());
        tileToPlace.transform.position = selectableTile.transform.position;
        GameManager.Instance.SelectedTile = null;
        selectableTile.gameObject.SetActive(false);
    }

    private void Highlight()
    {
        if (highlight != null) {
            highlight.GetComponent<MeshRenderer>().sharedMaterial = originalMaterialHighlight;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) {
            if (raycastHit.transform.tag != "Selectable") {
                return;
            }

            highlight = raycastHit.transform;

            bool isHighlighted = highlight.GetComponent<MeshRenderer>().material == highlightMaterial;
            bool isSelected = highlight == selectableTile;

            if (highlight.CompareTag("Selectable") && !isSelected && !isHighlighted) {
                originalMaterialHighlight = highlight.GetComponent<MeshRenderer>().material;
                highlight.GetComponent<MeshRenderer>().material = highlightMaterial;

                return;
            }

            highlight = null;
        }
    }

    private bool CanPlaceTile()
    {
        return Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && highlight && GameManager.Instance.SelectedTile != null;
    }
}
