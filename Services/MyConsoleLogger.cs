using System;

namespace MyTodoApp.Services
{
    public class MyConsoleLogger : ILog
    {
        public void Info(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
        }
    }
}
