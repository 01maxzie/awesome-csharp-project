using System.Text;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Spectre.Console;

namespace Awesome_C__Project
{
    internal class Program
    {
        public static List<Option> options;
        private static int lastSelection = -1; // Where we write the ">".
        static void Main(string[] args)
        {
            Encoding UTF8 = Encoding.UTF8;
            Console.OutputEncoding = UTF8;

            // Create options that you want your menu to have

            options = new List<Option>
            {
                new Option("  🏡 Home      ", () => HomePageLink()),
                new Option("  📩 Request   ", () => RequestPageLink()),
                new Option("  ⚙️ Configure ", () => ConfigurePageLink()),
                new Option("  💫 Updates   ", () => UpdatePageLink()),
                new Option("  ❌ Exit      ", () => Environment.Exit(0)),

            };

            // Set the default index of the selected item to be the first
            int index = 0;

            // Write the menu out
            WriteMenu(options, options[index]);


            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                // Handle each key input (down arrow will write the menu again with a different selected item)
                switch (keyinfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index + 1 < options.Count)
                        {
                            index++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                        {
                            index--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear(); // The only place where we clear.
                        options[index].Selected.Invoke();
                        lastSelection = index;
                        break;
                }
                WriteMenu(options, options[index]); // Write the menu in all the cases.
            }
            while (keyinfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }
        // Default action of all the options. You can create more methods

        static void WriteAt(int left, int top, string text)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
            Console.Write(text);
        }

        static void WriteMenu(List<Option> options, Option selectedOption)
        {
            // Don't clear here since we want to preserve the output the selected menu item.
            // Instead, let's use our new method WriteAt to write at a specific place.
            for (int i = 0; i < options.Count; i++)
            {
                Option option = options[i];
                if (option == selectedOption)
                {
                    Console.Write("\u001b[48;2;50;50;50m");
                }
                else
                {
                    Console.Write("\u001b[48;2;12;12;12m");
                }
                WriteAt(1, i, i == lastSelection ? "> " : "  ");
                Console.Write(option.Name);
            }
        }

        static void HomePageLink()
        {
            WriteAt(20, 0, "Home");
            WriteAt(20, 1, "──────────────────────────────");
            WriteAt(20, 3, "Nothing here yet, sorry.");
        }

        static void RequestPageLink()
        {
            WriteAt(20, 0, "Request");
            WriteAt(20, 1, "──────────────────────────────");
            WriteAt(20, 3, "Nothing here yet, sorry.");
        }

        static void UpdatePageLink()
        {
            WriteAt(20, 0, "Updates");
            WriteAt(20, 1, "──────────────────────────────");
            WriteAt(20, 3, "Nothing here yet, sorry.");
        }

        static void ConfigurePageLink()
        {
            WriteAt(20, 0, "Configure");
            WriteAt(20, 1, "──────────────────────────────");
            WriteAt(20, 3, "Nothing here yet, sorry.");
        }
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }

        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
