using System;
using System.IO;
using System.Text;
using UnityEngine;

public sealed class PersistentCrashLogger : MonoBehaviour
{
    private static PersistentCrashLogger instance;
    private static readonly object WriteLock = new object();

    private StreamWriter writer;
    private string logFilePath;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (instance != null)
        {
            return;
        }

        var gameObject = new GameObject(nameof(PersistentCrashLogger));
        instance = gameObject.AddComponent<PersistentCrashLogger>();

        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        try
        {
            string logDirectory =
                Path.Combine(Application.persistentDataPath, "CrashLogs");

            Directory.CreateDirectory(logDirectory);

            string fileName =
                $"session_{DateTime.Now:yyyyMMdd_HHmmss}.log";

            logFilePath = Path.Combine(logDirectory, fileName);

            writer = new StreamWriter(
                logFilePath,
                append: true,
                encoding: new UTF8Encoding(false))
            {
                // クラッシュ前のログをなるべく確実に保存する
                AutoFlush = true
            };

            Application.logMessageReceivedThreaded += OnLogReceived;

            WriteDirect(
                $"Logger started\n" +
                $"Unity: {Application.unityVersion}\n" +
                $"App: {Application.version}\n" +
                $"Device: {SystemInfo.deviceModel}\n" +
                $"OS: {SystemInfo.operatingSystem}\n" +
                $"Memory: {SystemInfo.systemMemorySize} MB\n" +
                $"Path: {logFilePath}");
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }

    private void OnLogReceived(
        string condition,
        string stackTrace,
        LogType type)
    {
        var builder = new StringBuilder();

        builder.Append('[');
        builder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        builder.Append("] [");
        builder.Append(type);
        builder.AppendLine("]");

        builder.AppendLine(condition);

        if (!string.IsNullOrWhiteSpace(stackTrace))
        {
            builder.AppendLine(stackTrace);
        }

        WriteDirect(builder.ToString());
    }

    public static void AddBreadcrumb(string message)
    {
        if (instance == null)
        {
            return;
        }

        instance.WriteDirect(
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] " +
            $"[BREADCRUMB] {message}");
    }

    private void WriteDirect(string message)
    {
        lock (WriteLock)
        {
            try
            {
                writer?.WriteLine(message);
            }
            catch
            {
                // ロガー自身の例外でアプリを壊さない
            }
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        WriteDirect(
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] " +
            $"ApplicationPause: {pauseStatus}");
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        WriteDirect(
            $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] " +
            $"ApplicationFocus: {hasFocus}");
    }

    private void OnApplicationQuit()
    {
        Shutdown();
    }

    private void OnDestroy()
    {
        Shutdown();
    }

    private void Shutdown()
    {
        Application.logMessageReceivedThreaded -= OnLogReceived;

        lock (WriteLock)
        {
            try
            {
                writer?.Flush();
                writer?.Dispose();
                writer = null;
            }
            catch
            {
                // 終了処理中なので無視
            }
        }
    }
}