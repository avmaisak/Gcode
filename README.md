# Gcode

[![Build status](https://ci.appveyor.com/api/projects/status/6jt202mby0ajjire?svg=true)](https://ci.appveyor.com/project/avmaisak/gcode)

Утилиты для работы с файлами формата G-code для платформы .NET

### Установка

Через Nuget

```
Install-Package Gcode.Utils
```


### Возможности:

- Преобразование сырого кадра в структурированный тип GcodeCommandFrame
- Преобразование типа GcodeCommandFrame в строку для последующей отправки на устройство
- Вычисление контрольной суммы
- Преобразование в формат JSON (RFC 7159)
- Обработка и получение информации (тип слайсера, версия, редакция, время печати, объём потраченного материала для всех экструдеров, стоимость печати, диаметр прутка) в зависимости от слайсера (Cura, KisSlicer, Simplify3d, Slic3R)

### Поддержка платформ:

- MS Windows
- Linux 

### Примеры использования:

````
// преобразование в строку
var gcode = new GcodeCommandFrame {X = 1, Y = 1};
var gcodeStr = GcodeParser.ToStringCommand(gcode);
// результат преобразования
>> 'X1 Y1'

// преобразование в тип GcodeCommandFrame
const string rawString = $"G1 X2.131 Y3.91 Z4.833 E0 F360";
var gcodeConverted = GcodeParser.ToGCode(rawString);
>> gcodeConverted.X = 2.131
>> gcodeConverted.Y = 3.91
>> gcodeConverted.Z = 4.833
>> gcodeConverted.E = 0
>> gcodeConverted.F = 360

// преобразование в JSON
const string rawString = "G1 X626.713 Y251.523 E12.01248; Comment";
var res = GcodeParser.ToJson(raw);
>> {"G":"1","X":"626.713","Y":"251.523","E":"12.01248","Comment":"Comment"}

// контрольная сумма
const string rawString = "M206 T3 P200 X89 ;extruder normal steps per mm";
var gcodeConverted = GcodeParser.ToGCode(rawString);

// установка номера строки
g.N = 1;
// получить контрольную сумму
var crc = GcodeCrc.FrameCrc(g);

// Получить информацию о слайсере
string[] src = "fileContentsArray";
// Slic3R Parser
var parser = new Slic3RParser();
var res = parser.GetSlicerInfo(src);
var volume = res.FilamentUsedExtruder1Volume;
var filamentDiameter = res.FilamentDiameter;
var filamentUsed = res.FilamentUsedExtruder1;

````
