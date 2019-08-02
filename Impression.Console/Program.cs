using System;

namespace ES.ON.Impression.ConsoleApp {
	class Program {
		static int Main(string[] args) {
			// The console is not the main use case;
			// Arguments and switches are processed in she simplest "quick & dirty" way, 
			// at the expense of code generality.

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

				// If any argument is -h, just show help and ignore the rest.
				if(args[0] == "-h" || args[1] == "-h") {
					ShowHelp();
					return 0;
				}

				// If there are 2 arguments, one of them must be -t;
				// Here the other must be the input.
				if(args[0] != "-t" && args[1] != "-t") {
					Console.WriteLine("Error: Invalid argument supplied.");
					return 2;
				}
				int position = args[0] == "-t" ? 1 : 0;
				return Do(args[position], false);
			}

			// Never reach this...
			throw new Exception("Internal Argument Processing Error.");
		}

		static void ShowHelp() {
			Console.WriteLine("Usage: Supply impression query as an argument enclosed in double quotes.\nUse -t to display the regex without modifiers (for testing purposes).");
		}

		static int Do(string input, bool full) {
			try {
				if(full) Console.WriteLine(ImpressionToRegex.Convert(input));
				else Console.WriteLine(ImpressionToRegex.ConvertNoOptions(input));
				return 0;
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				return 1;
			}
		}
	}
}
