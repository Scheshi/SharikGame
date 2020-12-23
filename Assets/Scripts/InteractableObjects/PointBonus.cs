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

        public PointBonus(GameObject gameObject, int objectID, SliderController slider) : base(gameObject, objectID)
        {
            _sliderController = slider;
        }

        public override bool IsActive { get; set; }

        public override void Interact()
        {
            _sliderController.ChangeValue();
        }
    }
}
