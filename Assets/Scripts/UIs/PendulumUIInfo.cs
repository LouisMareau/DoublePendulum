using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DoublePendulumProject.UI 
{    
    using Gameplay;

    public class PendulumUIInfo : MonoBehaviour
    {
        [Header("VALUES")]
        public TextMeshProUGUI length;
        public TextMeshProUGUI mass;
        public TextMeshProUGUI angle;
        public TextMeshProUGUI velocity;

        public void UpdateInfo(Pendulum selectedPendulum) {
            this.length.text = selectedPendulum.length.ToString();
            this.mass.text = selectedPendulum.mass.ToString();
            this.angle.text = selectedPendulum.angle.ToString();
            this.velocity.text = selectedPendulum.velocity.ToString();
        }
    }

}