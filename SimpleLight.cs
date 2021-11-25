using Oxide.Core;

namespace Oxide.Plugins
{
    [Info("SimpleLight", "RFC1920", "1.0.3")]
    [Description("Make SimpleLight and Ceiling light not require power")]
    internal class SimpleLight : RustPlugin
    {
        private ConfigData configData;

        private void OnServerInitialized()
        {
            LoadConfigVariables();
        }

        private void OnEntitySpawned(IOEntity light)
        {
            switch (light.GetType().ToString())
            {
                case "ceilinglight":
                    if (configData.doCeilingLight)
                    {
                        light.SetFlag(BaseEntity.Flags.On, true, false, true);
                    }
                    break;
                case "simplelight":
                    if (configData.doSimpleLight)
                    {
                        light.SetFlag(BaseEntity.Flags.On, true, false, true);
                    }
                    break;
            }
        }

        #region config
        protected override void LoadDefaultConfig()
        {
            Puts("Creating new config file.");
            ConfigData config = new ConfigData
            {
                doSimpleLight = true,
                doCeilingLight = false,
                Version = Version
            };

            SaveConfig(config);
        }

        private void LoadConfigVariables()
        {
            configData = Config.ReadObject<ConfigData>();
            configData.Version = Version;

            SaveConfig(configData);
        }

        private void SaveConfig(ConfigData config)
        {
            Config.WriteObject(config, true);
        }

        public class ConfigData
        {
            public bool doSimpleLight;
            public bool doCeilingLight;

            public VersionNumber Version;
        }
        #endregion
    }
}
