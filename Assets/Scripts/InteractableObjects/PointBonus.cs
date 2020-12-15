using UnityEngine;
using System;


namespace SharikGame
{
    [Serializable]
    public class PointBonus : InteractableObjectsController
    {
        [NonSerialized]private SliderController _sliderController;

        public PointBonus()
        {
            return;
        }

        public PointBonus(GameObject gameObject, int objectID) : base(gameObject, objectID)
        {
            _sliderController = ServiceLocator.GetDependency<SliderController>();
        }

        public override void Interact()
        {
            _sliderController.ChangeValue();
        }
    }
}
