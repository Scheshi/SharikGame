using UnityEngine;


namespace SharikGame
{
    public class PointBonus : InteractableObjectsController
    {
        private SliderController _sliderController;
        public PointBonus(GameObject gameObject) : base(gameObject)
        {
            _sliderController = ServiceLocator.GetDepencity<SliderController>();
        }

        public override void Interacte()
        {
            _sliderController.ChangeValue();
        }
    }
}
