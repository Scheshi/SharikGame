namespace SharikGame
{
    public class PointBonus : Bonus
    {

        protected override void Interaction(PlayerView playerView)
        {
            _slider.Display(1);
            _text.Display(1);
        }
    }
}
