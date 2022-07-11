
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

namespace Generics.Packages.InputSystem
{

    public class ScreenJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [HideInInspector] public Vector3 m_StartPos;

        [FormerlySerializedAs("movementRange")]
        [SerializeField]
        private float m_MovementRange = 50;

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;

        private RectTransform _rect;
        private RectTransform _parent;
        private Vector2 m_PointerDownPos;

        public RectTransform Rect => _rect ??= (RectTransform)transform;
        public RectTransform Parent => _parent ??= (RectTransform)transform.parent;
        public float movementRange
        {
            get => m_MovementRange;
            set => m_MovementRange = value;
        }

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }

        #region Unity Methods

        private void Start()
        {
            m_StartPos = Rect.anchoredPosition;
        }

        #endregion

        #region Listeners

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Parent, 
                eventData.position, 
                eventData.pressEventCamera, 
                out m_PointerDownPos
            );
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Parent, 
                eventData.position, 
                eventData.pressEventCamera, 
                out var position
            );
            var delta = position - m_PointerDownPos;

            delta = Vector2.ClampMagnitude(delta, movementRange);
            Rect.anchoredPosition = m_StartPos + (Vector3)delta;

            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
            SendValueToControl(newPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Rect.anchoredPosition = m_StartPos;
            SendValueToControl(Vector2.zero);
        }

        #endregion


    }

}