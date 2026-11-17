using System;

namespace SmartHome.Models
{
    public class SmartLight : SmartDevice
    {
        public int Brightness { get; private set; } = 80;

        public SmartLight(string name) : base(name, 15) { }

        public void SetBrightness(int level)
        {
            if (level < 0) level = 0;
            if (level > 100) level = 100;
            Brightness = level;
            Console.WriteLine($"[{Name}] Яркость: {Brightness}%.");
        }

        public void SetBrightness(string preset)
        {
            switch (preset.ToLower())
            {
                case "ночь": Brightness = 15; break;
                case "вечер": Brightness = 50; break;
                case "максимум": Brightness = 100; break;
                default: Brightness = 80; break;
            }
            Console.WriteLine($"[{Name}] Режим «{preset}» ({Brightness}%).");
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Текущая яркость: {Brightness}%.");
        }

        public override string GetStatus() => $"{Name}: {(IsOn ? "Вкл" : "Выкл")} | Яркость: {Brightness}% | {GetPowerConsumption()} Вт";
    }
}












