using UnityEngine;
using TMPro;

namespace DoublePendulumProject.UI 
{    
    using Gameplay;

    public class PendulumUIInfo : MonoBehaviour
    {
        [Header("PENDULUM A VALUES")]
        public TextMeshProUGUI lengthA;
        public TextMeshProUGUI massA;
        public TextMeshProUGUI angleA;
        public TextMeshProUGUI velocityA;

        [Header("PENDULUM B VALUES")]
        public TextMeshProUGUI lengthB;
        public TextMeshProUGUI massB;
        public TextMeshProUGUI angleB;
        public TextMeshProUGUI velocityB;

        public void UpdateInfo(DoublePendulum selectedPendulum) {
            // PENDULUM A values assignement
            this.lengthA.text = selectedPendulum.pendulumA.length.ToString();
            this.massA.text = selectedPendulum.pendulumA.mass.ToString();
            this.angleB.text = selectedPendulum.pendulumA.angle.ToString();
            this.velocityB.text = selectedPendulum.pendulumA.velocity.ToString();
            // PENDULUM B values assignement
            this.lengthB.text = selectedPendulum.pendulumB.length.ToString();
            this.massB.text = selectedPendulum.pendulumB.mass.ToString();
            this.angleB.text = selectedPendulum.pendulumB.mass.ToString();
            this.velocityB.text = selectedPendulum.pendulumB.velocity.ToString();
        }
    }

}