namespace SharikGame
{
    public class GoodBonus : Bonus
    {

        protected override void Interaction()
        {
            _slider.Display(1);
            _text.Display(1);
        }
    }
}
