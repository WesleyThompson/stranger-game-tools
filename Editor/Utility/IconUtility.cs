using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace StrangerGameTools.Editor.Utility
{
    /// <summary>
    /// A utility class for handling icons in the Unity Editor.
    /// </summary>
    [InitializeOnLoad]
    public static class IconUtility
    {
        static readonly Dictionary<string, Texture2D> _iconCache = new Dictionary<string, Texture2D>();

        static IconUtility()
        {
            EditorApplication.delayCall += AssignIcons;
        }

        public static void AssignIcons()
        {
            var scriptPaths = GetAllStrangerGameToolsScripts();
            foreach (var scriptPath in scriptPaths)
            {
                var monoImporter = AssetImporter.GetAtPath(scriptPath) as MonoImporter;
                if (monoImporter == null)
                    continue;
                var iconPath = GetIconPathForScript(scriptPath);
                if (iconPath != string.Empty)
                {
                    var icon = LoadAssetAtPathCached(iconPath);
                    if (monoImporter.GetIcon() != icon)
                    {
                        monoImporter.SetIcon(icon);
                        monoImporter.SaveAndReimport();
                    }
                }
            }
        }

        static List<string> GetAllStrangerGameToolsScripts()
        {
            var scripts = new List<string>();
            var directories = Directory.GetDirectories("Packages/com.strangerandstrangergames.strangergametools/Runtime");
            foreach (var directory in directories)
            {
                scripts.AddRange(Directory.GetFiles(directory, "*.cs"));
            }

            return scripts;
        }

        static string GetIconPathForScript(string scriptPath)
        {
            return "Packages/com.strangerandstrangergames.strangergametools/Editor/EditorResources/Icons/StrangerLogoSq256.png";
        }

        static Texture2D LoadAssetAtPathCached(string path)
        {
            if (!_iconCache.ContainsKey(path))
            {
                _iconCache[path] = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            }
            return _iconCache[path];
        }
    }
}
