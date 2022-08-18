using UnityEngine;

namespace LevelManagement
{
    public class SceneLoadedLevelReady : MonoBehaviour, ILevelReady
    {
        public bool IsLevelReady => _isStartCalled;
        
        private bool _isStartCalled;

        private void Start()
        {
            _isStartCalled = true;
        }
    }
}