﻿using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using Jot;
using Jot.Storage;

namespace Infrastructure
{
    public static class Globals
    {
        static Globals()
        {
            Tracker = new Tracker(new JsonFileStore(UserSettingsFolder));

            Tracker.Configure<Window>()
                .Properties(w => new { w.Top, w.Left })
                .PersistOn(nameof(Window.Closing))
                .StopTrackingOn(nameof(Window.Closing));
        }

        private static readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
        private static readonly string ExecutingAssemblyName = ExecutingAssembly.GetName().CodeBase.Substring(8).Replace('/', '\\');

        public static readonly string AssemblyFolder = Path.GetDirectoryName(ExecutingAssemblyName);
        public static readonly string AssemblyVersion = FileVersionInfo.GetVersionInfo(ExecutingAssembly.Location).ProductVersion.Replace(".0", "");

        public static readonly string UserSettingsFolder = Path.Combine(AssemblyFolder, "user-settings");
        public static readonly Tracker Tracker;
    }
}