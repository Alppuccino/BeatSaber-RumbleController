using BS_Utils.Utilities;

namespace Rumble_Controller
{
    internal class PluginConfig
    {
        public bool RegenerateConfig = true;
        private static Config config = new Config("RumbleController");

        public static bool enabled
        {
            get
            {
                return config.GetBool("RumbleController", "Enabled", true, true);
            }
            set
            {
                config.SetBool("RumbleController", "Enabled", value);
            }
        }
        public static bool noteCut
        {
            get
            {
                return config.GetBool("RumbleController", "Note Cut Rumble", true, true);
            }
            set
            {
                config.SetBool("RumbleController", "Note Cut Rumble", value);
            }
        }
        public static bool saberClash
        {
            get
            {
                return config.GetBool("RumbleController", "Saber Clash Rumble", true, true);
            }
            set
            {
                config.SetBool("RumbleController", "Saber Clash Rumble", value);
            }
        }
        public static bool obstacleRumble
        {
            get
            {
                return config.GetBool("RumbleController", "Wall Rumble", true, true);
            }
            set
            {
                config.SetBool("RumbleController", "Wall Rumble", value);
            }
        }

    }
}