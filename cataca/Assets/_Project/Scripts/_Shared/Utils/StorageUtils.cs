using System;
using System.Collections.Generic;
using _Project.Scripts.Features.Data.Entities;
using _Project.Scripts.Features.Data.Storages.BuiltIn.Configs;
using UnityEngine;

namespace _Project.Scripts._Shared.Utils
{
    public static class StorageUtils
    {
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
    }
}