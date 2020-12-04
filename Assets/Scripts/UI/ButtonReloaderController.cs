using UnityEngine.SceneManagement;


namespace SharikGame
{
    public class ButtonReloaderController
    {
        public void Reload()
        {
            ControllersUpdater.Dispose();
            ServiceLocator.Dispose();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
