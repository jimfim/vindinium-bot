namespace vindinium.Models.Bots
{
	internal interface IBot
	{
		string BotName { get; }

		void Run();
	}
}