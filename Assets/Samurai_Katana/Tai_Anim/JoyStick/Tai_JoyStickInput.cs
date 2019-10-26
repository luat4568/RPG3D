using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tai_JoyStickInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public RectTransform borderRect;
    public RectTransform knod;
    public float distanceKnod = 229f;
    public Vector2 moveDir;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {

        radius = borderRect.sizeDelta.x * 0.5f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        moveDir = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(borderRect, eventData.position, eventData.pressEventCamera, out pos);
        Debug.Log(pos);
        float x = pos.x / radius;
        x = Mathf.Clamp(x, -1f, 1f);
        float y = pos.y / radius;
        y = Mathf.Clamp(y, -1f, 1f);

        moveDir = new Vector2(x, y);

        knod.anchoredPosition = new Vector2(moveDir.x * radius, moveDir.y * radius);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        moveDir = Vector2.zero;
    }




    // Update is called once per frame
    void Update()
    {
        if (moveDir.magnitude <= 0)
            knod.anchoredPosition = new Vector2(Tai_InputManager.moveDir.x * radius, Tai_InputManager.moveDir.y * radius);
    }
}
