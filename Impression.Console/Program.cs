using System;

namespace ES.ON.Impression.ConsoleApp {
	class Program {
		static int Main(string[] args) {
			if(args.Length > 2 || args.Length == 0) {
				Console.WriteLine("Error: Invalid number of arguments, 1 or 2 required. Use -h for help.");
				return 1;
			}

			if(args.Length == 1) {
				if(args[0] == "-h") {
					ShowHelp();
					return 0;

				} else if(args[0] == "-t") {
					Console.WriteLine("Error: Missing argument.");
					return 1;

				} else return Do(args[0], true);
			}
			else if(args.Length == 2) {
				if(args[0] == "-h" || args[1] == "-h") {
					ShowHelp();
					return 0;
				}

				if(args[0] != "-t" && args[1] != "-t") {
					Console.WriteLine("Error: Invalid argument supplied.");
					return 2;
				}
				int position = args[0] == "-t" ? 1 : 0;
				return Do(args[position], false);
			}

			throw new Exception("Never reach this.");
		}

		static void ShowHelp() {
			Console.WriteLine("Usage: Supply impression query as 1 argument enclosed in double quotes.\nUse -t to display the regex without modifiers (for testing purposes).");
		}

		static int Do(string input, bool full) {
			try {
				if(full) Console.WriteLine(ImpressionToRegex.Convert(input));
				else Console.WriteLine(ImpressionToRegex.ConvertNoOptions(input));
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
			return 0;
		}
	}
}
