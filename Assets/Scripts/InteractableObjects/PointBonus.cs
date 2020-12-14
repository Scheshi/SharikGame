using UnityEngine;


namespace SharikGame
{
    public class PointBonus : InteractableObjectsController
    {
        private SliderController _sliderController;

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
