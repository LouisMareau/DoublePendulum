using UnityEngine.UI;
using TMPro;

namespace DoublePendulumProject.UI.Modules
{

    [System.Serializable]
    public class InfoUILabel 
    {
        
        /// <summary>
        /// Reference to the label.
        /// </summary>
        public TextMeshProUGUI label;

        /// <summary>
        /// Reference to the image (used to display the color of the currently selected double pendulum).
        /// </summary>
        public Image image;

        public InfoUILabel (TextMeshProUGUI label, Image image) {
            this.label = label;
            this.image = image;
        }

    }

}