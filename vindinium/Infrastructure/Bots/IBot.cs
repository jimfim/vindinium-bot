namespace vindinium.Infrastructure.Bots
{
	internal interface IBot
	{
		string BotName { get; }

		void Run();
	}
}