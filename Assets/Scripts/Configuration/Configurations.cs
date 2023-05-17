using UnityEngine;

namespace Configuration
{
    // TODO: Move all project wide configurations over here.
    public class Configurations
    {
        private static Configurations _instance;
        public static Configurations Instance => _instance ??= new Configurations();
    }
}