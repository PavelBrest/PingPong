﻿using System;

namespace PingPong
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new PongGame())
				game.Run();
		}
	}
}
