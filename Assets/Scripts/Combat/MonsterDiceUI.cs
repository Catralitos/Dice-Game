using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Combat
{
    public class MonsterDiceUI : MonoBehaviour, IDropHandler
    {
        public NumericalDice assignedDice;
        public Image monsterDiceFace;
        public Image numericalDiceFace;

        public void SetFace(Sprite face)
        {
            monsterDiceFace.sprite = face;
            numericalDiceFace.enabled = false;
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                assignedDice = eventData.pointerDrag.GetComponent<NumericalDiceUI>().representedDice;
                eventData.pointerDrag.gameObject.SetActive(false);
                numericalDiceFace.sprite = assignedDice.Faces[0].secondMember;
                numericalDiceFace.enabled = true;
            }
        }
    }
}