using System;

namespace SmartHome.Models
{
    public abstract class SmartDevice : IEnergyConsumer
    {
        public string Name { get; private set; }
        public bool IsOn { get; private set; }
        public int PowerWatts { get; private set; }

        protected SmartDevice(string name, int powerWatts)
        {
            Name = name;
            PowerWatts = powerWatts;
        }

        public virtual void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"[{Name}] Устройство включено.");
        }

        public virtual void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"[{Name}] Устройство выключено.");
        }

        public int GetPowerConsumption() => IsOn ? PowerWatts : 0;

        public abstract string GetStatus();
    }
}
