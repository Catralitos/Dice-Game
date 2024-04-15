using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Combat
{
    public class NumericalDiceUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public NumericalDice representedDice;
        
        
        [SerializeField] private Canvas canvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Image _image;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _image = GetComponent<Image>();
        }

        public void SetDice(NumericalDice dice)
        {
            representedDice = dice;
            _image.sprite = dice.Faces[0].secondMember;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}