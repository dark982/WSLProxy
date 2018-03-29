using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WSLAtomRedirect
{
	class Program
	{
		static void Main(string[] args)
		{

			Regex rgx = new Regex(@"^[A-Za-z]:\\.*\\.*$");

			string arg = "";

			for(int i = 0; i < args.Length; i++)
			{
				if (rgx.IsMatch(args[i]))
				{
					//Is possible file path
					args[i] = args[i].Replace(@"\","/");
					args[i] = args[i].Replace(":","");

					args[i] = Char.ToLowerInvariant(args[i][0]) + args[i].Substring(1);

					args[i] = @"/mnt/" + args[i];
				}
				else
				{
					//Is not a file path
				}

				arg += args[i] + " ";
			}

			arg = "" + arg;

			Process p = new Process();
			ProcessStartInfo psi = new ProcessStartInfo
			{
				WindowStyle = ProcessWindowStyle.Hidden,
				FileName = @"wsl",
				Arguments = arg,
				UseShellExecute = false,
				RedirectStandardOutput = true,

			};


			p.StartInfo = psi;
			p.Start();

			Console.Write(p.StandardOutput.ReadToEnd());

			p.WaitForExit();
			
		}
	}
}
