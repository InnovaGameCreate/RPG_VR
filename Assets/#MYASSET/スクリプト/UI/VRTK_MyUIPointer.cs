// UI Pointer|UI|80020
namespace VRTK
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    [AddComponentMenu("VRTK/Scripts/UI/VRTK_UICustomPointer")]
    public class VRTK_MyUIPointer : VRTK_UIPointer
    {
        public const float distance = 0.1f;
        /// <summary>
        /// The IsActivationButtonPressed method is used to determine if the configured activation button is currently in the active state.
        /// </summary>
        /// <returns>Returns true if the activation button is active.</returns>
        public override bool IsActivationButtonPressed()
        {
            return true;
        }
    }
}