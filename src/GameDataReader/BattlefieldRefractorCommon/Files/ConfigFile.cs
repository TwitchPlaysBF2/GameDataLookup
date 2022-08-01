﻿using GameDataReader.BattlefieldRefractorCommon.Parsing;
using GameDataReader.Common.Files;

namespace GameDataReader.BattlefieldRefractorCommon.Files;

/// <summary>
/// Represents a Refractor engine .con configuration file.
/// </summary>
/// <typeparam name="T">The type of the config file.</typeparam>
internal abstract class ConfigFile<T> : ConfigFile
{
    protected readonly string GameName;

    protected ConfigFile(string gameName)
    {
        GameName = gameName;
    }

    protected override string GetSettingValue(string settingKey)
    {
        var settingFinder = ReadConfigFile();
        var setting = settingFinder.GetSetting(settingKey);
        var value = setting.ParseValue();
        return value;
    }

    protected override Common.Parsing.SettingResolver ReadConfigFile()
    {
        var filePath = GetFilePath();
        if (!File.Exists(filePath))
            throw new FileNotFoundException(
                "Couldn't find configuration data. Is the game installed?\r\n" +
                $"{typeof(T).FullName} not found at location: {filePath}");

        var configLines = File.ReadAllLines(filePath);
        var resolver = new SettingResolver(configLines);
        return resolver;
    }
}