using System;

namespace SmartHome
{
    public abstract class SmartDevice
    {
        public string Name { get; set; }
        public bool IsOn { get; protected set; }
        public int PowerWatts { get; protected set; }
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

        public virtual string GetStatus()
        {
            return $"{Name}: {(IsOn ? "Включено" : "Выключено")} ({PowerWatts} Вт)";
        }
    }
    public class SmartLight : SmartDevice
    {
        public int Brightness { get; private set; } = 80;

        public SmartLight(string name) : base(name, 15) { }
        public void SetBrightness(int level)
        {
            if (level < 0) level = 0;
            if (level > 100) level = 100;
            Brightness = level;
            Console.WriteLine($"[{Name}] Яркость изменена на {Brightness}%.");
        }
        public void SetBrightness(string preset)
        {
            switch (preset.ToLower())
            {
                case "ночь":
                    Brightness = 15;
                    break;
                case "вечер":
                    Brightness = 50;
                    break;
                case "максимум":
                    Brightness = 100;
                    break;
                default:
                    Brightness = 80;
                    break;
            }
            Console.WriteLine($"[{Name}] Применён режим «{preset}» (яркость {Brightness}%).");
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Текущая яркость: {Brightness}%.");
        }

        public override string GetStatus() => $"{base.GetStatus()}, Яркость: {Brightness}%";
    }

    public class Thermostat : SmartDevice
    {
        public double TargetTemperature { get; set; } = 22.0;

        public Thermostat(string name) : base(name, 1500) { }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Поддерживается температура {TargetTemperature}°C.");
        }

        public override string GetStatus() => $"{base.GetStatus()}, Температура: {TargetTemperature}°C";
    }

    public class SmartSpeaker : SmartDevice
    {
        public int Volume { get; set; } = 50;

        public SmartSpeaker(string name) : base(name, 20) { }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Громкость установлена на {Volume}%.");
        }

        public override string GetStatus() => $"{base.GetStatus()}, Громкость: {Volume}%";
    }
    public class SecurityCamera : SmartDevice
    {
        public bool IsRecording { get; private set; }

        public SecurityCamera(string name) : base(name, 10) { }

        public override void TurnOn()
        {
            base.TurnOn();
            IsRecording = true;
            Console.WriteLine($"[{Name}] Начинает запись.");
        }

        public override void TurnOff()
        {
            IsRecording = false;
            base.TurnOff();
        }

        public override string GetStatus() => $"{base.GetStatus()}, Запись: {(IsRecording ? "идёт" : "нет")}";
    }

    public class RobotVacuum : SmartDevice
    {
        public int BatteryLevel { get; set; }

        public RobotVacuum(string name, int batteryLevel = 100) : base(name, 45)
        {
            BatteryLevel = batteryLevel;
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Уровень заряда: {BatteryLevel}%.");
        }

        public override string GetStatus() => $"{base.GetStatus()}, Заряд: {BatteryLevel}%";
    }

    public class SmartKettle : SmartDevice
    {
        public int WaterTemperature { get; set; }

        public SmartKettle(string name, int waterTemperature = 100) : base(name, 2000)
        {
            WaterTemperature = waterTemperature;
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Начинает нагрев до заданной температуры ({WaterTemperature}°C).");
        }

        public override string GetStatus() => $"{base.GetStatus()}, Целевая температура: {WaterTemperature}°C";
    }
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== ДЕМОНСТРАЦИЯ ПЕРЕГРУЗКИ ===");
            SmartLight livingRoomLight = new SmartLight("Люстра");
            livingRoomLight.SetBrightness(65);
            livingRoomLight.SetBrightness("вечер");

            SmartDevice[] devices = new SmartDevice[]
            {
                livingRoomLight,
                new Thermostat("Термостат"),
                new SmartSpeaker("Колонка"),
                new SecurityCamera("Камера"),
                new RobotVacuum("Пылесос", 85),
                new SmartKettle("Чайник", 95)
            };

            while (true)
            {
                Console.WriteLine("\n=== УМНЫЙ ДОМ ===");
                Console.WriteLine("1. Показать все устройства");
                Console.WriteLine("2. Включить все устройства");
                Console.WriteLine("3. Выключить все устройства");
                Console.WriteLine("4. Включить устройство по имени");
                Console.WriteLine("5. Выключить устройство по имени");
                Console.WriteLine("6. Статистика (сколько включено / выключено)");
                Console.WriteLine("7. Показать суммарное энергопотребление");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0")
                    break;

                switch (choice)
                {
                    case "1":
                        foreach (SmartDevice device in devices)
                        {
                            Console.WriteLine(device.GetStatus());
                        }
                        break;

                    case "2":
                        foreach (SmartDevice device in devices)
                        {
                            device.TurnOn();
                        }
                        break;

                    case "3":
                        foreach (SmartDevice device in devices)
                        {
                            device.TurnOff();
                        }
                        break;

                    case "4":
                        Console.Write("Введите имя устройства для включения: ");
                        string nameToTurnOn = Console.ReadLine();
                        SmartDevice foundOn = FindDeviceByName(devices, nameToTurnOn);
                        if (foundOn != null)
                            foundOn.TurnOn();
                        else
                            Console.WriteLine("Устройство с таким именем не найдено.");
                        break;

                    case "5":
                        Console.Write("Введите имя устройства для выключения: ");
                        string nameToTurnOff = Console.ReadLine();
                        SmartDevice foundOff = FindDeviceByName(devices, nameToTurnOff);
                        if (foundOff != null)
                            foundOff.TurnOff();
                        else
                            Console.WriteLine("Устройство с таким именем не найдено.");
                        break;

                    case "6":
                        int onCount = 0;
                        int offCount = 0;
                        foreach (SmartDevice device in devices)
                        {
                            if (device.IsOn)
                                onCount++;
                            else
                                offCount++;
                        }
                        Console.WriteLine($"Включено устройств: {onCount}");
                        Console.WriteLine($"Выключено устройств: {offCount}");
                        break;

                    case "7":
                        int totalWatts = 0;
                        foreach (SmartDevice device in devices)
                        {
                            if (device.IsOn)
                            {
                                totalWatts += device.PowerWatts;
                            }
                        }
                        Console.WriteLine($"Суммарное энергопотребление включённых устройств: {totalWatts} Вт");
                        break;

                    default:
                        Console.WriteLine("Неверный номер пункта.");
                        break;
                }
            }
        }
        static SmartDevice FindDeviceByName(SmartDevice[] devices, string name)
        {
            foreach (SmartDevice device in devices)
            {
                if (string.Equals(device.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return device;
                }
            }
            return null;
        }
    }
}