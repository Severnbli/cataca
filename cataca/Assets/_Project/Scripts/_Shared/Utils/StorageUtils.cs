using System;
using System.Collections.Generic;
using _Project.Scripts.Features.Data.Entities;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using UnityEngine;

namespace _Project.Scripts._Shared.Utils
{
    public static class StorageUtils
    {
        public static Level LoadLevelToLoad(BuiltInStorageConfig config)
        {
            return Load<Level>(config.LevelToLoad);
        }

        public static void SaveLevelToLoad(BuiltInStorageConfig config, Level level)
        {
            Save(config.LevelToLoad, level);
        }
        
        public static List<Level> LoadLevels(BuiltInStorageConfig config)
        {
            return LoadBuiltInList<Level>(config.Levels);
        }

        public static void SaveLevels(BuiltInStorageConfig config, List<Level> levels)
        {
            SaveBuiltInList(config.Levels, levels);
        }

        public static List<Record> LoadRecords(BuiltInStorageConfig config)
        {
            return LoadBuiltInList<Record>(config.Records);
        }

        public static void SaveRecords(BuiltInStorageConfig config, List<Record> records)
        {
            SaveBuiltInList(config.Records, records);
        }

        public static Settings LoadSettings(BuiltInStorageConfig config)
        {
            return Load<Settings>(config.Settings);
        }

        public static void SaveSettings(BuiltInStorageConfig config, Settings settings)
        {
            Save(config.Settings, settings);
        }

        public static List<T> LoadBuiltInList<T>(string path)
        {
            var jsonData = PlayerPrefs.GetString(path, string.Empty);
            var list = string.IsNullOrEmpty(jsonData)
                ? new List<T>()
                : JsonUtils.FromJson<List<T>>(jsonData);
            return list;
        }

        public static void SaveBuiltInList<T>(string path, List<T> list)
        {
            PlayerPrefs.SetString(path, JsonUtils.ToJson(list));
        }

        public static T Load<T>(string path) where T : new()
        {
            var jsonData = PlayerPrefs.GetString(path, String.Empty);
            var obj = string.IsNullOrEmpty(jsonData)
                ? new T()
                : JsonUtils.FromJson<T>(jsonData);
            return obj;
        }

        public static void Save<T>(string path, T data)
        {
            var jsonData = JsonUtils.ToJson(data);
            PlayerPrefs.SetString(path, jsonData);
        }
    }
}