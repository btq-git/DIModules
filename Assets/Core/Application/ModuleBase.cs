namespace BTQ.Core.Application
{
    public abstract class ModuleBase { }

    public abstract class ModuleBase<T> : ModuleBase where T : ModuleConfig
    {
        protected T config;

        public ModuleBase(T config)
        {
            this.config = config;
        }
    }
}