using TMPro;
using UnityEngine;
namespace AStar.UI
{
    public class UnitButtonController : MonoBehaviour
    {
        #region Fields And Variables
        [SerializeField] TextMeshProUGUI buttonText;
        #endregion
        #region Unity Methods
        
        #endregion
        #region Custom Methods
        public void SetText(string text)
        { if(buttonText) buttonText.text = text; }
        #endregion
    }
}
