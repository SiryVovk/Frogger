using UnityEngine;

public class AppUtils
{
    public static void QuitApplication()
    {
        // If we are running in the editor, stop playing
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Otherwise, quit the application
        Application.Quit();
#endif
    }
}
