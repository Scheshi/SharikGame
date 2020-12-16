using UnityEngine;


namespace SharikGame
{
    public class PointBonus : InteractableObjectsController
    {
        private SliderController _sliderController;
        public PointBonus(GameObject gameObject) : base(gameObject)
        {
            _sliderController = ServiceLocator.GetDependency<SliderController>();
        }

        public override void Interact()
        {
            _sliderController.ChangeValue();
        }
    }
}
