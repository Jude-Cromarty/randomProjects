using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpellMaster : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject castedSpell;
    public int SpellCost;
    public float spellDamage;
    private GameObject m_DraggingIcon, cardGameObject;
    private Transform cardTransform;
    private RectTransform m_DraggingPlane;
    public bool dragOnSurfaces = true;

    private string cardName;
    private PlayerHandler playerHandler;
    public float castTime; //will be tied to animation time
    private bool hitExists = false;

    private void Start()
    {
        Camera camera = GetComponent<Camera>();
        playerHandler = GameObject.FindObjectOfType<PlayerHandler>();//get player script
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        cardTransform = transform;
        cardGameObject = cardTransform.gameObject;
        cardName = cardGameObject.name; //Get name of card from objects name

        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject(this.gameObject.name);


        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();

        image.sprite = GetComponent<Image>().sprite;
        image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData data)
    {

        if (m_DraggingIcon != null)
            SetDraggedPosition(data);
    }

    private void SetDraggedPosition(PointerEventData data)
    {

        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out Vector3 globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null)
        {
            spellDamage *= playerHandler.damageMultiplier;
            Vector2 worldPos = (Camera.main.ScreenToWorldPoint(m_DraggingIcon.transform.position));// Convert screen position to world space
            Brewery(cardName, worldPos, spellDamage);
            Destroy(m_DraggingIcon);
        }
    }

    static public T FindInParents<T>(GameObject go) where T : Component
    {

        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

    public void Brewery(string spellName, Vector3 droppedLocation, float damage)
    {
        castedSpell = Instantiate(Resources.Load<GameObject>("Spells/" + spellName.Replace("Card", "").Replace("(Clone)", "")), droppedLocation, Quaternion.identity);
        Collider2D castedSpellCollider = castedSpell.GetComponent<Collider2D>();
        Vector2 castedSpellSize = castedSpellCollider.bounds.size;
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemies");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(droppedLocation, castedSpellSize.x / 2f, Vector2.zero, 0f, enemyLayerMask);
        int roundedDamage = Mathf.RoundToInt(damage);
        foreach (RaycastHit2D hit in hits)
        {
            hitExists = true;
            GameObject collidedObject = hit.collider.gameObject;
            Enemy enemy = collidedObject.GetComponent<Enemy>();
            StartCoroutine(damageEnemies(enemy, roundedDamage, castedSpell));
            gameObject.transform.SetParent(null, true);
            gameObject.transform.position = new Vector3(0,0,0);
        }
        if (!hitExists)
{
  Destroy(gameObject);
  Destroy(castedSpell);
}
    }
    public IEnumerator damageEnemies(Enemy enemyScript, int damageRounded, GameObject CastedSpell)
    {
        yield return new WaitForSecondsRealtime(castTime);
        if(!enemyScript){Destroy(castedSpell); yield break;}
        enemyScript.health -= damageRounded * damageRounded / (damageRounded + enemyScript.defense);
        Destroy(CastedSpell);
        Destroy(gameObject);
    }


}
